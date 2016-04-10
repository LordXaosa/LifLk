using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LifLk.Models;

namespace LifLk.Controllers
{
    public class HomeController : Controller
    {
        LifDB db = new LifDB();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //ViewBag.Message = "Your application description page.";
                if (Session["userSteamId"] == null)
                {
                    HttpContext.GetOwinContext().Authentication.SignOut();
                    Session["charId"] = null;
                    Session["userSteamId"] = null;
                    return RedirectToAction("Index", "Home");
                }
                decimal steamId = decimal.Parse(Session["userSteamId"].ToString());
                account acc = db.account.Where(p => p.SteamID == steamId).Include(p=>p.character).FirstOrDefault();
                if (acc == null)
                {
                    HttpContext.GetOwinContext().Authentication.SignOut();
                    Session["charId"] = null;
                    Session["userSteamId"] = null;
                    return View();
                }
                return View(acc);
            }
            return View();
        }

        public ActionResult Buy()
        {
            if (Session["userSteamId"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                Session["charId"] = null;
                Session["userSteamId"] = null;
                return RedirectToAction("Index", "Home");
            }
            if (Session["charId"] == null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                Session["charId"] = null;
                Session["userSteamId"] = null;
                return RedirectToAction("Index", "Home");
            }
            List<prices> prices = db.prices.Where(p=>p.objects_types.FaceImage.Contains("Items")).Include(p=>p.objects_types).ToList();
            return View(prices.Select(p => new BuyItemModel()
            {
                ObjectsTypes = p.objects_types,
                Price = p.price,
                ObjectId = p.objectid
            }).ToList());
        }

        public ActionResult BuyItem(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            BuyItemModel model = new BuyItemModel();
            model.ObjectsTypes = db.objects_types.FirstOrDefault(p => p.ID == id);
            //model.Price = model.ObjectsTypes.prices.FirstOrDefault().price;
            model.ObjectId = model.ObjectsTypes.ID;
            if (Session["charId"] == null)
            {
                AddLog(string.Format("[BUY][PRE][ERROR]No charId. Item id:{0} quantity: {1} price: {2}", model.ObjectId, model.Quantity, model.Price));
                return RedirectToAction("Index", "Home");
            }
            long charId = long.Parse(Session["charId"].ToString());
            
            character ch = db.character.Find(charId);
            if (ch == null)
            {
                AddLog(string.Format("[BUY][PRE][ERROR]Bad char. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prices price = model.ObjectsTypes.prices.FirstOrDefault();
            if (price == null)
            {
                AddLog(string.Format("[BUY][PRE][ERROR]No price. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lifdscp_online_character online = db.lifdscp_online_character.FirstOrDefault(p => p.CharacterID == charId);
            if (online != null)
            {
                ModelState.AddModelError("", "Персонаж должен быть не в сети!");
                AddLog(string.Format("[BUY][PRE][ERROR]Char online. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                //return RedirectToAction("Buy", "Home");
                return View(model);
            }
            AddLog(string.Format("[BUY][PRE][SUCCESS]Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
            return View(model);
        }

        [HttpPost]
        public ActionResult BuyItem(BuyItemModel model)
        {
            if (Session["charId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (model.ObjectsTypes == null)
            {
                model.ObjectsTypes = db.objects_types.FirstOrDefault(p => p.ID == model.ObjectId);
            }
            if (!ModelState.IsValid)
            {
                //return View(model);
                return BuyItem((int)model.ObjectsTypes.ID);
            }
            long charId = long.Parse(Session["charId"].ToString());
            lifdscp_online_character online = db.lifdscp_online_character.FirstOrDefault(p => p.CharacterID == charId);
            if (online != null)
            {
                ModelState.AddModelError("", "Персонаж должен быть не в сети!");
                AddLog(string.Format("[BUY][ERROR]Char online. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                return BuyItem((int)model.ObjectsTypes.ID);
                //return View(model);
            }
            character ch = db.character.Find(charId);
            if (ch == null)
            {
                AddLog(string.Format("[BUY][ERROR]Bad char. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prices price = model.ObjectsTypes.prices.FirstOrDefault();
            if (price == null)
            {
                AddLog(string.Format("[BUY][ERROR]No price. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            double charWeight = 0.0d;
            foreach (items item in ch.containers.items)
            {
                charWeight += (double)(item.objects_types.UnitWeight*item.Quantity)/1000.0d;
            }
            foreach (items item in ch.containers1.items)
            {
                charWeight += (double)(item.objects_types.UnitWeight * item.Quantity) / 1000.0d;
            }
            double afterWeight = (double)(charWeight + ((model.ObjectsTypes.UnitWeight*model.Quantity)/1000.0d));
            double maxWeight = (((ch.Willpower/1000000.0d)*2) + 100);
            if (afterWeight >= (maxWeight*2)-1)
            {
                ModelState.AddModelError("", "Недостаточно места в инвентаре!");
                AddLog(string.Format("[BUY][ERROR]No place in inventory. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                return BuyItem((int)model.ObjectsTypes.ID);
            }
            
            List<items> coins = ch.containers1.items.Where(
                                        p => p.ObjectTypeID == 1059 || p.ObjectTypeID == 1060 || p.ObjectTypeID == 1061)
                                    .ToList();
            decimal sum = coins.Sum(p =>
            {
                if (p.ObjectTypeID == 1059)
                {
                    return 100 * p.Quantity;
                }
                else if (p.ObjectTypeID == 1060)
                {
                    return 100 * 1000 * p.Quantity;
                }
                else if (p.ObjectTypeID == 1061)
                {
                    return 100 * 1000 * 1000 * p.Quantity;
                }
                return 0;
            });
            decimal finalPrice = price.price * (1.0m + ((model.Quality - 50.0m) / 100.0m));
            model.Price = finalPrice * model.Quantity;
            if (model.Price < 100)
            {
                model.Price = 100;
            }
            if (sum < model.Price)
            {
                //error
                ModelState.AddModelError("", "Недостаточно средств!");
                AddLog(string.Format("[BUY][ERROR]Not enough money. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                //return BuyItem((int)model.ObjectsTypes.ID);;
                return View(model);
            }
            else
            {
                if (!model.ConfirmBuy)
                {
                    ModelState.AddModelError("", "Нажмите еще раз для подтверждения покупки!");
                    model.ConfirmBuy = true;
                    AddLog(string.Format("[BUY][PRE]Confirmation. Item id:{0} quantity: {1} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId));
                    return View(model);
                }
                sum = sum - model.Price;
                //sum = sum/100;

                long goldPrice = (long)(sum / 1000 / 1000 / 100);
                long silverPrice = (long)((sum - goldPrice * 1000 * 1000 * 100) / 1000 / 100);
                long copperPrice = (long)((sum % 100000.0m)) / 100;

                
                if (copperPrice > 0)
                    db.f_insertNewItemInventory((int)ch.RootContainerID, 1059, 50, (int)copperPrice, 0, 0, null, false);
                if (silverPrice > 0)
                db.f_insertNewItemInventory((int)ch.RootContainerID, 1060, 50, (int)silverPrice, 0, 0, null, false);
                if (goldPrice > 0) 
                    db.f_insertNewItemInventory((int)ch.RootContainerID, 1061, 50, (int)goldPrice, 0, 0, null, false);
                db.f_insertNewItemInventory((int)ch.RootContainerID, (int)model.ObjectsTypes.ID, model.Quality, model.Quantity, 10000, 10000, null, false);
                foreach (items coin in coins)
                {
                    db.f_deleteItem((int)coin.ID);
                }
                AddLog(string.Format("[BUY][SUCCESS]Item id:{0} quantity: {1} quality: {4} price: {2} char: {3}", model.ObjectId, model.Quantity, model.Price, charId, model.Quality));
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Sell(int? itemId)
        {
            if (itemId == null)
            {
                AddLog(string.Format("[SELL][PRE][ERROR]No item id."));
                return RedirectToAction("Index", "Home");
            }
            if (Session["charId"] == null)
            {
                AddLog(string.Format("[SELL][PRE][ERROR]No char id."));
                return RedirectToAction("Index", "Home");
            }
            long charId = long.Parse(Session["charId"].ToString());
            character ch = db.character.Find(charId);
            if (ch == null)
            {
                AddLog(string.Format("[SELL][PRE][ERROR]Bad char id. char: {0}", charId));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellItemModel model = new SellItemModel();
            model.ItemId = itemId.Value;
            model.Item = db.items.FirstOrDefault(p => p.ID == itemId.Value);
            lifdscp_online_character online = db.lifdscp_online_character.FirstOrDefault(p => p.CharacterID == charId);
            if (online != null)
            {
                ModelState.AddModelError("", "Персонаж должен быть не в сети!");
                AddLog(string.Format("[SELL][PRE][ERROR]Char online. char: {0}", charId));
                //return Sell(itemId);
                return View(model);
            }
            prices price = model.Item.objects_types.prices.FirstOrDefault();
            if (price == null)
            {
                AddLog(string.Format("[SELL][PRE][ERROR]No price. Item id:{0} quantity: {1} price: {2} char: {3}", model.Item.ObjectTypeID, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ch.RootContainerID != model.Item.ContainerID)
            {
                AddLog(string.Format("[SELL][PRE][ERROR]You are not owner!. Item id:{0} quantity: {1} price: {2} char: {3}", model.Item.ObjectTypeID, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            AddLog(string.Format("[SELL][PRE][SUCCESS]Item id:{0} quantity: {1} price: {2} char: {3}", model.Item.ObjectTypeID, model.Quantity, model.Price, charId));
            return View(model);
        }
        [HttpPost]
        public ActionResult Sell(SellItemModel model)
        {
            if (Session["charId"] == null)
            {
                AddLog(string.Format("[SELL][ERROR]No char id. Item id:{0} quantity: {1} price: {2}", model.ItemId, model.Quantity, model.Price));
                return RedirectToAction("Index", "Home");
            }
            long charId = long.Parse(Session["charId"].ToString());
            lifdscp_online_character online = db.lifdscp_online_character.FirstOrDefault(p => p.CharacterID == charId);
            if (online != null)
            {
                ModelState.AddModelError("", "Персонаж должен быть не в сети!");
                AddLog(string.Format("[SELL][ERROR]Char online. Item id:{0} quantity: {1} price: {2} char: {3}", model.ItemId, model.Quantity, model.Price, charId));
                return Sell(model.ItemId);
                //return View(model);
            }
            character ch = db.character.Find(charId);
            if (ch == null)
            {
                AddLog(string.Format("[SELL][ERROR]Bad char. Item id:{0} quantity: {1} price: {2} char: {3}", model.ItemId, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (model.Item == null)
            {
                model.Item = db.items.FirstOrDefault(p => p.ID == model.ItemId);
            }
            prices price = model.Item.objects_types.prices.FirstOrDefault();
            if (price == null)
            {
                AddLog(string.Format("[SELL][ERROR]No price. Item id:{0} quantity: {1} price: {2} char: {3}", model.Item.ObjectTypeID, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (ch.RootContainerID != model.Item.ContainerID)
            {
                AddLog(string.Format("[SELL][ERROR]You are not owner! Item id:{0} quantity: {1} price: {2} char: {3}", model.ItemId, model.Quantity, model.Price, charId));
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            List<items> coins =
                                ch.containers1.items.Where(
                                        p => p.ObjectTypeID == 1059 || p.ObjectTypeID == 1060 || p.ObjectTypeID == 1061)
                                    .ToList();
            decimal sum = coins.Sum(p =>
            {
                if (p.ObjectTypeID == 1059)
                {
                    return 100 * p.Quantity;
                }
                else if (p.ObjectTypeID == 1060)
                {
                    return 100 * 1000 * p.Quantity;
                }
                else if (p.ObjectTypeID == 1061)
                {
                    return 100 * 1000 * 1000 * p.Quantity;
                }
                return 0;
            });
            decimal finalPrice = price.price * (1.0m + ((model.Item.Quality - 50.0m) / 100.0m));
            model.Price = finalPrice * model.Quantity;
            model.Price = model.Price / 4;
            model.Price = Math.Truncate(model.Price);
            if (!model.Confirm)
            {
                model.Confirm = true;
                ModelState.AddModelError("","Нажмите еще раз для подтверждения продажи");
                AddLog(string.Format("[SELL][PRE]Item id:{0} quantity: {1} price: {2} char: {3}", model.Item.ObjectTypeID, model.Quantity, model.Price, charId));
                return View(model);
            }
            else
            {
                if (model.Quantity > model.Item.Quantity)
                {
                    ModelState.AddModelError("", "У вас нет столько вещей в наличии!");
                    model.Confirm = false;
                    AddLog(string.Format("[SELL][ERROR]Not enough items in inventory. Item id:{0} quantity: {1} price: {2} char: {3}", model.ItemId, model.Quantity, model.Price,charId));
                    return View(model);
                }
                sum += model.Price;
                long goldPrice = (long)(sum / 1000 / 1000 / 100);
                long silverPrice = (long)((sum - goldPrice * 1000 * 1000 * 100) / 1000 / 100);
                long copperPrice = (long)((sum % 100000.0m)) / 100;
                foreach (items coin in coins)
                {
                    db.f_deleteItem((int)coin.ID);
                }
                if (copperPrice > 0)
                    db.f_insertNewItemInventory((int)ch.RootContainerID, 1059, 50, (int)copperPrice, 0, 0, null, false);
                if (silverPrice > 0)
                    db.f_insertNewItemInventory((int)ch.RootContainerID, 1060, 50, (int)silverPrice, 0, 0, null, false);
                if (goldPrice > 0)
                    db.f_insertNewItemInventory((int)ch.RootContainerID, 1061, 50, (int)goldPrice, 0, 0, null, false);
                
                if (model.Quantity == model.Item.Quantity)
                {
                    db.f_deleteItem(model.ItemId);
                    AddLog(string.Format("[SELL][SUCCESS]Item id:{0} quantity: {1} (all) quality: {4} price: {2} char: {3}", model.Item.ObjectTypeID, model.Quantity, model.Price, charId, model.Item.Quality));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    items item = db.items.Find(model.ItemId);
                    item.Quantity -= model.Quantity;
                    db.SaveChanges();
                    AddLog(string.Format("[SELL][SUCCESS]Item id:{0} quantity: {1} quality: {4} price: {2} char: {3}", model.Item.ObjectTypeID, model.Quantity, model.Price, charId, model.Item.Quality));
                    return RedirectToAction("Index", "Home");
                }
            }
           
        }

        private void AddLog(string message)
        {
            logs log = new logs();
            log.message = message;
            if (Session["userSteamId"] != null)
            {
                log.steamid = long.Parse(Session["userSteamId"].ToString());
            }
            log.ip = HttpContext.Request.UserHostAddress;
            log.date = DateTime.Now;
            db.logs.Add(log);
            db.SaveChanges();
        }
    }
}