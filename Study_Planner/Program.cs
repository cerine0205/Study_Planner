using StudyPlanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanner
{
    class Program
    {
        // Main planner object to store all tasks
        static Planner myPlanner = new Planner();

        static void Main(string[] args)
        {
            // This is the main program loop - keeps running until user exits
            bool keepRunning = true;

            while (keepRunning)
            {
                // Show the menu and get user choice
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("   STUDY PLANNER SYSTEM");
                Console.WriteLine("========================================");

                // Calculate and show current progress
                double currentProgress = CalculateProgressManually();
                Console.WriteLine("Current Progress: " + currentProgress + "%");
                Console.WriteLine("========================================");

                // Display menu options
                Console.WriteLine("1. Add Study Session");
                Console.WriteLine("2. Add Deadline Task");
                Console.WriteLine("3. Show All Tasks");
                Console.WriteLine("4. Mark Task as Completed");
                Console.WriteLine("5. Show High Priority Tasks");
                Console.WriteLine("6. Exit");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice (1-6): ");

                // Read user input
                string userChoice = Console.ReadLine().Trim();

                // Process user choice using if-else
                if (userChoice == "1")
                {
                    AddStudySession();
                }
                else if (userChoice == "2")
                {
                    AddDeadlineTask();
                }
                else if (userChoice == "3")
                {
                    ShowAllTasks();
                }
                else if (userChoice == "4")
                {
                    MarkTaskCompleted();
                }
                else if (userChoice == "5")
                {
                    ShowHighPriorityTasks(myPlanner);
                }
                else if (userChoice == "6")
                {
                    Console.WriteLine("\nThank you for using Study Planner!");
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("\nError: Please enter a number between 1 and 6.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        // ========================================
        // METHOD 1: ADD STUDY SESSION
        // ========================================
        static void AddStudySession()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   ADD NEW STUDY SESSION");
            Console.WriteLine("========================================");

            // Step 1: Get and validate Title
            string taskTitle = GetValidTitle();

            // Step 2: Get and validate Subject/Category
            string taskSubject = GetValidSubject();

            // Step 3: Get and validate Topic
            string taskTopic = GetValidTopic();

            // Step 4: Get and validate Minutes
            int taskMinutes = GetValidMinutes();

            // Step 5: Get and validate Date
            DateTime taskDate = GetValidDate();

            // Step 6: Get and validate Priority
            Priority taskPriority = GetValidPriority();

            // Try to create the study session
            try
            {
                StudySession newSession = new StudySession(
                    taskDate,
                    taskTitle,
                    taskSubject,
                    taskMinutes,
                    taskPriority,
                    taskTopic
                );

                myPlanner.AddItem(newSession);

                Console.WriteLine("\n*** SUCCESS! ***");
                Console.WriteLine("Study session added successfully!");
                Console.WriteLine("Details: " + newSession.GetDetails());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: Could not add the session.");
                Console.WriteLine("Reason: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ========================================
        // METHOD 2: ADD DEADLINE TASK
        // ========================================
        static void AddDeadlineTask()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   ADD NEW DEADLINE TASK");
            Console.WriteLine("========================================");

            // Step 1: Get and validate Title
            string taskTitle = GetValidTitle();

            // Step 2: Get and validate Subject
            string taskSubject = GetValidSubject();

            // Step 3: Get and validate Minutes
            int taskMinutes = GetValidMinutes();

            // Step 4: Get and validate Date
            DateTime taskDate = GetValidDate();

            // Step 5: Get and validate Task Type
            TaskType taskType = GetValidTaskType();

            // Step 6: Get and validate Priority
            Priority taskPriority = GetValidPriority();

            // Try to create the deadline task
            try
            {
                DeadlineTask newTask = new DeadlineTask(
                    taskDate,
                    taskTitle,
                    taskSubject,
                    taskMinutes,
                    taskType,
                    taskPriority
                );

                myPlanner.AddItem(newTask);

                Console.WriteLine("\n*** SUCCESS! ***");
                Console.WriteLine("Deadline task added successfully!");
                Console.WriteLine("Details: " + newTask.GetDetails());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: Could not add the task.");
                Console.WriteLine("Reason: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ========================================
        // METHOD 3: SHOW ALL TASKS
        // ========================================
        static void ShowAllTasks()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   ALL TASKS");
            Console.WriteLine("========================================");

            // Check if there are any tasks
            if (myPlanner.Items.Count == 0)
            {
                Console.WriteLine("No tasks found. The list is empty.");
            }
            else
            {
                // Loop through all tasks and display them
                int taskNumber = 1;
                for (int i = 0; i < myPlanner.Items.Count; i++)
                {
                    PlannerItem currentTask = myPlanner.Items[i];

                    // Show task number and status
                    if (currentTask.IsCompleted)
                    {
                        Console.Write("[DONE] ");
                    }
                    else
                    {
                        Console.Write("[TODO] ");
                    }

                    Console.WriteLine(taskNumber + ". " + currentTask.GetDetails());
                    taskNumber++;
                }

                Console.WriteLine("========================================");
                Console.WriteLine("Total tasks: " + myPlanner.Items.Count);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ========================================
        // METHOD 4: MARK TASK AS COMPLETED
        // ========================================
        static void MarkTaskCompleted()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   MARK TASK AS COMPLETED");
            Console.WriteLine("========================================");

            // Check if there are any tasks
            if (myPlanner.Items.Count == 0)
            {
                Console.WriteLine("No tasks found. The list is empty.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Show all incomplete tasks
            Console.WriteLine("Incomplete tasks:\n");

            int displayNumber = 1;
            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                PlannerItem currentTask = myPlanner.Items[i];
                if (!currentTask.IsCompleted)
                {
                    Console.WriteLine(displayNumber + ". " + currentTask.Title + " (" + currentTask.Category + ")");
                    displayNumber++;
                }
            }

            // If all tasks are completed
            if (displayNumber == 1)
            {
                Console.WriteLine("All tasks are already completed! Great job!");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Get task number to mark as complete
            Console.Write("\nEnter task number to mark as completed: ");
            string taskNumberInput = Console.ReadLine();

            // Validate the input
            int selectedTaskNumber;
            bool isValidNumber = int.TryParse(taskNumberInput, out selectedTaskNumber);

            if (!isValidNumber)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            else if (selectedTaskNumber < 1 || selectedTaskNumber >= displayNumber)
            {
                Console.WriteLine("Error: Task number out of range.");
            }
            else
            {
                // Find and mark the task as completed
                int incompleteCounter = 0;
                for (int i = 0; i < myPlanner.Items.Count; i++)
                {
                    PlannerItem currentTask = myPlanner.Items[i];
                    if (!currentTask.IsCompleted)
                    {
                        incompleteCounter++;
                        if (incompleteCounter == selectedTaskNumber)
                        {
                            currentTask.MarkCompleted();
                            Console.WriteLine("\n*** SUCCESS! ***");
                            Console.WriteLine("Task marked as completed: " + currentTask.Title);
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ========================================
        // VALIDATION METHOD 1: GET VALID TITLE
        // ========================================
        // This method keeps asking until user enters a valid title
        static string GetValidTitle()
        {
            string userInput = "";
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter task title: ");
                userInput = Console.ReadLine();

                // Check if input is empty or just spaces
                if (userInput == null || userInput.Trim() == "")
                {
                    Console.WriteLine("Error: Title cannot be empty. Please try again.");
                }
                else
                {
                    isValid = true;
                }
            }

            return userInput.Trim();
        }

        // ========================================
        // VALIDATION METHOD 2: GET VALID SUBJECT
        // ========================================
        static string GetValidSubject()
        {
            string userInput = "";
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter subject/category: ");
                userInput = Console.ReadLine();

                // Check if input is empty or just spaces
                if (userInput == null || userInput.Trim() == "")
                {
                    Console.WriteLine("Error: Subject cannot be empty. Please try again.");
                }
                else
                {
                    isValid = true;
                }
            }

            return userInput.Trim();
        }

        // ========================================
        // VALIDATION METHOD 3: GET VALID TOPIC
        // ========================================
        static string GetValidTopic()
        {
            string userInput = "";
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter topic: ");
                userInput = Console.ReadLine();

                // Check if input is empty or just spaces
                if (userInput == null || userInput.Trim() == "")
                {
                    Console.WriteLine("Error: Topic cannot be empty. Please try again.");
                }
                else
                {
                    isValid = true;
                }
            }

            return userInput.Trim();
        }

        // ========================================
        // VALIDATION METHOD 4: GET VALID MINUTES
        // ========================================
        // This is MY MAIN CONTRIBUTION - Manual validation using TryParse
        static int GetValidMinutes()
        {
            int validMinutes = 0;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter estimated minutes (e.g., 30, 60, 120): ");
                string minutesInput = Console.ReadLine();

                // Try to convert the input to a number
                bool isNumber = int.TryParse(minutesInput, out validMinutes);

                if (!isNumber)
                {
                    // User entered letters or special characters
                    Console.WriteLine("Error: Please enter a number, not letters or symbols.");
                }
                else if (validMinutes <= 0)
                {
                    // User entered zero or negative number
                    Console.WriteLine("Error: Minutes must be greater than zero.");
                }
                else if (validMinutes > 999)
                {
                    // User entered a very large number
                    Console.WriteLine("Error: Minutes cannot exceed 999. Please enter a reasonable value.");
                }
                else
                {
                    // Input is valid
                    isValid = true;
                }
            }

            return validMinutes;
        }

        // ========================================
        // VALIDATION METHOD 5: GET VALID DATE
        // ========================================
        // This ensures date is in correct format and not in the past
        static DateTime GetValidDate()
        {
            DateTime validDate = DateTime.Today;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter date (dd/MM/yyyy, e.g., 25/12/2024): ");
                string dateInput = Console.ReadLine();

                // Try to parse the date in the exact format
                bool isValidFormat = DateTime.TryParseExact(
                    dateInput,
                    "dd/MM/yyyy",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out validDate
                );

                if (!isValidFormat)
                {
                    // Date format is wrong
                    Console.WriteLine("Error: Date format is incorrect.");
                    Console.WriteLine("Please use dd/MM/yyyy format (e.g., 25/12/2024).");
                }
                else if (validDate.Date < DateTime.Today)
                {
                    // Date is in the past
                    Console.WriteLine("Error: You cannot add a task with a past date.");
                    Console.WriteLine("Today's date is: " + DateTime.Today.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Please enter today's date or a future date.");
                }
                else
                {
                    // Date is valid
                    isValid = true;
                }
            }

            return validDate;
        }

        // ========================================
        // VALIDATION METHOD 6: GET VALID PRIORITY
        // ========================================
        static Priority GetValidPriority()
        {
            Priority validPriority = Priority.Medium;
            bool isValid = false;

            while (!isValid)
            {
                Console.WriteLine("\nSelect priority:");
                Console.WriteLine("1. Low");
                Console.WriteLine("2. Medium");
                Console.WriteLine("3. High");
                Console.Write("Enter your choice (1-3): ");

                string priorityInput = Console.ReadLine();

                // Try to convert to number
                int priorityNumber;
                bool isNumber = int.TryParse(priorityInput, out priorityNumber);

                if (!isNumber)
                {
                    Console.WriteLine("Error: Please enter a number (1, 2, or 3).");
                }
                else if (priorityNumber == 1)
                {
                    validPriority = Priority.Low;
                    isValid = true;
                }
                else if (priorityNumber == 2)
                {
                    validPriority = Priority.Medium;
                    isValid = true;
                }
                else if (priorityNumber == 3)
                {
                    validPriority = Priority.High;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Error: Invalid choice. Please enter 1, 2, or 3.");
                }
            }

            return validPriority;
        }

        // ========================================
        // VALIDATION METHOD 7: GET VALID TASK TYPE
        // ========================================
        static TaskType GetValidTaskType()
        {
            TaskType validTaskType = TaskType.Assignment;
            bool isValid = false;

            while (!isValid)
            {
                Console.WriteLine("\nSelect task type:");
                Console.WriteLine("1. Assignment");
                Console.WriteLine("2. Quiz");
                Console.WriteLine("3. Exam");
                Console.Write("Enter your choice (1-3): ");

                string typeInput = Console.ReadLine();

                // Try to convert to number
                int typeNumber;
                bool isNumber = int.TryParse(typeInput, out typeNumber);

                if (!isNumber)
                {
                    Console.WriteLine("Error: Please enter a number (1, 2, or 3).");
                }
                else if (typeNumber == 1)
                {
                    validTaskType = TaskType.Assignment;
                    isValid = true;
                }
                else if (typeNumber == 2)
                {
                    validTaskType = TaskType.Quiz;
                    isValid = true;
                }
                else if (typeNumber == 3)
                {
                    validTaskType = TaskType.Exam;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Error: Invalid choice. Please enter 1, 2, or 3.");
                }
            }

            return validTaskType;
        }

        // ========================================
        // HELPER METHOD: CALCULATE PROGRESS MANUALLY
        // ========================================
        // This method calculates progress using a simple loop
        static double CalculateProgressManually()
        {
            // If there are no tasks, progress is 0%
            if (myPlanner.Items.Count == 0)
            {
                return 0.0;
            }

            // Count how many tasks are completed
            int completedCount = 0;
            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                if (myPlanner.Items[i].IsCompleted)
                {
                    completedCount++;
                }
            }

            // Calculate percentage
            double totalTasks = myPlanner.Items.Count;
            double progress = (completedCount / totalTasks) * 100.0;

            // Round to 2 decimal places manually
            progress = Math.Round(progress, 2);

            return progress;
        }


        static void ShowHighPriorityTasks(Planner planner)
        {
            var highPriorityItems = planner.GetItemsByPriority(Priority.High);

            if (highPriorityItems.Count == 0)
            {
                Console.WriteLine("No high priority tasks found.");
            }
            else
            {
                Console.WriteLine("High Priority Tasks:");
                foreach (var item in highPriorityItems)
                {
                    Console.WriteLine(item);
                }
            }

            Console.ReadKey();

        }


    }
}