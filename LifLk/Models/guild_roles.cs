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
    
    public partial class guild_roles
    {
        public guild_roles()
        {
            this.character = new HashSet<character>();
            this.guild_actions_queue = new HashSet<guild_actions_queue>();
            this.guild_type_role_msgs = new HashSet<guild_type_role_msgs>();
        }
    
        public byte ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<character> character { get; set; }
        public virtual ICollection<guild_actions_queue> guild_actions_queue { get; set; }
        public virtual ICollection<guild_type_role_msgs> guild_type_role_msgs { get; set; }
    }
}
