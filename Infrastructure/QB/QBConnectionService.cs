using Common.DTOs.QB.Auth;
using Common.Enums.QB;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Infrastructure.QB
{
    public class QBConnectionService : IQBConnectionService
    {
        private const string QuickBooksMinorVersion = "65";
        public (DateTime time, int count) Last = new(DateTime.UtcNow, 0);

        public QBConnectionService()
        {
            Last = new(DateTime.UtcNow, 0);
        }
        /// <summary>
        /// Generic method for Add//update and Get to QBO
        /// </summary>
        /// <param name="qBAtuth"></param>
        /// <param name="props"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private ServiceContext GetServiceContext(QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token)
        {
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(token);
            ServiceContext serviceContext = new ServiceContext(props.RealmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.MinorVersion.Qbo = QuickBooksMinorVersion;
            serviceContext.IppConfiguration.BaseUrl.Qbo = qBAtuth.Environment == "sandbox" ? "https://sandbox-quickbooks.api.intuit.com/" : "https://quickbooks.api.intuit.com/";
            return serviceContext;
        }
        /// <summary>
        /// Generic Implementation for Getting Query Response from QuickBooks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="qBAtuth"></param>
        /// <param name="data"></param>
        /// <param name="token"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<Tuple<string, List<T>>> GetQueryResponse<T>(QBAuthDTO qBAtuth, QBTokenPropertiesDTO data, string token, string query)
        {
            SetCount();

            ServiceContext serviceContext = GetServiceContext(qBAtuth, data, token);
            Intuit.Ipp.QueryFilter.QueryService<T> CusService = new Intuit.Ipp.QueryFilter.QueryService<T>(serviceContext);
            var result = CusService.ExecuteIdsQuery(query.Replace("%20", " ")).ToList();
            Last.time = DateTime.UtcNow;

            return new Tuple<string, List<T>>(query, result);
        }
        /// <summary>
        ///  Generic Implementation for Adding/Updating Entity from QuickBooks
        /// </summary>
        /// <param name="data"></param>
        /// <param name="qBAtuth"></param>
        /// <param name="props"></param>
        /// <param name="token"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public async Task<IEntity> PerformOperationOnEntity(IEntity data, QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token, QBOActionTypeEnum actionType)
        {
            SetCount();
            ServiceContext serviceContext = GetServiceContext(qBAtuth, props, token);

            DataService service = new DataService(serviceContext);
            if (QBOActionTypeEnum.Add == actionType)
            {
                var res = service.Add(data);
                return res;
            }
            else if (QBOActionTypeEnum.Update == actionType)
            {
                var res = service.Update(data);
                return res;
            }
            else if (QBOActionTypeEnum.Delete == actionType)
            {
                var res = service.Delete(data) as IEntity;
                return res;
            }
            Last.time = DateTime.UtcNow;

            return null;
        }

        private void SetCount()
        {
            if (Last.time - DateTime.UtcNow >= TimeSpan.FromSeconds(10))
            {
                Last.count = 0;
            }
            else if (Last.count > 5)
            {
                Thread.Sleep(10000);
                Last.count = 0;
            }
            else
            {
                Last.count++;

            }
        }

        public async Task<List<(string message, IEntity? entity)>> PerformBatchOperationOnEntity(QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token, List<(IEntity entity, OperationEnum operation)> batchItemRequests, List<string> queries)
        {
            SetCount();

            List<(string, IEntity?)> entities = new List<(string, IEntity?)>();
            ServiceContext serviceContext = GetServiceContext(qBAtuth, props, token);
            DataService service = new DataService(serviceContext);
            Batch batch = service.CreateNewBatch();
            int id = 0;
            if (batchItemRequests != null)
            {
                foreach (var item in batchItemRequests)
                {
                    id++;
                    if (item.entity != null)
                        batch.Add(item.entity, "bId" + id, item.operation);
                }
            }
            else if (queries != null)
            {
                foreach (var item in queries)
                {
                    id++;
                    batch.Add(item, "bId" + id);
                }
            }
            try
            {
                if (batch != null && batch.Count > 0)
                {
                    batch.Execute();
                    id = 0;
                    if (batchItemRequests != null)
                    {
                        foreach (var item in batchItemRequests)
                        {
                            id++;
                            IntuitBatchResponse? Response = batch["bId" + id];
                            if (Response != null)
                            {
                                if (Response.ResponseType == ResponseType.Entity)
                                {
                                    entities.Add((string.Empty, Response?.Entity));
                                }
                                else if (Response.ResponseType == ResponseType.Exception)
                                {
                                    entities.Add((Response.Exception.Message, null));
                                }
                            }
                        }
                    }
                    else if (queries != null)
                    {
                        foreach (var item in queries)
                        {
                            id++;
                            IntuitBatchResponse? Response = batch["bId" + id];
                            if (Response != null)
                            {
                                if (Response.ResponseType == ResponseType.Exception)
                                {
                                    entities.Add((Response.Exception.Message, null));
                                }
                                else if (Response.ResponseType == ResponseType.Query)
                                {
                                    if (Response.Entities != null)
                                    {
                                        var list = Response.Entities.ToList().Select(o => (string.Empty, o ?? null));
                                        entities.AddRange(list);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            Last.time = DateTime.UtcNow;

            return entities;
        }
        public async Task<List<(string message, List<IEntity> entity)>> PerformBatchOperationOnEntities(QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token, List<string> queries)
        {
            SetCount();

            List<(string, List<IEntity>)> entities = new List<(string, List<IEntity>)>();
            ServiceContext serviceContext = GetServiceContext(qBAtuth, props, token);
            DataService service = new DataService(serviceContext);
            Batch batch = service.CreateNewBatch();
            int id = 0;
            if (queries != null)
            {
                foreach (var item in queries)
                {
                    id++;
                    batch.Add(item, "bId" + id);
                }
            }
            try
            {
                if (batch != null && batch.Count > 0)
                {
                    batch.Execute();
                    id = 0;
                    if (queries != null)
                    {
                        foreach (var item in queries)
                        {
                            id++;
                            IntuitBatchResponse Response = batch["bId" + id];
                            if (Response.ResponseType == ResponseType.Exception)
                            {
                                entities.Add((Response.Exception.Message, null));
                            }
                            else if (Response.ResponseType == ResponseType.Query)
                            {
                                if (Response.Entities != null)
                                {
                                    var list = Response.Entities.ToList().Select(o => o).ToList();
                                    entities.Add((string.Empty, list));
                                }
                            }

                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            Last.time = DateTime.UtcNow;

            return entities;
        }
        public async Task<IEntity> PerformOperationOnEntityUpdate(IEntity data, QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token, QBOActionTypeEnum actionType)
        {
            SetCount();

            ServiceContext serviceContext = GetServiceContext(qBAtuth, props, token);
            serviceContext.IppConfiguration.MinorVersion.Qbo = "31";
            serviceContext.IppConfiguration.BaseUrl.Qbo = qBAtuth.Environment == "sandbox" ? "https://sandbox-quickbooks.api.intuit.com/" : "https://quickbooks.api.intuit.com/";
            DataService service = new DataService(serviceContext);
            try
            {
                if (QBOActionTypeEnum.Add == actionType)
                {
                    var res = service.Add(data);

                    Last.time = DateTime.UtcNow;
                    return res;
                }
                else if (QBOActionTypeEnum.Update == actionType)
                {
                    var res = service.Update(data);

                    Last.time = DateTime.UtcNow;
                    return res;
                }
                else if (QBOActionTypeEnum.Delete == actionType)
                {
                    var res = service.Delete(data);

                    Last.time = DateTime.UtcNow;
                    return res;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
        }
    }
}
