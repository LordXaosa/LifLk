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
    
    public partial class guild_types
    {
        public guild_types()
        {
            this.guild_type_role_msgs = new HashSet<guild_type_role_msgs>();
            this.guilds = new HashSet<guilds>();
        }
    
        public byte ID { get; set; }
        public int GuildLevel { get; set; }
        public string Name { get; set; }
        public long MessageID { get; set; }
    
        public virtual ICollection<guild_type_role_msgs> guild_type_role_msgs { get; set; }
        public virtual ICollection<guilds> guilds { get; set; }
    }
}
