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
            // Re-fetch Channels to ensure the dropdown list is populated
            ViewBag.Channels = db.Channels.Select(c => new SelectListItem
            {
                Value = c.ChannelId.ToString(),
                Text = c.ChannelName
            }).ToList();

            // Check if model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if ProgramName already exists for the selected channel
                    var existingProgram = db.Programs
                        .FirstOrDefault(x => x.ProgramName == p.ProgramName && x.ChannelId == p.ChannelId);

                    if (existingProgram != null)
                    {
                        ModelState.AddModelError("ProgramName", "Program already exists under this channel.");
                        return View(p);
                    }

                    var data = new Program()
                    {
                        ProgramName = p.ProgramName,
                        TRPScore = p.TRPScore,
                        ChannelId = p.ChannelId,
                        AirTime = p.AirTime
                    };

                    db.Programs.Add(data);
                    db.SaveChanges();

                    TempData["Success"] = "Program added successfully!";
                    //return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }

            // Return view with validation errors
            return View(p);
        }






        public ActionResult Delete()
        {
            return View();
        }


        




        ///////////////////
        // viewing the channel list

        public static Program Convert(ProgramDTO p)
        {
            return new Program()
            {
                ProgramId = p.ProgramId,
                ProgramName = p.ProgramName,
                TRPScore = p.TRPScore,
                ChannelId = p.ChannelId,
                AirTime = p.AirTime
            };
        }

        public static ProgramDTO Convert(Program p)
        {
            return new ProgramDTO()
            {
                ProgramId = p.ProgramId,
                ProgramName = p.ProgramName,
                TRPScore = p.TRPScore,
                ChannelId = p.ChannelId,
                AirTime = p.AirTime
            };
        }

        public static List<ProgramDTO> Convert(List<Program> data)
        {
            var list = new List<ProgramDTO>();
            foreach (var d in data)
            {
                list.Add(Convert(d));
            }
            return list;
        }
        public ActionResult List()
        {
            var data = db.Programs.ToList();
            return View(Convert(data));
        }





        [HttpGet]
        public ActionResult Edit(int? ProgramId)
        {
            if (ProgramId == null)
            {
                TempData["Error"] = "ProgramId is missing.";
                return RedirectToAction("List");
            }

            var program = db.Programs.Find(ProgramId.Value);
            if (program == null)
            {
                TempData["Error"] = "Program not found.";
                return RedirectToAction("List");
            }

            // Convert to DTO for editing
            var programDto = new ProgramDTO
            {
                ProgramId = program.ProgramId,
                ProgramName = program.ProgramName,
                TRPScore = program.TRPScore,
                ChannelId = program.ChannelId,
                AirTime = program.AirTime
            };

            // Pass list of channels for dropdown
            ViewBag.Channels = new SelectList(db.Channels.ToList(), "ChannelId", "ChannelName", programDto.ChannelId);

            return View(programDto);
        }

        // POST: Edit Program
        [HttpPost]
        public ActionResult Edit(ProgramDTO programDto)
        {
            if (ModelState.IsValid)
            {
                var program = db.Programs.Find(programDto.ProgramId);
                if (program == null)
                {
                    TempData["Error"] = "Program not found.";
                    return RedirectToAction("List");
                }

                // Update program properties
                program.ProgramName = programDto.ProgramName;
                program.TRPScore = programDto.TRPScore;
                program.ChannelId = programDto.ChannelId;
                program.AirTime = programDto.AirTime;

                db.SaveChanges();
                TempData["Success"] = "Program updated successfully!";
                return RedirectToAction("List");
            }

            // Re-populate dropdown if validation fails
            ViewBag.Channels = new SelectList(db.Channels.ToList(), "ChannelId", "ChannelName", programDto.ChannelId);
            return View(programDto);
        }
    }

}