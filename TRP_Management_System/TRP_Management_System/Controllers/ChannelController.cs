using System;
using System.Collections.Generic;
using System.Linq;
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
                return RedirectToAction("List");
            }
            return View(c);
        }


        public ActionResult Delete()
        {
            return View();
        }


        public ActionResult Edit()
        {
            return View();
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
    }
}