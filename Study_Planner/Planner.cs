using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanner
{
    // Manages planner items and validates data before adding them to the system.
    public class Planner
    {public PlannerItem[] GetItems()
{
     return items;
}
        private PlannerItem[] items;
        private int count;
        private readonly FileStorage storage = new FileStorage("planner-data.json");
        public int WeeklyGoalMinutes { get; set; } = 0;

        
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

            if (count >= items.Length)
            {
                PlannerItem[] newArray = new PlannerItem[items.Length * 2];
                for (int i = 0; i < count; i++)
                    newArray[i] = items[i];

                items = newArray;
            }

         
            items[count] = item;
            count++;

          
            storage.Save(items, WeeklyGoalMinutes, count);
        }
        public PlannerItem[] Items => items;

        public double CalculateProgress()
        {

            if (count == 0) return 0;

            int completed = 0;

            for (int i = 0; i < count; i++)
            {
                if (items[i].IsCompleted)
                completed++;
            }

             return Math.Round((completed * 100.0) / count, 2);
        }  
        
        public PlannerItem[] FilterByDay(DateTime day)
        {
            PlannerItem[] temp = new PlannerItem[count];
            int newCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (items[i].Date.Date == day.Date)
                {
                   temp[newCount] = items[i];
                   newCount++;
                }
            }

            PlannerItem[] result = new PlannerItem[newCount];
            for (int i = 0; i < newCount; i++)
               result[i] = temp[i];

            return result;
        }

        public PlannerItem[] FilterByWeek(DateTime startOfWeek)
        {
           DateTime end = startOfWeek.AddDays(7);

           PlannerItem[] temp = new PlannerItem[count];
           int newCount = 0;

           for (int i = 0; i < count; i++)
           {
              if (items[i].Date >= startOfWeek && items[i].Date < end)
              {
                 temp[newCount] = items[i];
                 newCount++;
              }
           }

           PlannerItem[] result = new PlannerItem[newCount];
           for (int i = 0; i < newCount; i++)
              result[i] = temp[i];

            return result;
        }

        public PlannerItem[] SortByPriority()
        {
            PlannerItem[] sorted = new PlannerItem[count];

            for (int i = 0; i < count; i++)
                sorted[i] = items[i];

            Array.Sort(sorted, 0, count, Comparer<PlannerItem>.Create((a, b) =>
            {
                int priorityCompare = b.Priority.CompareTo(a.Priority);
                if (priorityCompare != 0)
                    return priorityCompare;

                return a.Date.CompareTo(b.Date);
            }));

            return sorted;
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
            if (index < 0 || index >= count)
               return false;

            Items[index].MarkCompleted();
            storage.Save(items, WeeklyGoalMinutes);

             return true;
        }



        public List<PlannerItem> GetItemsByPriority(Priority priority)
        {
            List<PlannerItem> result = new List<PlannerItem>();

            foreach (var item in items)
            {
                if (item.Priority == priority)
                {
                    result.Add(item);
                }
            }

            return result;
        }


    }
}