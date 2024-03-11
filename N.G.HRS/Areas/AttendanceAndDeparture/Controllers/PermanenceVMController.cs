﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Areas.AttendanceAndDeparture.Models;
using N.G.HRS.Areas.AttendanceAndDeparture.ViewModels;
using N.G.HRS.Date;
using N.G.HRS.Repository;

namespace N.G.HRS.Areas.AttendanceAndDeparture.Controllers
{
    [Area("AttendanceAndDeparture")]

    public class PermanenceVMController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRepository<PermanenceModels> _permanenceModelsRepository;
        private readonly IRepository<Periods> _periodsRepository;
        public PermanenceVMController(AppDbContext appDbContext, IRepository<PermanenceModels> permanenceModelsRepository, IRepository<Periods> periodsRepository)
        {
           this. _appDbContext = appDbContext;
           this._periodsRepository = periodsRepository;
           this._permanenceModelsRepository = permanenceModelsRepository;
            
        }

        public async Task< IActionResult> Index()
        {
            // Populate ViewData for dropdown lists
            await PopulateDropdownListsAsync();
            var periods = await _appDbContext.periods.Include(e=>e.PermanenceModels).ToListAsync();
            var permmenance = await _appDbContext.permanenceModels.ToListAsync();

            var permmenanceVM = new PermanenceModelsAndPeriodsVM
            {
                permanenceModelsList = permmenance,
                 periodsList= periods,
            };

            return View(permmenanceVM);
        }
        public async Task<IActionResult> PemanenceModelsAndPeriods()
        {
            await PopulateDropdownListsAsync();
            var viewModel = new PermanenceModelsAndPeriodsVM();

            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PermanenceModelsAndPeriodsVM PVM )
        {
            try
            {
                await PopulateDropdownListsAsync();

                if (PVM.permanenceModels != null )
                {
                    await _permanenceModelsRepository.AddAsync(PVM.permanenceModels);

                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Add));


                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";

                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة الموظف";
            }

            return View(PVM);
        }
                [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePeriod(PermanenceModelsAndPeriodsVM PVM )
        {
            try
            {
                await PopulateDropdownListsAsync();

                if (PVM.periods != null )
                {
                    await _periodsRepository.AddAsync(PVM.periods);
                    TempData["Success"] = "تم الحفظ بنجاح";
                    return RedirectToAction(nameof(Index));


                }
                else
                {
                    TempData["Error"] = "لم تتم الإضافة، هناك خطأ";

                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                TempData["Error"] = "حدث خطأ أثناء محاولة إضافة الموظف";
            }

            return View(PVM);
        }

        private async Task PopulateDropdownListsAsync()
        {
            var periods = await _periodsRepository.GetAllAsync();
            ViewData["Periods"] = new SelectList(periods, "Id", "PeriodsName");
            var permanance = await _permanenceModelsRepository.GetAllAsync();
            ViewData["permanance"] = new SelectList(permanance, "Id", "PermanenceName");

        }

    }
}
