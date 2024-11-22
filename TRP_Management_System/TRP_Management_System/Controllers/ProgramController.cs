using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRP_Management_System.DTOs;
using TRP_Management_System.EF;

namespace TRP_Management_System.Controllers
{
    public class ProgramController : Controller
    {
        TRP_DBEntities1 db = new TRP_DBEntities1();

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Channels = db.Channels.Select(c => new SelectListItem
            {
                Value = c.ChannelId.ToString(),
                Text = c.ChannelName
            }).ToList();

            return View(new ProgramDTO());
        }

        [HttpPost]
        public ActionResult Create(ProgramDTO p)
        {
            ViewBag.Channels = db.Channels.Select(c => new SelectListItem
            {
                Value = c.ChannelId.ToString(),
                Text = c.ChannelName
            }).ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    var data = new Program()
                    {
                        ProgramName = p.ProgramName,
                        TRPScore = p.TRPScore,
                        ChannelId = p.ChannelId,
                        AirTime = p.AirTime
                    };

                    db.Programs.Add(data);
                    db.SaveChanges();

                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }

            return View(p);
        }



        public ActionResult Delete()
        {
            return View();
        }


        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

    }
}