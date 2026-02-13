using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace StudyPlanner
{
    public class FileStorage
    {
        private readonly string _path;

        public FileStorage(string path = "planner-data.json")
        {
            _path = path;
        }

        // DTO for saving/loading (because PlannerItem is abstract)
        [DataContract]
        private class ItemDto
        {

            // "StudySession" or "DeadlineTask"
            [DataMember] public string ItemClass;  
            [DataMember] public DateTime Date;
            [DataMember] public string Title;
            [DataMember] public string Category;
            [DataMember] public int EstimatedMinutes;
            [DataMember] public TaskType Type;
            [DataMember] public Priority Priority;
            [DataMember] public bool IsCompleted;

            // StudySession extra
            [DataMember(EmitDefaultValue = false)] public string Topic;
        }

        [DataContract]
        private class RootDto
        {
            [DataMember] public int WeeklyGoalMinutes;
            [DataMember] public List<ItemDto> Items;
        }

        public void Save(List<PlannerItem> items, int weeklyGoalMinutes = 0)
        {
            var root = new RootDto
            {
                WeeklyGoalMinutes = weeklyGoalMinutes,
                Items = new List<ItemDto>()
            };

            foreach (var it in items)
            {
                if (it is StudySession s)
                {
                    root.Items.Add(new ItemDto
                    {
                        ItemClass = "StudySession",
                        Date = s.Date,
                        Title = s.Title,
                        Category = s.Category,
                        EstimatedMinutes = s.EstimatedMinutes,
                        Type = s.Type,
                        Priority = s.Priority,
                        IsCompleted = s.IsCompleted,
                        Topic = s.Topic
                    });
                }
                else if (it is DeadlineTask d)
                {
                    root.Items.Add(new ItemDto
                    {
                        ItemClass = "DeadlineTask",
                        Date = d.Date,
                        Title = d.Title,
                        Category = d.Category,
                        EstimatedMinutes = d.EstimatedMinutes,
                        Type = d.Type,
                        Priority = d.Priority,
                        IsCompleted = d.IsCompleted
                    });
                }
            }

            using (var fs = File.Create(_path))
            {
                var ser = new DataContractJsonSerializer(typeof(RootDto));
                ser.WriteObject(fs, root);
            }
        }

        public (List<PlannerItem> Items, int WeeklyGoalMinutes) Load()
        {
            if (!File.Exists(_path))
                return (new List<PlannerItem>(), 0);

            using (var fs = File.OpenRead(_path))
            {
                var ser = new DataContractJsonSerializer(typeof(RootDto));
                var root = (RootDto)ser.ReadObject(fs);

                var result = new List<PlannerItem>();

                if (root?.Items != null)
                {
                    foreach (var dto in root.Items)
                    {
                        PlannerItem obj = null;

                        // Determine the correct object type when reconstructing from JSON
                        if (dto.ItemClass == "StudySession")
                        {
                            // Recreate StudySession object (requires Topic in addition to base fields)
                            // If Topic is null in saved file, use "General" as a safe default value
                            obj = new StudySession(
                                dto.Date,
                                dto.Title,
                                dto.Category,
                                dto.EstimatedMinutes,
                                dto.Priority,
                                dto.Topic ?? "General"
                            );
                        }
                        else if (dto.ItemClass == "DeadlineTask")
                        {
                            // Recreate DeadlineTask object using stored task type and priority
                            obj = new DeadlineTask(
                                dto.Date,
                                dto.Title,
                                dto.Category,
                                dto.EstimatedMinutes,
                                dto.Type,
                                dto.Priority
                            );
                        }
                        
                        // After reconstructing the object, restore completion state
                        if (obj != null && dto.IsCompleted)
                            obj.MarkCompleted();

                        // Add the restored object to the result list
                        if (obj != null)
                            result.Add(obj);
                    }
                }

                return (result, root?.WeeklyGoalMinutes ?? 0);
            }
        }
    }
}