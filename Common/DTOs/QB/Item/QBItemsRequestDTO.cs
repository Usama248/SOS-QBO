using Common.DTOs.QB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class QBItemsRequestDTO
    {
        public bool TrackQtyOnHand { get; set; }
        public string Name { get; set; }
        public int QtyOnHand { get; set; }
        public IncomeAccountRefDTO IncomeAccountRef { get; set; }
        public AssetAccountRefDTO AssetAccountRef { get; set; }
        public string InvStartDate { get; set; }
        public string Type { get; set; }
        public ExpenseAccountRefDTO ExpenseAccountRef { get; set; }
    }


}
