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
    
    public partial class guild_type_role_msgs
    {
        public byte ID { get; set; }
        public byte GuildTypeID { get; set; }
        public byte GuildRoleID { get; set; }
        public long MessageID { get; set; }
    
        public virtual guild_roles guild_roles { get; set; }
        public virtual guild_types guild_types { get; set; }
    }
}
