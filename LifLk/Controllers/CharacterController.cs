using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LifLk.Models;

namespace LifLk.Controllers
{
    public class CharacterController : Controller
    {
        private LifDB db = new LifDB();

        // GET: /Character/
        public async Task<ActionResult> Index(long? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            character character = db.character.Where(p=>p.ID==id).Include(p=>p.account).Include(p=>p.containers).Include(p=>p.containers1).Include(p=>p.equipment_slots).FirstOrDefault();
            if (character == null)
            {
                return HttpNotFound();
            }
            if (Session["userSteamId"] == null)
            {
                Session["charId"] = null;
                Session["userSteamId"] = null;
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            decimal steamId = decimal.Parse(Session["userSteamId"].ToString());
            Session["charId"] = id.Value;
            if (steamId != character.account.SteamID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(character);

        }



        // GET: /Character/Edit/5
        /*public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            character character = await db.character.FindAsync(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.account, "ID", "ID", character.AccountID);
            ViewBag.EquipmentContainerID = new SelectList(db.containers, "ID", "ID", character.EquipmentContainerID);
            ViewBag.GuildRoleID = new SelectList(db.guild_roles, "ID", "Name", character.GuildRoleID);
            ViewBag.GuildID = new SelectList(db.guilds, "ID", "Name", character.GuildID);
            ViewBag.RootContainerID = new SelectList(db.containers, "ID", "ID", character.RootContainerID);
            return View(character);
        }*/

        // POST: /Character/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,IsActive,Name,LastName,GeoID,AccountID,GeoAlt,RaceID,Alignment,CriminalSecondsLeft,Strength,StrengthLock,Agility,AgilityLock,Intellect,IntellectLock,Willpower,WillpowerLock,Constitution,ConstitutionLock,RootContainerID,EquipmentContainerID,HardHP,HardStam,SoftHP,SoftStam,Luck,HungerRate,AlchemyHash,VisibilityHash,appearance,GuildID,GuildRoleID,TitleMessageID,BindedObjectID,RallyObjectID,LastTimeUsedPraiseYourGodAbility,LastTimeUsedTransmuteIntoGold,CreateTimestamp,DeleteTimestamp")] character character)
        {
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.account, "ID", "ID", character.AccountID);
            ViewBag.EquipmentContainerID = new SelectList(db.containers, "ID", "ID", character.EquipmentContainerID);
            ViewBag.GuildRoleID = new SelectList(db.guild_roles, "ID", "Name", character.GuildRoleID);
            ViewBag.GuildID = new SelectList(db.guilds, "ID", "Name", character.GuildID);
            ViewBag.RootContainerID = new SelectList(db.containers, "ID", "ID", character.RootContainerID);
            return View(character);
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
