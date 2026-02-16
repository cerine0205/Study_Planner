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
            [DataMember] public ItemDto[] Items;
        }

        public void Save(PlannerItem[] items, int weeklyGoalMinutes = 0 , int count = -1)
        {
            int n = (count >= 0) ? count : items.Length;

            ItemDto[] dtos = new ItemDto[n];

            for (int i = 0; i < n; i++)
            {
               var item = items[i];

               if (item is StudySession s)
               {
                 dtos[i] = new ItemDto
                 {
                   ItemClass = "StudySession",
                   Date = s.Date,
                   Title = s.Title,
                   Category = s.Category,
                   EstimatedMinutes = s.EstimatedMinutes,
                   Priority = s.Priority,
                   Topic = s.Topic,
                   IsCompleted = s.IsCompleted
                 };
               }
              else if (item is DeadlineTask d)
              {
                   dtos[i] = new ItemDto
                   {
                      ItemClass = "DeadlineTask",
                      Date = d.Date,
                      Title = d.Title,
                      Category = d.Category,
                      EstimatedMinutes = d.EstimatedMinutes,
                      Priority = d.Priority,
                      Type = d.Type,
                      IsCompleted = d.IsCompleted
                    };
               }
           }

            var root = new RootDto
           {
               WeeklyGoalMinutes = weeklyGoalMinutes,
               Items = dtos
            };

            using (var fs = new FileStream(_path, FileMode.Create, FileAccess.Write))
            {
                var ser = new DataContractJsonSerializer(typeof(RootDto));
                ser.WriteObject(fs, root);
            }

        }

        public (PlannerItem[] Items, int WeeklyGoalMinutes) Load()
        {
            if (!File.Exists(_path))
                return (Array.Empty<PlannerItem>(), 0);

            using (var fs = File.OpenRead(_path))
            {
                var ser = new DataContractJsonSerializer(typeof(RootDto));
                var root = (RootDto)ser.ReadObject(fs);

                PlannerItem[] result = new PlannerItem[100]; 
                int count = 0;

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
                        {
                           if (count >= result.Length)
                           {
                             
                              PlannerItem[] newArray = new PlannerItem[result.Length * 2];
                              for (int i = 0; i < result.Length; i++)
                                     newArray[i] = result[i];
              
                                        result = newArray;
                            }

                            result[count] = obj;
                            count++;
                        } 
                    }
                }
                
                PlannerItem[] finalArray = new PlannerItem[count];
                for (int i = 0; i < count; i++)
                  finalArray[i] = result[i];

                return (finalArray, root?.WeeklyGoalMinutes ?? 0);
            }
        }
    }
}