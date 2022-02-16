using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Cars
{
    public class EditModel : CarCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public EditModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await _context.Car
                .Include(b => b.Dealer)
                .Include(b => b.Fuel)
                .Include(b => b.CarCategories).ThenInclude(b => b.Category)
                .Include(b => b.CarGadgets).ThenInclude(b => b.Gadget)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Car == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Car);
            PopulateAssignedGadgetData(_context, Car);
            ViewData["DealerID"] = new SelectList(_context.Set<Dealer>(), "ID", "DealerName");
            ViewData["FuelID"] = new SelectList(_context.Set<Fuel>(), "ID", "FuelName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
selectedCategories, string[] selectedGadgets)
        {
            if (id == null)
            {
                return NotFound();
            }
            var carToUpdate = await _context.Car
            .Include(i => i.Dealer)
            .Include(i => i.Fuel)
            .Include(i => i.CarCategories)
            .ThenInclude(i => i.Category)
            .Include(i => i.CarGadgets)
            .ThenInclude(i => i.Gadget)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (carToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Car>(
            carToUpdate,
            "Car",
            i => i.Brand, i => i.Model,
            i => i.Price, i => i.AppearanceDate, i => i.Dealer, i => i.Dealer, i => i.Fuel, i => i.Fuel))
            {
                UpdateCarCategories(_context, selectedCategories, carToUpdate);
                UpdateCarGadgets(_context, selectedGadgets, carToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            /*{
                UpdateCarGadgets(_context, selectedGadgets, carToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }*/
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care
            //este editata 

            UpdateCarCategories(_context, selectedCategories, carToUpdate);
            PopulateAssignedCategoryData(_context, carToUpdate);
            PopulateAssignedGadgetData(_context, carToUpdate);
            /*UpdateCarGadgets(_context, selectedGadgets, carToUpdate);
            PopulateAssignedGadgetData(_context, carToUpdate);*/
            return Page();
        }
    }
}

           /* private bool CarExists(int id)
            {
                return _context.Car.Any(e => e.ID == id);
            }
        }
    }
}
*/
