using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
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
        public async Task<IActionResult> Index()
        {
            List<Seller> sellersList = await _sellerService.FindAllAsync();

            return View(sellersList);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            Seller? seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }

        // GET: Departments/Create
        public async Task<IActionResult> Create()
        {
            // Will populate the dropdown with the data from DepartmentService:
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id", "Name", "Email", "BirthDate", "BaseSalary", "DepartmentId")] Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department>? departments = await _departmentService.FindAllAsync();
                SellerFormViewModel sellerFormViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(sellerFormViewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {            
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            Seller? seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel sellerFormViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(sellerFormViewModel);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Email", "BirthDate", "BaseSalary", "DepartmentId")] Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department>? departments = await _departmentService.FindAllAsync();
                SellerFormViewModel sellerFormViewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(sellerFormViewModel);
            }
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
            catch (DbConcurrencyException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            Seller? seller = await _sellerService.FindByIdAsync(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(seller);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        // Error:
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}

