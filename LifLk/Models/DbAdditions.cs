using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace LifLk.Models
{
    public class DbAdditions
    {
    }

    public partial class LifDB : DbContext
    {
        public virtual int f_insertNewItemInventory(Nullable<int> inContainerId, Nullable<int> inItemTypeId,
            Nullable<int> inQuality, Nullable<int> inQuantity, Nullable<int> inCreatedDurability, Nullable<int> inDurability,
            string inFeatureText, Nullable<bool> inHaveEffects)
        {
            var inContainerIdParameter = inContainerId.HasValue ?
                new MySqlParameter("inContainerId", inContainerId) :
                new MySqlParameter("inContainerId", typeof(int));
            var inItemTypeIdParameter = inItemTypeId.HasValue ?
                new MySqlParameter("inItemTypeId", inItemTypeId) :
                new MySqlParameter("inItemTypeId", typeof(int));
            var inQualityParameter = inQuality.HasValue ?
                new MySqlParameter("inQuality", inQuality) :
                new MySqlParameter("inQuality", typeof(int));
            var inQuantityParameter = inQuantity.HasValue ?
                new MySqlParameter("inQuantity", inQuantity) :
                new MySqlParameter("inQuantity", typeof(int));
            var inCreatedDurabilityParameter = inCreatedDurability.HasValue ?
                new MySqlParameter("inCreatedDurability", inCreatedDurability) :
                new MySqlParameter("inCreatedDurability", typeof(int));
            var inDurabilityParameter = inDurability.HasValue ?
                new MySqlParameter("inDurability", inDurability) :
                new MySqlParameter("inDurability", typeof(int));
            var inFeatureTextParameter = inFeatureText!=null ?
                new MySqlParameter("inFeatureText", inFeatureText) :
                new MySqlParameter("inFeatureText", "");
            var inHaveEffectsParameter = inHaveEffects.HasValue ?
                new MySqlParameter("inHaveEffects", inHaveEffects) :
                new MySqlParameter("inHaveEffects", typeof(bool));

            return this.Database.SqlQuery<int>("SELECT f_insertNewItemInventory (@inContainerId, @inItemTypeId, @inQuality, @inQuantity, @inCreatedDurability, @inDurability, @inFeatureText, @inHaveEffects)",
                inContainerIdParameter, inItemTypeIdParameter, inQualityParameter, inQuantityParameter,
                inCreatedDurabilityParameter, inDurabilityParameter, inFeatureTextParameter, inHaveEffectsParameter).SingleOrDefault();

            /*return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LifDB.f_insertNewItemInventory",
                inContainerIdParameter, inItemTypeIdParameter, inQualityParameter, inQuantityParameter,
                inCreatedDurabilityParameter, inDurabilityParameter, inFeatureTextParameter, inHaveEffectsParameter);*/
        }
    }
}