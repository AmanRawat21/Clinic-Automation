using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        Models.ProjectIBMEntities _db=new Models.ProjectIBMEntities();  
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Doctor
        public ActionResult ListDoctor()
        {
            return View(_db.Doctors.Include("User").ToList());

        }
        
        public ActionResult AddNewDoctor()
        {
            return View();
        }




        [HttpPost]
        public ActionResult AddNewDoctor(Models.Doctor doctor)
        {
            Models.User user = new Models.User { UserName = "Doc" + DateTime.Now.ToString("ddmmyyyyhhmmss"), UserPassword = DateTime.Now.ToString("ddmmyyyyhhmmss"), UserRole = "Doctor", LoginStatus = "ACTIVE" };

            doctor.User = user;
            doctor.DoctorStatus = "ACTIVE";
            _db.Doctors.Add(doctor);
            
            _db.SaveChanges();






            return RedirectToAction("Index", "Admin");

        }


        // GET: Patient
        public ActionResult ListPatient()
        {
            return View(_db.Patients.ToList());

        }

        public ActionResult AddNewPatient()
        {
            return View();
        }




        [HttpPost]
        public ActionResult AddNewPatient(Models.Patient patient)
        {
            Models.User user = new Models.User { UserName = "Pat" + DateTime.Now.ToString("ddmmyyyyhhmmss"), UserPassword = DateTime.Now.ToString("ddmmyyyyhhmmss"), UserRole = "Patient", LoginStatus = "ACTIVE" };

            patient.User = user;
            _db.Patients.Add(patient);

            _db.SaveChanges();






            return RedirectToAction("Index", "Admin");

        }

        // GET: Supplier

        public ActionResult ListSupplier()
        {
            return View(_db.Suppliers.ToList());

        }

        public ActionResult AddNewSupplier()
        {
            return View();
        }




        [HttpPost]
        public ActionResult AddNewSupplier(Models.Supplier supplier)
        {
            Models.User user = new Models.User { UserName = "Sup" + DateTime.Now.ToString("ddmmyyyyhhmmss"), UserPassword = DateTime.Now.ToString("ddmmyyyyhhmmss"), UserRole = "Supplier", LoginStatus = "ACTIVE" };

            supplier.User = user;
            _db.Suppliers.Add(supplier);

            _db.SaveChanges();






            return RedirectToAction("Index", "Admin");

        }


        //Get: Salesman

        public ActionResult ListSalesman()
        {
            return View(_db.Salesmen.ToList());

        }

        public ActionResult AddNewSalesman()
        {
            return View();
        }




        [HttpPost]
        public ActionResult AddNewSalesman(Models.Salesman salesman)
        {
            Models.User user = new Models.User { UserName = "Sal" + DateTime.Now.ToString("ddmmyyyyhhmmss"), UserPassword = DateTime.Now.ToString("ddmmyyyyhhmmss"), UserRole = "Salesman", LoginStatus = "ACTIVE" };

            salesman.User = user;
            _db.Salesmen.Add(salesman);

            _db.SaveChanges();






            return RedirectToAction("Index", "Admin");

        }

    }
}