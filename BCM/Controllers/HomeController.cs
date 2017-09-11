using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCM.Models;
using PagedList;
using System.Data.Entity;
using System.Net;
using System.Data.Entity.SqlServer;

namespace BCM.Controllers
{
    public class HomeController : Controller
    {
        ContactManagerEntities db = new ContactManagerEntities();
        public ActionResult Index(string option, string search, int? pageNumber, string sort)
        {
            ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "";
            ViewBag.SortBycompanyName = sort == "CompanyName" ? "descending companyname" : "CompanyName";

            var records = db.Contacts.AsQueryable();

            if(option == "CompanyName")
            {
                records = records.Where(x => x.CompanyName.Contains(search) || search == null);
            } else if (option == "Surname")
            {
                records = records.Where(x => x.Surname.StartsWith(search) || search == null);
            }
            else
            {
                records = records.Where(x => x.FirstName.StartsWith(search) || search == null);
            }
            switch (sort)
            {
                case "descending name":
                    records = records.OrderByDescending(x => x.FirstName);
                    break;

                case "descending companyname":
                    records = records.OrderByDescending(x => x.CompanyName);
                    break;

                case "CompanyName":
                    records = records.OrderByDescending(x => x.CompanyName);
                    break;

                default:
                    records = records.OrderBy(x => x.FirstName);
                    break;
            }
            return View(records.ToPagedList(pageNumber ?? 1, 10));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Contact newContact)
        {
            if(ModelState.IsValid)
            {
                db.Contacts.Add(newContact);
                db.SaveChanges();

                return RedirectToAction("Index");
                                
            }
            return View(newContact);
        }
        [HttpGet]
        public ActionResult Details()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(Id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }
    }



}