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
    
    public partial class custom_texts
    {
        public custom_texts()
        {
            this.features = new HashSet<features>();
        }
    
        public long ID { get; set; }
        public string Custom_text { get; set; }
    
        public virtual ICollection<features> features { get; set; }
    }
}
