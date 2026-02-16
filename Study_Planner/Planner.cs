using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanner{

    // Manages planner items and validates data before adding them to the system.
public class Planner{
   
   
    public PlannerItem[] Items = new PlannerItem[0];

    public void AddItem(PlannerItem item)
    {
        Array.Resize(ref Items, Items.Length + 1);
        Items[Items.Length - 1] = item;
    }

    public double CalculateProgress()
    {
    if (Items.Length == 0)
        return 0;

    int completed = Items.Count(i => i.IsCompleted);
    double progress = (completed * 100.0) / Items.Length;

    if (progress >= 80)
    {
        Console.WriteLine(" Amazing work! You are almost there!");
    }

    return Math.Round(progress, 2);
    }

    public PlannerItem[] FilterByDay(DateTime day)
    {
        return Items
            .Where(i => i.Date.Date == day.Date)
            .ToArray();
    }

    public PlannerItem[] SortByPriority()
    {
        return Items
            .OrderByDescending(i => i.Priority)
            .ThenBy(i => i.Date)
            .ToArray();
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
               if (index < 0 || index >= Items.Length)
            return false;

        Items[index].MarkCompleted();
        return true;
    }



       
    public PlannerItem[] GetItemsByPriority(Priority priority)
    {
        return Items.Where(i => i.Priority == priority).ToArray();
    }


}}
