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
    
    public partial class skills
    {
        public long ID { get; set; }
        public long CharacterID { get; set; }
        public long SkillTypeID { get; set; }
        public long SkillAmount { get; set; }
        public sbyte LockStatus { get; set; }
    
        public virtual character character { get; set; }
        public virtual skill_type skill_type { get; set; }
    }
}
