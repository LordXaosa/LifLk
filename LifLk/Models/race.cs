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
    
    public partial class race
    {
        public race()
        {
            this.character = new HashSet<character>();
        }
    
        public byte ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<character> character { get; set; }
    }
}