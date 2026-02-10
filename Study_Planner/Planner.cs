using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanner
{
    // Manages planner items and validates data before adding them to the system.
    public class Planner
    {
        private List<PlannerItem> items;

        public Planner()
        {
            items = new List<PlannerItem>();
        }

        // Adds a new planner item after validating its date
        public void AddItem(PlannerItem item)
        {
            if (item == null)
                throw new ArgumentNullException("Item cannot be null.");

            if (item.Date.Date < DateTime.Today)
                throw new ArgumentException("You cannot add an item with a past date.");

            items.Add(item);
        }
    }
}