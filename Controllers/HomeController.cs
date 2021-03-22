using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StreetASP.Models;
using System.Data.Entity.Infrastructure;
namespace StreetASP.Controllers
{
    public class HomeController : Controller
    {
        MyContext context = new MyContext();
        [HttpGet]
        public ActionResult DisplayAll(string searchString)
        {
            var streets = context.Streets.Include(p => p.Country).ToList();
            if (!String.IsNullOrEmpty(searchString)) // searching
            {
                var findedStreets = streets.Where(s => s.Name.Contains(searchString));
                return View(findedStreets);
            }
            else
                return View(streets);
        }
        /// <summary>
        /// Edit only street name at a time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditStreetName(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Street street = context.Streets.FirstOrDefault(c => c.Id == id);
            EditStreetNameModel esnm = new EditStreetNameModel();
            esnm.StreetId = street.Id;
            esnm.StreetName = street.Name;
            return View(esnm);
        }
        /// <summary>
        /// Edit only country name at a time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCountryName(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Street street = context.Streets.Where(s => s.Id == id).Include(p => p.Country).FirstOrDefault();
            EditCountryNameModel ecnm = new EditCountryNameModel();

            ecnm.StreetId = street.Id;
            ecnm.StreetName = street.Name;
            ecnm.CountryName = street.Country.Name;
            return View(ecnm);
        }
        /// <summary>
        /// Change only street name at a time
        /// </summary>
        /// <param name="esnm"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult EditStreetName(EditStreetNameModel esnm)
        {
            if (ModelState.IsValid)
            {
                //checking if we have such a street in the DB
                Street checkedStreet = context.Streets.FirstOrDefault(s => s.Name == esnm.StreetName);
                if (checkedStreet != null)//we have street with same name in DB - cancel action
                {
                    ModelState.AddModelError("", "We have street with same name in DB .Choose another name");
                    return View(esnm);
                }
                var street = new Street();
                street.Id = esnm.StreetId;
                street.Name = esnm.StreetName;
                context.Entry(street).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("DisplayAll");
            }
            else
            {

            }
            {
                ModelState.AddModelError("", "Errors while entering data");
                return View(esnm);
            }
        }
        /// <summary>
        /// Change only country name at a time
        /// </summary>
        /// <param name="ecnm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCountryName(EditCountryNameModel ecnm)
        {
            if (ModelState.IsValid)
            {
                var street = context.Streets.FirstOrDefault(s => s.Id == ecnm.StreetId);

                var country = IsExistCountry(ecnm.CountryName) ;//checking if we have such a country in the DB 
                if (country != null)//we have such country in DB - update street and database.
                {
                    street.Country = country;
                    context.Entry(country).State = EntityState.Modified;
                    context.Entry(street).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("DisplayAll");
                }
                else// No such country in Database - at first, add country in the DB, and then update street
                {
                    street.Country = AddCountryToDB(ecnm.CountryName);
                    context.Entry(street).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("DisplayAll");
                }
            }
            else
            {
                ModelState.AddModelError("", "Errors while entering data");
                return View(ecnm);
            }
        }
        /// <summary>
        /// Add new Country in DB
        /// </summary>
        /// <param name="inputCountryName"></param>
        /// <returns></returns>
        private Country AddCountryToDB(string inputCountryName)
        {
            Country addedCountry = new Country();
            addedCountry.Name = inputCountryName;
            context.Countries.Add(addedCountry);
            context.SaveChanges();
            return addedCountry;
        }
        [HttpGet]
        public ActionResult Create()
        {
            CreateViewModel cvm = new CreateViewModel();
            return View(cvm);
        }
        [HttpPost]
        public ActionResult Create(CreateViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var checkCountry = IsExistCountry(cvm.CountryName); ///checking if we have such a country in the DB 
                if (checkCountry != null)// We have such Country in the DB - just Update the street
                {
                    var street = new Street { Name = cvm.StreetName, Country = checkCountry };
                    context.Streets.Add(street);
                    context.SaveChanges();
                    return RedirectToAction("DisplayAll");
                }
                else// No such country in the DB - Add Street and Country to the DB
                {

                    var street = new Street { Name = cvm.StreetName, Country = checkCountry };
                    context.Streets.Add(street);
                    context.Countries.Add(checkCountry);
                    context.SaveChanges();
                    return RedirectToAction("DisplayAll");
                }

            }
            else
            {
                ModelState.AddModelError("", "Errors while entering data");
                return View(cvm);
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var street = context.Streets.Where(s => s.Id == id).Include("Country").FirstOrDefault();// check if we have street with this ID in DB
            if (street == null)
            {
                return HttpNotFound();
            }
            return View(street);
        }
        /// <summary>
        /// Confirmation that we really want to delete the record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Street street = context.Streets.Find(id);
            context.Streets.Remove(street);
            context.SaveChanges();
            return RedirectToAction("DisplayAll");
        }
        /// <summary>
        /// Checking if we have such a country in the DB
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns></returns>
        private Country IsExistCountry(string inputName)
        {
            var country = context.Countries.FirstOrDefault(c => c.Name == inputName);
            return country;
        }

    }
}
