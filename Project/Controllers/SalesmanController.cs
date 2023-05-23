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
    public class SalesmanController : Controller
    {
        private ProjectIBMEntities db = new ProjectIBMEntities();

        // GET: Salesman
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Salesman).Include(o => o.Supplier);
            return View(orders.ToList());
        }

        // GET: Salesman/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Salesman/Create
        public ActionResult Create()
        {
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanFirstName");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierFirstName");
            return View();
        }

        // POST: Salesman/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,OrderDescription,SupplierId,SalesmanId,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanFirstName", order.SalesmanId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierFirstName", order.SupplierId);
            return View(order);
        }

        // GET: Salesman/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanFirstName", order.SalesmanId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierFirstName", order.SupplierId);
            return View(order);
        }

        // POST: Salesman/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderDescription,SupplierId,SalesmanId,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalesmanId = new SelectList(db.Salesmen, "SalesmanId", "SalesmanFirstName", order.SalesmanId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierFirstName", order.SupplierId);
            return View(order);
        }

        // GET: Salesman/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Salesman/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
