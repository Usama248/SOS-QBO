using Common.DTOs.QB.Auth;
using Common.Enums.QB;
using Intuit.Ipp.Data;

namespace Infrastructure.QB
{
    public interface IQBConnectionService
    {
        Task<Tuple<string, List<T>>> GetQueryResponse<T>(QBAuthDTO qBAtuth, QBTokenPropertiesDTO data, string token, string query);
        Task<List<(string message, List<IEntity> entity)>> PerformBatchOperationOnEntities(QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token, List<string> queries);
        Task<List<(string message, IEntity? entity)>> PerformBatchOperationOnEntity(QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token, List<(IEntity entity, OperationEnum operation)> batchItemRequests, List<string> queries);
        Task<IEntity> PerformOperationOnEntity(IEntity data, QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token, QBOActionTypeEnum actionType);
        Task<IEntity> PerformOperationOnEntityUpdate(IEntity data, QBAuthDTO qBAtuth, QBTokenPropertiesDTO props, string token, QBOActionTypeEnum actionType);
    }
}
