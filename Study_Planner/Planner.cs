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
        public List<PlannerItem> Items = new List<PlannerItem>();

    public double CalculateProgress()
    {
        if (Items.Count == 0) return 0;
        int completed = Items.Count(i => i.IsCompleted);
        return Math.Round((completed * 100.0) / Items.Count, 2);
    }
    public List<PlannerItem> FilterByDay(DateTime day)
    {
        return Items.Where(i => i.Date.Date == day.Date).ToList();
    }

    public List<PlannerItem> FilterByWeek(DateTime startOfWeek)
    {
        DateTime end = startOfWeek.AddDays(7);
        return Items.Where(i => i.Date >= startOfWeek && i.Date < end).ToList();
    }

    public List<PlannerItem> SortByPriority()
    {
        return Items.OrderByDescending(i => i.Priority).ThenBy(i => i.Date).ToList();
    }
    }
}