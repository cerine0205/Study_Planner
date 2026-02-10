using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudyPlanner
{

    // Represents a study session (Inheritance from PlannerItem)
    public class StudySession : PlannerItem
    {
        public string Topic { get; }

        public StudySession(
            DateTime date,
            string title,
            string category,
            int estimatedMinutes,
            Priority priority,
            string topic)
            : base(
                date,
                title,
                category,
                estimatedMinutes,
                TaskType.StudySession,
                priority)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Topic cannot be empty.");

            Topic = topic.Trim();
        }

        public override string GetDetails()
        {
            return $"{Type} | {Date:dd/MM/yyyy} | {Category} | {Title} | Est: {EstimatedMinutes} min | Priority: {Priority} | Completed: {IsCompleted}";

        }
    }
}