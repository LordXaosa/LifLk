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
    
    public partial class prices
    {
        public long id { get; set; }
        public long objectid { get; set; }
        public long price { get; set; }
    
        public virtual objects_types objects_types { get; set; }
    }
}
