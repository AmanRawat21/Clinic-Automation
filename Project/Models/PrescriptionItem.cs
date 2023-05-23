//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PrescriptionItem
    {
        public int PrescriptionItemId { get; set; }
        public int PrescriptionId { get; set; }
        public int DrugId { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public string Instruction { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Drug Drug { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}