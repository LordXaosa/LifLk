﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LifDB : DbContext
    {
        public LifDB()
            : base("name=LifDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<account> account { get; set; }
        public virtual DbSet<character> character { get; set; }
        public virtual DbSet<character_effects> character_effects { get; set; }
        public virtual DbSet<character_titles> character_titles { get; set; }
        public virtual DbSet<chars_deathlog> chars_deathlog { get; set; }
        public virtual DbSet<claims> claims { get; set; }
        public virtual DbSet<containers> containers { get; set; }
        public virtual DbSet<custom_texts> custom_texts { get; set; }
        public virtual DbSet<effects> effects { get; set; }
        public virtual DbSet<effects_sets> effects_sets { get; set; }
        public virtual DbSet<equipment_slots> equipment_slots { get; set; }
        public virtual DbSet<features> features { get; set; }
        public virtual DbSet<guild_actions_processed> guild_actions_processed { get; set; }
        public virtual DbSet<guild_actions_queue> guild_actions_queue { get; set; }
        public virtual DbSet<guild_lands> guild_lands { get; set; }
        public virtual DbSet<guild_roles> guild_roles { get; set; }
        public virtual DbSet<guild_standing_types> guild_standing_types { get; set; }
        public virtual DbSet<guild_standings> guild_standings { get; set; }
        public virtual DbSet<guild_type_role_msgs> guild_type_role_msgs { get; set; }
        public virtual DbSet<guild_types> guild_types { get; set; }
        public virtual DbSet<guilds> guilds { get; set; }
        public virtual DbSet<item_effects> item_effects { get; set; }
        public virtual DbSet<items> items { get; set; }
        public virtual DbSet<lifdscp_online_character> lifdscp_online_character { get; set; }
        public virtual DbSet<lifdscp_online_history> lifdscp_online_history { get; set; }
        public virtual DbSet<objects_types> objects_types { get; set; }
        public virtual DbSet<personal_lands> personal_lands { get; set; }
        public virtual DbSet<prices> prices { get; set; }
        public virtual DbSet<race> race { get; set; }
        public virtual DbSet<roles> roles { get; set; }
        public virtual DbSet<skill_type> skill_type { get; set; }
        public virtual DbSet<skills> skills { get; set; }
        public virtual DbSet<titles> titles { get; set; }
        public virtual DbSet<logs> logs { get; set; }
    
        public virtual int f_deleteItem(Nullable<int> inItemID)
        {
            var inItemIDParameter = inItemID.HasValue ?
                new ObjectParameter("inItemID", inItemID) :
                new ObjectParameter("inItemID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f_deleteItem", inItemIDParameter);
        }
    
        public virtual int f_fromGeoID(Nullable<int> geoID)
        {
            var geoIDParameter = geoID.HasValue ?
                new ObjectParameter("geoID", geoID) :
                new ObjectParameter("geoID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f_fromGeoID", geoIDParameter);
        }
    }
}
