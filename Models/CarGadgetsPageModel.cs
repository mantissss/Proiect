using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Proiect.Data;

namespace Proiect.Models
{
    public class CarGadgetsPageModel : PageModel
    {
        public List<AssignedGadgetData> AssignedGadgetDataList;
        public void PopulateAssignedGadgetData(ProiectContext context,
        Car car)
        {
            var allGadgets = context.Gadget;
            var carGadgets = new HashSet<int>(
            car.CarGadgets.Select(c => c.GadgetID)); //
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
