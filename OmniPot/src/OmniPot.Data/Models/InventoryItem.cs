using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OmniPot.Data.Models
{
    public class InventoryItem : EntityBase
    {
        public Guid InventoryItemId { get; set; }

        public Guid TenantId { get; set; }

        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(144)]
        public string Notes { get; set; }
        public string SalesNotes { get; set; }
        public Guid? ItemTypeId { get; set; }
        public Guid? ItemCategoryId { get; set; }
        public Guid? ItemGroupId { get; set; }
        [ForeignKey("ItemTypeId")]
        public ItemType ItemType { get; set; }
        [ForeignKey("ItemCategoryId")]
        public ItemType ItemCategory { get; set; }
        [ForeignKey("ItemGroupId")]
        public ItemType ItemGroup { get; set; }
        public Guid? UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public Guid TaxGroupId { get; set; }
        [ForeignKey("TaxGroupId")]
        public TaxGroup TaxGroup { get; set; }
        public Guid? WholesaleTaxGroupId { get; set; }
        [ForeignKey("WholesaleTaxGroupId")]
        public TaxGroup WholesaleTaxGroup { get; set; }

        public decimal TraceableQuantity { get; set; }
        public DateTime? ItemExpiryDate { get; set; }

        public Guid? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        public decimal StaticPrice { get; set; }
        public decimal PurchaseCost { get; set; }

        [StringLength(20)]
        public string UpcSku { get; set; }
        //TODO: Pricing groups.

        public ClassificationType ClassificationType { get; set; }

        public IList<UploadDocument> UploadDocuments { get; set; } = new List<UploadDocument>();

        //TODO: InventoryItem has many Batches

    }
}
