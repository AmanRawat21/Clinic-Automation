using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private ProjectIBMEntities db = new ProjectIBMEntities();

        // GET: Doctor
        public ActionResult Index()
        {
            var patients = db.Patients.Include(p => p.User);
            return View(patients.ToList());
        }

        public ActionResult Inventory()
        {
            return View(db.Drugs.ToList());
        }

        public ActionResult CreatePurchaseOrder()

        {
            ViewBag.Supplier = db.Suppliers.Select(s => new { s.SupplierId, SupplierName = s.SupplierFirstName + " " + s.SupplierLastName });

            ViewBag.Drugs = db.Drugs.Select(s => new { s.DrugId, s.DrugName });

            ViewBag.Salesman = db.Salesmen.Select(s => new { s.SalesmanId, SalesmanName = s.SalesmanFirstName + " " + s.SalesmanLastName });



            return View();
        }

        [HttpPost]
        public ActionResult CreatePurchaseOrder(OrderHeaderViewModel order, List<OrderItemViewModel> Orderitems)

        {

            Order newOrder = new Order { OrderDate = order.OrderDate, OrderDescription = order.OrderDesc };


            newOrder.Supplier = db.Suppliers.Find(order.Supplier);

            newOrder.Salesman = db.Salesmen.Find(order.Salesman);

            foreach (OrderItemViewModel item in Orderitems)
            {
                var ord = new Models.OrderItem { Quantity = item.Quantity };
                ord.Drug = db.Drugs.Find(item.DrugId);
                newOrder.OrderItems.Add(ord);

            }


            db.Orders.Add(newOrder);


            db.SaveChanges();



            return RedirectToAction("Index", "Doctor");
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,PatientFirstName,PatientLastName,PatientDescription,PatientEmail,PatientPhone,PatientBloodGroup,UserId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", patient.UserId);
            return View(patient);
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", patient.UserId);
            return View(patient);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientId,PatientFirstName,PatientLastName,PatientDescription,PatientEmail,PatientPhone,PatientBloodGroup,UserId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", patient.UserId);
            return View(patient);
        }

        // GET: Doctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
