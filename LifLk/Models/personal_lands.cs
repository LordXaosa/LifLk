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
    
    public partial class personal_lands
    {
        public personal_lands()
        {
            this.claims = new HashSet<claims>();
        }
    
        public long ID { get; set; }
        public long CharID { get; set; }
        public string Name { get; set; }
        public long GeoID1 { get; set; }
        public long GeoID2 { get; set; }
        public byte IsTemp { get; set; }
    
        public virtual character character { get; set; }
        public virtual ICollection<claims> claims { get; set; }
    }
}
