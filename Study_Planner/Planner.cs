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
        private readonly FileStorage storage = new FileStorage("planner-data.json");
        public int WeeklyGoalMinutes { get; private set; } = 0;

        
        public Planner()
        {
            var loaded = storage.Load();
            items = loaded.Items;
            WeeklyGoalMinutes = loaded.WeeklyGoalMinutes;
           
        }

        // Adds a new planner item after validating its date
        public void AddItem(PlannerItem item)
        {
            if (item == null)
                throw new ArgumentNullException("Item cannot be null.");

            if (item.Date.Date < DateTime.Today)
                throw new ArgumentException("You cannot add an item with a past date.");

            items.Add(item);
            storage.Save(items, WeeklyGoalMinutes);
        }
        public List<PlannerItem> Items => items;

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

        public Dictionary<string, (int plannedMin, int completedMin)> SubjectSummaryForMonth(int year, int month)
        {
            var inMonth = Items.Where(i => i.Date.Year == year && i.Date.Month == month);

            return inMonth
              .GroupBy(i => i.Category)
              .ToDictionary(
                  g => g.Key,
                  g => (
                      plannedMin: g.Sum(x => x.EstimatedMinutes),
                     completedMin: g.Where(x => x.IsCompleted)
                                    .Sum(x => x.EstimatedMinutes)
                    )
                );
        }

        public bool MarkCompletedByIndex(int index)
        {
            if (index < 0 || index >= Items.Count)
               return false;

            Items[index].MarkCompleted();
            storage.Save(items, WeeklyGoalMinutes);

             return true;
        }

     
    }
}