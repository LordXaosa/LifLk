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
    
    public partial class features
    {
        public features()
        {
            this.containers = new HashSet<containers>();
            this.items = new HashSet<items>();
        }
    
        public long ID { get; set; }
        public Nullable<long> CustomtextID { get; set; }
        public bool has_effects { get; set; }
    
        public virtual ICollection<containers> containers { get; set; }
        public virtual custom_texts custom_texts { get; set; }
        public virtual ICollection<items> items { get; set; }
    }
}
