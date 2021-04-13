using Moodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moodle.Controllers
{
    public class FileController : Controller
    {
        private DbCtx db = new DbCtx();

        // GET: File
        public ActionResult Index()
        {
            List<File> files = db.Files.ToList();
            ViewBag.Files = files;
            return View();
        }

        [Authorize(Roles = "Professor")]
        [HttpPost]
        public ActionResult New(int sectionId, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        Name = System.IO.Path.GetFileName(upload.FileName),
                        UploadDate = DateTime.Now,
                        SectionId = sectionId,
                        Type = upload.ContentType,
                        ProfessorId = db.Sections.Find(sectionId).Course.Professor.ProfessorId
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    db.Files.Add(avatar);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", "Section", new { id = sectionId});
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Professor, Student")]
        [HttpGet]
        public ActionResult Download(int? id)
        {
            if (id.HasValue)
            {
                File file = db.Files.Find(id);
                if (file != null)
                {
                    return File(file.Content, file.Type, file.Name);
                }
                return HttpNotFound("Could not find the file with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing file id parameter!");
        }

        [Authorize(Roles = "Professor")]
        [HttpGet]
        public ActionResult DeleteFile(int? id)
        {
            if (id.HasValue)
            {
                File file = db.Files.Find(id);
                if (file != null)
                {
                    int sectionId = file.SectionId;

                    db.Files.Remove(file);
                    db.SaveChanges();

                    return RedirectToAction("Details", "Section", new { id = sectionId });
                }
                return HttpNotFound("Could not find the file with id " + id.ToString() + "!");
            }
            return HttpNotFound("Missing file id parameter!");
        }
    }
}