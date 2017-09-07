using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCM.Models;
using PagedList;

namespace BCM.Controllers
{
    public class HomeController : Controller
    {
        ContactManagerEntities db = new ContactManagerEntities();
        public ActionResult Index(string option, string search, int? pageNumber)
        {
            if(option == "CompanyName")
            {
                return View(db.Contacts.Where(x => x.CompanyName.Contains(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 10));
            } else if (option == "Surname")
            {
                return View(db.Contacts.Where(x => x.Surname.StartsWith(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 10));
            }
            else
            {
                return View(db.Contacts.Where(x => x.FirstName.StartsWith(search) || search == null).ToList().ToPagedList(pageNumber ?? 1, 10));
            }
        }
    }
}