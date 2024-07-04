using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly ISellerService _sellerService;
        private readonly IDepartmentService _departmentService;

        public SellersController(ISellerService sellerService, IDepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Seller> sellersList = _sellerService.FindAll();

            return View(sellersList);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            // Will populate the dropdown with the data from DepartmentService:
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (ModelState.IsValid)
            {
                _sellerService.Insert(seller);
                return RedirectToAction(nameof(Index));
            }

            return View(seller);
        }
    }
}

