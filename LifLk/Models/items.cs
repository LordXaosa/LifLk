//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LifLk.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class items
    {
        public items()
        {
            this.equipment_slots = new HashSet<equipment_slots>();
        }
    
        public long ID { get; set; }
        public long ContainerID { get; set; }
        public long ObjectTypeID { get; set; }
        public byte Quality { get; set; }
        public long Quantity { get; set; }
        public int Durability { get; set; }
        public int CreatedDurability { get; set; }
        public Nullable<long> FeatureID { get; set; }
    
        public virtual containers containers { get; set; }
        public virtual ICollection<equipment_slots> equipment_slots { get; set; }
        public virtual features features { get; set; }
        public virtual objects_types objects_types { get; set; }
    }
}
