using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanner
{
    // Represents a deadline-based task (Assignment, Quiz, Exam)
    public class DeadlineTask : PlannerItem
    {
        public DeadlineTask(
            DateTime date,
            string title,
            string category,
            int estimatedMinutes,
            TaskType type,
            Priority priority)
            : base(
                date,
                title,
                category,
                estimatedMinutes,
                type,
                priority)
        {
            if (type == TaskType.StudySession)
                throw new ArgumentException("DeadlineTask type must be Assignment, Quiz, or Exam.");
        }

        public override string GetDetails()
        {
            return $"{Type} (Deadline) | {Date:dd/MM/yyyy} | {Category} | {Title} | Est: {EstimatedMinutes} min | Priority: {Priority} | Completed: {IsCompleted}";

        }
    }
}