using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;

namespace Proiect.Models
{
    public class CarCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public List<AssignedGadgetData> AssignedGadgetDataList;
        public void PopulateAssignedCategoryData(ProiectContext context,  Car car)
        {
            var allCategories = context.Category;
            var carCategories = new HashSet<int>(car.CarCategories.Select(c => c.CategoryID)); //
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = carCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateCarCategories(ProiectContext context,
        string[] selectedCategories, Car carToUpdate)
        {
            if (selectedCategories == null)
            {
                carToUpdate.CarCategories = new List<CarCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var carCategories = new HashSet<int>
            (carToUpdate.CarCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!carCategories.Contains(cat.ID))
                    {
                        carToUpdate.CarCategories.Add(
                        new CarCategory
                        {
                            CarID = carToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (carCategories.Contains(cat.ID))
                    {
                        CarCategory courseToRemove
                        = carToUpdate
                        .CarCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }

        public void PopulateAssignedGadgetData(ProiectContext context,  Car car)
        {
            var allGadgets = context.Gadget;
            var carGadgets = new HashSet<int>(car.CarGadgets.Select(c => c.GadgetID)); //
            AssignedGadgetDataList = new List<AssignedGadgetData>();
            foreach (var cat in allGadgets)
            {
                AssignedGadgetDataList.Add(new AssignedGadgetData
                {
                    GadgetID = cat.ID,
                    Name = cat.GadgetName,
                    Assigned = carGadgets.Contains(cat.ID)
                });
            }
        }
        public void UpdateCarGadgets(ProiectContext context,
        string[] selectedGadgets, Car carToUpdate)
        {
            if (selectedGadgets == null)
            {
                carToUpdate.CarGadgets = new List<CarGadget>();
                return;
            }
            var selectedGadgetsHS = new HashSet<string>(selectedGadgets);
            var carGadgets = new HashSet<int>
            (carToUpdate.CarGadgets.Select(c => c.Gadget.ID));
            foreach (var cat in context.Gadget)
            {
                if (selectedGadgetsHS.Contains(cat.ID.ToString()))
                {
                    if (!carGadgets.Contains(cat.ID))
                    {
                        carToUpdate.CarGadgets.Add(
                        new CarGadget
                        {
                            CarID = carToUpdate.ID,
                            GadgetID = cat.ID
                        });
                    }
                }
                else
                {
                    if (carGadgets.Contains(cat.ID))
                    {
                        CarGadget courseToRemove
                        = carToUpdate
                        .CarGadgets
                        .SingleOrDefault(i => i.GadgetID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }

        }
    }
}
