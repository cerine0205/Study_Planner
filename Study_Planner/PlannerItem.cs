using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanner
{
    public enum TaskType
    {
        StudySession,
        Assignment,
        Quiz,
        Exam
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }


    // Abstract base class that defines common properties for all planner items
    public abstract class PlannerItem
    {
        public DateTime Date { get; }
        public string Title { get; }
        public string Category { get; }
        public int EstimatedMinutes { get; }
        public TaskType Type { get; }
        public Priority Priority { get; }
        public bool IsCompleted { get; private set; }

        protected PlannerItem(
            DateTime date,
            string title,
            string category,
            int estimatedMinutes,
            TaskType type,
            Priority priority)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Subject cannot be empty.");

            if (estimatedMinutes <= 0)
                throw new ArgumentException("Estimated time must be greater than 0 minutes.");

            Date = date;
            Title = title.Trim();
            Category = category.Trim();
            EstimatedMinutes = estimatedMinutes;
            Type = type;
            Priority = priority;
            IsCompleted = false;
        }

            public void MarkCompleted()
            {
                IsCompleted = true;
            }

        public virtual string GetDetails()
        {
            return $"{Type} | {Date:dd/MM/yyyy} | {Category} | {Title} | Est: {EstimatedMinutes} min | Priority: {Priority} | Completed: {IsCompleted}";
        }

        public virtual bool IsOverdue(DateTime today)
        {
            return !IsCompleted && Date.Date < today.Date;
        }

        public override string ToString()
            {
                return GetDetails();
            }
    }
}