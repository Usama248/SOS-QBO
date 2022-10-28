using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.SOS
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class SOSItem
    {
        public int id { get; set; }
        public int starred { get; set; }
        public int syncToken { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string description { get; set; }
        public string sku { get; set; }
        public object barcode { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public object tags { get; set; }
        public object notes { get; set; }
        public object purchaseDescription { get; set; }
        public object vendorPartNumber { get; set; }
        public object customerPartNumber { get; set; }
        public object vendor { get; set; }
        public object bin { get; set; }
        public object warranty { get; set; }
        public object category { get; set; }
        public object @class { get; set; }
        public object incomeAccount { get; set; }
        public object cogsAccount { get; set; }
        public object assetAccount { get; set; }
        public object expenseAccount { get; set; }
        public double onhand { get; set; }
        public double available { get; set; }
        public double onSO { get; set; }
        public double onSR { get; set; }
        public double rented { get; set; }
        public double onWO { get; set; }
        public double onPO { get; set; }
        public double onRMA { get; set; }
        public object reorderPoint { get; set; }
        public object maxStock { get; set; }
        public object leadTime { get; set; }
        public double salesPrice { get; set; }
        public double baseSalesPrice { get; set; }
        public double markupPercent { get; set; }
        public bool useMarkup { get; set; }
        public double minimumPrice { get; set; }
        public double basePurchaseCost { get; set; }
        public double purchaseCost { get; set; }
        public double costBasis { get; set; }
        public double weight { get; set; }
        public double volume { get; set; }
        public string weightUnit { get; set; }
        public string volumeUnit { get; set; }
        public int sublevel { get; set; }
        public bool taxable { get; set; }
        public object salesTaxCode { get; set; }
        public object purchaseTaxCode { get; set; }
        public bool willSync { get; set; }
        public bool updateShopify { get; set; }
        public bool updateBigCommerce { get; set; }
        public bool alwaysShippable { get; set; }
        public bool hasImage { get; set; }
        public bool serialTracking { get; set; }
        public bool lotTracking { get; set; }
        public bool archived { get; set; }
        public bool showOnSalesForms { get; set; }
        public bool showOnPurchasingForms { get; set; }
        public bool showOnManufacturingForms { get; set; }
        public object customFields { get; set; }
        public List<object> uoms { get; set; }
        public List<object> priceTiers { get; set; }
        public bool summaryOnly { get; set; }
        public object imageAsBase64String { get; set; }
        public bool imageChanged { get; set; }
        public bool hasVariants { get; set; }
        public object variantMaster { get; set; }
        public double commissionRate { get; set; }
        public double commissionAmount { get; set; }
        public bool commissionExempt { get; set; }
        public object syncMessage { get; set; }
        public string lastSync { get; set; }
        public object keys { get; set; }
        public object values { get; set; }
    }

    public class SOSItemResponseDTO
    {
        public int count { get; set; }
        public int totalCount { get; set; }
        public List<SOSItem> data { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }


}
