using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MyInventory.Data;
using MyInventory.Models;

namespace MyInventory.Controllers
{
    public class SupplierController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Suppliers.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Suppliers record)
        {
            var suppliers = new Suppliers();
            suppliers.CompanyName = record.CompanyName;
            suppliers.Representative = record.Representative;
            suppliers.Code = record.Code;
            suppliers.Address = record.Address;
            suppliers.DateAdded = DateTime.Now;
            suppliers.Type = record.Type;

            _context.Suppliers.Add(suppliers);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var suppliers = _context.Suppliers.Where(s => s.SupplierId == id).SingleOrDefault();
            if (suppliers == null)
            {
                return RedirectToAction("Index");
            }

            return View(suppliers);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Suppliers record)
        {
            var suppliers = _context.Suppliers.Where(s => s.SupplierId == id).SingleOrDefault();
            suppliers.CompanyName = record.CompanyName;
            suppliers.Representative = record.Representative;
            suppliers.Code = record.Code;
            suppliers.Address = record.Address;
            suppliers.DateModified = DateTime.Now;
            suppliers.Type = record.Type;

            _context.Suppliers.Update(suppliers);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var suppliers = _context.Suppliers.Where(s => s.SupplierId == id).SingleOrDefault();
            if (suppliers == null)
            {
                return RedirectToAction("Index");
            }

            _context.Suppliers.Remove(suppliers);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}
