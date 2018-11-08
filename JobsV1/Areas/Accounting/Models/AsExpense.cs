//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobsV1.Areas.Accounting.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AsExpense
    {
        public int Id { get; set; }
        public System.DateTime TrxDate { get; set; }
        public string TrxDesc { get; set; }
        public decimal Amount { get; set; }
        public string TrxRemarks { get; set; }
        public System.DateTime DateEntered { get; set; }
        public int AsCostCenterId { get; set; }
        public int AsExpCategoryId { get; set; }
        public int AsExpBillerId { get; set; }
    
        public virtual AsCostCenter AsCostCenter { get; set; }
        public virtual AsExpCategory AsExpCategory { get; set; }
        public virtual AsExpBiller AsExpBiller { get; set; }
    }
}