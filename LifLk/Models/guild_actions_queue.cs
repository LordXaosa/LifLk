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
    
    public partial class guild_actions_queue
    {
        public long ID { get; set; }
        public decimal TicketID { get; set; }
        public string ActionType { get; set; }
        public long ProducerCharID { get; set; }
        public Nullable<long> GuildID { get; set; }
        public Nullable<long> CharID { get; set; }
        public string GuildName { get; set; }
        public string GuildTag { get; set; }
        public string GuildCharter { get; set; }
        public Nullable<byte> GuildTypeID { get; set; }
        public byte CharIsKicked { get; set; }
        public Nullable<byte> CharGuildRoleID { get; set; }
        public Nullable<long> OtherGuildID { get; set; }
        public Nullable<byte> StandingTypeID { get; set; }
        public System.DateTime AddedTimestamp { get; set; }
        public long OwnerConnectionID { get; set; }
        public System.DateTime OwnedTimestamp { get; set; }
    
        public virtual character character { get; set; }
        public virtual character character1 { get; set; }
        public virtual guild_roles guild_roles { get; set; }
        public virtual guild_standing_types guild_standing_types { get; set; }
        public virtual guild_types guild_types { get; set; }
        public virtual guilds guilds { get; set; }
        public virtual guilds guilds1 { get; set; }
    }
}