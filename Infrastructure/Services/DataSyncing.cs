using Application.IServices;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DataSyncing : IDataSyncing
    {
        private readonly ISSOItemsService _SSOItemsService;
        private readonly IQBItemsService _QBItemsService;
        public DataSyncing(ISSOItemsService SSOItemsService, IQBItemsService QBItemsService) {
            _SSOItemsService = SSOItemsService;
            _QBItemsService = QBItemsService;
        }
        public async Task<bool> SyncData()
        {
            var SSOItemList = await _SSOItemsService.getAllSSOItemsAsync();
            try
            {
                //foreach (var item in SSOItemList.data)
                //{
                //    Item request = new Item
                //    {
                //        Name = item.name,
                //        Description = item.description,
                //        Type = Intuit.Ipp.Data.ItemTypeEnum.NonInventory
                //    };
                //    _QBItemsService.CreateItem(request);
                //};
                return true;
            }
            catch (Exception ex) { 
                return true;
            }
        }
    }
}
