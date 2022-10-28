using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.QB
{
    public class QBItemResponseDTO
    {
        public Item Item { get; set; }
        public DateTime time { get; set; }
    }

    public class Item
    {
        public string FullyQualifiedName { get; set; }
        public string domain { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool TrackQtyOnHand { get; set; }
        public int UnitPrice { get; set; }
        public int PurchaseCost { get; set; }
        public int QtyOnHand { get; set; }
        public IncomeAccountRefDTO IncomeAccountRef { get; set; }
        public AssetAccountRefDTO AssetAccountRef { get; set; }
        public bool Taxable { get; set; }
        public bool sparse { get; set; }
        public bool Active { get; set; }
        public string SyncToken { get; set; }
        public string InvStartDate { get; set; }
        public string Type { get; set; }
        public string Description { get; set;}
        public ExpenseAccountRefDTO ExpenseAccountRef { get; set; }
        public MetaData MetaData { get; set; }
    }

    public class MetaData
    {
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }
    }


}

