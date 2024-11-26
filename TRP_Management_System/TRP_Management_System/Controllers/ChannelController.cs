using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using TRP_Management_System.DTOs;
using TRP_Management_System.EF;

namespace TRP_Management_System.Controllers
{
    public class ChannelController : Controller
    {
        TRP_DBEntities1 db = new TRP_DBEntities1();

        // creating a channel
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ChannelDTO());
        }

        [HttpPost]
        public ActionResult Create(ChannelDTO c)
        {
            if (ModelState.IsValid)
            {
                var data = new Channel()
                {
                    ChannelId = c.ChannelId,
                    ChannelName = c.ChannelName,
                    EstablishedYear = c.EstablishedYear,
                    Country = c.Country
                };
                db.Channels.Add(data);
                db.SaveChanges();

                TempData["Success"] = "Channel created successfully!";
                return RedirectToAction("List");
            }
            return View(c);
        }


      

        [HttpGet]
        public ActionResult Edit(int? ChannelId)
        {
            if (ChannelId == null) // Check if ChannelId is null
            {
                TempData["Error"] = "id not found.";
                return RedirectToAction("List"); // Redirect to List if no ChannelId is provided
            }

            var data = db.Channels.Find(ChannelId.Value); // Find the channel using the provided ID
            if (data == null) // Handle case where the channel is not found
            {
                TempData["Error"] = "Channel not found.";
                return RedirectToAction("List");
            }

            return View(Convert(data)); // Pass the ChannelDTO to the view
        }

        [HttpPost]
        public ActionResult Edit(ChannelDTO c)
        {
            if (ModelState.IsValid)
            {
                var data = db.Channels.Find(c.ChannelId);
                if (data == null)
                {
                    TempData["Error"] = "Channel not found.";
                    return RedirectToAction("List");
                }

                db.Entry(data).CurrentValues.SetValues(c); // Update the channel data
                db.SaveChanges();
                TempData["Success"] = "Channel updated successfully!";
                return RedirectToAction("List");
            }
            return View(c);
        }



        // viewing the channel list

        public static Channel Convert(ChannelDTO c)
        {
            return new Channel()
            {
                ChannelId = c.ChannelId,
                ChannelName = c.ChannelName,
                EstablishedYear = c.EstablishedYear,
                Country = c.Country
            };
        }

        public static ChannelDTO Convert(Channel c)
        {
            return new ChannelDTO()
            {
                ChannelId = c.ChannelId,
                ChannelName = c.ChannelName,
                EstablishedYear = c.EstablishedYear,
                Country = c.Country
            };
        }

        public static List<ChannelDTO> Convert(List<Channel> data)
        {
            var list = new List<ChannelDTO>();
            foreach (var d in data)
            {
                list.Add(Convert(d));
            }
            return list;
        }
        public ActionResult List()
        {
            var data = db.Channels.ToList();
            return View(Convert(data));
        }





        // GET: Delete Channel
        [HttpGet]
        public ActionResult Delete(int? ChannelId)
        {
            if (ChannelId == null)
            {
                TempData["Error"] = "ChannelId is missing.";
                return RedirectToAction("List");
            }

            var channel = db.Channels.Find(ChannelId.Value);
            if (channel == null)
            {
                TempData["Error"] = "Channel not found.";
                return RedirectToAction("List");
            }

            // Check if any associated programs exist
            if (db.Programs.Any(p => p.ChannelId == ChannelId.Value))
            {
                TempData["Error"] = "Cannot delete channel. Associated programs exist.";
                return RedirectToAction("List");
            }

            return View(channel); // Pass the channel entity to the view
        }

        // POST: Delete Channel
        [HttpPost]
        public ActionResult DeleteConfirmed(int ChannelId)
        {
            var channel = db.Channels.Find(ChannelId);
            if (channel == null)
            {
                TempData["Error"] = "Channel not found.";
                return RedirectToAction("List");
            }

            // Check if any associated programs exist
            if (db.Programs.Any(p => p.ChannelId == ChannelId))
            {
                TempData["Error"] = "Cannot delete channel. Associated programs exist.";
                return RedirectToAction("List");
            }

            db.Channels.Remove(channel); // Delete the channel
            db.SaveChanges();

            TempData["Success"] = "Channel deleted successfully!";
            return RedirectToAction("List");
        }

    }
}