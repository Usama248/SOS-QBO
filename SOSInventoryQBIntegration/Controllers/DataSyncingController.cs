using Application.IServices;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace SOSInventoryQBIntegration.Controllers
{
    public class DataSyncingController : Controller {
        private readonly IDataSyncing _dataSyncing;
        public DataSyncingController(IDataSyncing dataSyncing) {
            _dataSyncing = dataSyncing;
        }
        public async Task<JsonResult> SyncData()
        {
           var res = await _dataSyncing.SyncData();
            return Json(new {Data = res});
        }
    }
}
