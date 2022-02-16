using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Cars
{
    public class CreateModel : CarCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DealerID"] = new SelectList(_context.Set<Dealer>(), "ID", "DealerName");
            ViewData["FuelID"] = new SelectList(_context.Set<Fuel>(), "ID", "FuelName");

            var car = new Car();
            car.CarCategories = new List<CarCategory>();
            car.CarGadgets = new List<CarGadget>();
            PopulateAssignedCategoryData(_context, car);
            PopulateAssignedGadgetData(_context, car);
            /*car.CarGadgets = new List<CarGadget>();
            PopulateAssignedGadgetData(_context, car);*/
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories, string[] selectedGadgets) 
        {
            var newCar = new Car();
            if (selectedCategories != null)
            {
                newCar.CarCategories = new List<CarCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new CarCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newCar.CarCategories.Add(catToAdd);
                }
            }


            if (selectedGadgets != null)
            {
                newCar.CarGadgets = new List<CarGadget>();
                foreach (var cat in selectedGadgets)
                {
                    var catToAdd = new CarGadget
                    {
                        GadgetID = int.Parse(cat)
                    };
                    newCar.CarGadgets.Add(catToAdd);
                }
            }

            PopulateAssignedGadgetData(_context, newCar);
            PopulateAssignedCategoryData(_context, newCar);

            if (await TryUpdateModelAsync<Car>(newCar, "Car",
            i => i.Brand, i => i.Model, i => i.Price, i => i.AppearanceDate,
            i => i.DealerID, i => i.DealerID, i => i.FuelID, i => i.FuelID
            ))
            {
                _context.Car.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }


            /*if (await TryUpdateModelAsync<Car>(
            newCar,
            "Car",
            i => i.Brand, i => i.Model, i => i.Price, i => i.AppearanceDate, i => i.DealerID))
            {
                _context.Car.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            } */


            
            return Page();
        }
        /*public async Task<IActionResult> OnPostAsync(string[] selectedGadgets)
        {
            var newCar = new Car();
            if (selectedGadgets != null)
            {
                newCar.CarGadgets = new List<CarGadget>();
                foreach (var cat in selectedGadget)
                {
                    var catToAdd = new CarGadget
                    {
                        GadgetID = int.Parse(cat)
                    };
                    newCar.CarGadgets.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Car>(
            newCar,
            "Car",
            i => i.Brand, i => i.Model,
            i => i.Price, i => i.AppearanceDate, i => i.DealerID))
            {
                _context.Car.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedGadgetData(_context, newCar);
            return Page();
        }*/
    }
}
