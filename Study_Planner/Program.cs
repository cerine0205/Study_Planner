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

        // Study tips array - Simple student-level feature
        static string[] studyTips = new string[]
        {
            "Take short breaks every 30 minutes to stay focused.",
            "Review your notes within 24 hours for better retention.",
            "Study in a quiet place with good lighting.",
            "Get enough sleep before exams - 7-8 hours is ideal.",
            "Start with the hardest subject when your mind is fresh."
        };
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


                DisplayRandomTip();
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
                Console.WriteLine("6.  Search Tasks");
                Console.WriteLine("7.  Edit Task");
                Console.WriteLine("8.  Delete Task");
                Console.WriteLine("9.  Update Task Status");
                Console.WriteLine("10. Show Overdue Tasks");
                Console.WriteLine("11. Set Weekly Study Goal");
                Console.WriteLine("12. Show Upcoming Deadlines (Next 3 Days)");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("0. Exit");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice (0-12): ");

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
                    SearchTasks();
                }
                else if (userChoice == "7")
                {
                    EditTask();
                }
                else if (userChoice == "8")
                {
                    DeleteTask();
                }
                else if (userChoice == "9")
                {
                    UpdateTaskStatus();
                }
                else if (userChoice == "10")
                {
                    ShowOverdueTasks();
                }
                else if (userChoice == "11")
                {
                    SetWeeklyGoal();
                }
                else if (userChoice == "12")
                {
                    ShowUpcomingDeadlines();
                }
                else if (userChoice == "0")
                {
                    Console.WriteLine("\n========================================");
                    Console.WriteLine("Thank you for using Study Planner!");
                    Console.WriteLine("========================================");
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("\nError: Please enter a number between 0 and 12.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
        static void DisplayRandomTip()
        {
            Random random = new Random();
            int randomIndex = random.Next(studyTips.Length);
            Console.WriteLine("Note: " + studyTips[randomIndex]);
            Console.WriteLine();
        }
        // METHOD 1: ADD STUDY SESSION

        static void AddStudySession()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   ADD NEW STUDY SESSION");
            Console.WriteLine("========================================");

            // Get and validate all inputs
            string taskTitle = GetValidTitle();
            string taskSubject = GetValidSubject();
            string taskTopic = GetValidTopic();
            int taskMinutes = GetValidMinutes();
            DateTime taskDate = GetValidDate();
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

        // METHOD 2: ADD DEADLINE TASK

        static void AddDeadlineTask()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   ADD NEW DEADLINE TASK");
            Console.WriteLine("========================================");

            // Get and validate all inputs
            string taskTitle = GetValidTitle();
            string taskSubject = GetValidSubject();
            int taskMinutes = GetValidMinutes();
            DateTime taskDate = GetValidDate();
            TaskType taskType = GetValidTaskType();
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

        // METHOD 3: SHOW ALL TASKS

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

        // METHOD 4: MARK TASK AS COMPLETED

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
        static string GetValidTitle()
        {
            string userInput = "";
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter task title: ");
                userInput = Console.ReadLine();

                // Check if user pressed Enter without typing anything
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Error: Title cannot be empty. Please try again.");
                }
                else if (userInput.Trim().Length < 3)
                {
                    Console.WriteLine("Error: Title is too short. Please enter at least 3 characters.");
                    Console.WriteLine("Example: 'Review Math' or 'Study Chapter 5'");
                }
                else if (IsOnlyNumbers(userInput.Trim()))
                {
                    Console.WriteLine("Error: Title cannot be just numbers. Please add a description.");
                    Console.WriteLine("Example: Instead of '4', write 'Chapter 4 Review'");
                }
                else if (HasTooManyRepeatedChars(userInput.Trim()))
                {
                    Console.WriteLine("Error: Title contains too many repeated characters.");
                    Console.WriteLine("Please enter a meaningful title, not random letters.");
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

                // Check if user pressed Enter without typing anything
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Error: Subject cannot be empty. Please try again.");
                }
                else if (IsOnlyNumbers(userInput.Trim()))
                {
                    Console.WriteLine("Error: Subject name cannot be just numbers.");
                    Console.WriteLine("Please enter a real subject name like 'Math' or 'Physics'.");
                }
                else if (HasTooManyRepeatedChars(userInput.Trim()))
                {
                    Console.WriteLine("Error: Subject name contains too many repeated characters.");
                    Console.WriteLine("Please enter a real subject name, not random letters.");
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

                // Check if user pressed Enter without typing anything
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Error: Topic cannot be empty. Please try again.");
                }
                else if (userInput.Trim().Length < 3)
                {
                    Console.WriteLine("Error: Topic is too short. Please enter at least 3 characters.");
                }
                else if (HasTooManyRepeatedChars(userInput.Trim()))
                {
                    Console.WriteLine("Error: Topic contains too many repeated characters.");
                    Console.WriteLine("Please enter a meaningful topic, not random letters.");
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
        static int GetValidMinutes()
        {
            int validMinutes = 0;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter estimated minutes (e.g., 30, 60, 120): ");
                string minutesInput = Console.ReadLine();

                // Check if user pressed Enter without typing anything
                if (string.IsNullOrWhiteSpace(minutesInput))
                {
                    Console.WriteLine("Error: Input cannot be empty. Please enter a number.");
                    continue;
                }

                bool isNumber = int.TryParse(minutesInput, out validMinutes);

                if (!isNumber)
                {
                    Console.WriteLine("Error: Please enter a number, not letters or symbols.");
                }
                else if (validMinutes <= 0)
                {
                    Console.WriteLine("Error: Minutes must be greater than zero.");
                }
                else if (validMinutes > 480)
                {
                    Console.WriteLine("Error: Minutes cannot exceed 480 (8 hours).");
                    Console.WriteLine("Reason: This is unrealistic for a single study session.");
                    Console.WriteLine("Please break your task into smaller sessions.");
                }
                else
                {
                    isValid = true;
                }
            }

            return validMinutes;
        }

        // ========================================
        // VALIDATION METHOD 5: GET VALID DATE
        // ========================================
        static DateTime GetValidDate()
        {
            DateTime validDate = DateTime.Today;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("Enter date (dd/MM/yyyy, e.g., 25/03/2026): ");
                string dateInput = Console.ReadLine();

                // Check if user pressed Enter without typing anything
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    Console.WriteLine("Error: Date cannot be empty. Please try again.");
                    continue;
                }

                bool isValidFormat = DateTime.TryParseExact(
                    dateInput,
                    "dd/MM/yyyy",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out validDate
                );

                if (!isValidFormat)
                {
                    Console.WriteLine("Error: Date format is incorrect.");
                    Console.WriteLine("Reason: Day, month, or year is invalid, or wrong separators used.");
                    Console.WriteLine("Please use dd/MM/yyyy format (e.g., 25/03/2026).");
                }
                else if (validDate.Date < DateTime.Today)
                {
                    Console.WriteLine("Error: You cannot add a task with a past date.");
                    Console.WriteLine("Reason: This task should have been completed already.");
                    Console.WriteLine("Today's date is: " + DateTime.Today.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Please enter today's date or a future date.");
                }
                else
                {
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

                // Check if user pressed Enter without typing anything
                if (string.IsNullOrWhiteSpace(priorityInput))
                {
                    Console.WriteLine("Error: Input cannot be empty. Please enter a number (1, 2, or 3).");
                    continue;
                }

                int priorityNumber;
                bool isNumber = int.TryParse(priorityInput, out priorityNumber);

                if (!isNumber)
                {
                    Console.WriteLine("Error: Please enter a number (1, 2, or 3).");
                }
                else if (priorityNumber < 1 || priorityNumber > 3)
                {
                    Console.WriteLine("Error: Invalid choice. Number must be between 1 and 3.");
                    Console.WriteLine("Reason: Only 3 priority levels are available.");
                    Console.WriteLine("Please enter 1 for Low, 2 for Medium, or 3 for High.");
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

                // Check if user pressed Enter without typing anything
                if (string.IsNullOrWhiteSpace(typeInput))
                {
                    Console.WriteLine("Error: Input cannot be empty. Please enter a number (1, 2, or 3).");
                    continue;
                }

                int typeNumber;
                bool isNumber = int.TryParse(typeInput, out typeNumber);

                if (!isNumber)
                {
                    Console.WriteLine("Error: Please enter a number (1, 2, or 3).");
                }
                else if (typeNumber < 1 || typeNumber > 3)
                {
                    Console.WriteLine("Error: Invalid choice. Number must be between 1 and 3.");
                    Console.WriteLine("Reason: Only 3 task types are available.");
                    Console.WriteLine("Please enter 1 for Assignment, 2 for Quiz, or 3 for Exam.");
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
            }

            return validTaskType;
        }

        // ========================================
        // HELPER METHOD: CHECK IF STRING IS ONLY NUMBERS
        // ========================================
        static bool IsOnlyNumbers(string input)
        {
            if (input == "")
            {
                return false;
            }

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        // ========================================
        // HELPER METHOD: CHECK FOR REPEATED CHARACTERS
        // ========================================
        static bool HasTooManyRepeatedChars(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }

            string uniqueChars = "";

            foreach (char c in input)
            {
                bool found = false;
                foreach (char unique in uniqueChars)
                {
                    if (c == unique)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    uniqueChars = uniqueChars + c;
                }
            }

            if (uniqueChars.Length < 2)
            {
                return true;
            }

            return false;
        }// HELPER METHOD: CHECK IF STRING IS ONLY NUMBERS
        static bool IsOnlyNumbers(string input)
        {
            if (input == "")
            {
                return false;
            }

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        // HELPER METHOD: CHECK FOR REPEATED CHARACTERS
        static bool HasTooManyRepeatedChars(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }

            string uniqueChars = "";

            foreach (char c in input)
            {
                bool found = false;
                foreach (char unique in uniqueChars)
                {
                    if (c == unique)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    uniqueChars = uniqueChars + c;
                }
            }

            if (uniqueChars.Length < 2)
            {
                return true;
            }

            return false;
        }

        // HELPER METHOD: CALCULATE PROGRESS MANUALLY
        static double CalculateProgressManually()
        {
            if (myPlanner.Items.Count == 0)
            {
                return 0.0;
            }

            int completedCount = 0;
            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                if (myPlanner.Items[i].IsCompleted)
                {
                    completedCount++;
                }
            }

            double totalTasks = myPlanner.Items.Count;
            double progress = (completedCount / totalTasks) * 100.0;

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
      // ========================================
        // SEARCH OPERATION
        // ========================================
        static void SearchTasks()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   SEARCH TASKS");
            Console.WriteLine("========================================");

            // Check if list is empty
            if (myPlanner.Items.Count == 0)
            {
                Console.WriteLine("No tasks found. The list is empty.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Get search keyword from user
            Console.Write("Enter search keyword (title or subject): ");
            string searchKeyword = Console.ReadLine();

            // Validate input
            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                Console.WriteLine("Error: Search keyword cannot be empty.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // First pass: count matching tasks
            int matchCount = 0;
            string lowerKeyword = searchKeyword.Trim().ToLower();

            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                PlannerItem currentTask = myPlanner.Items[i];
                string lowerTitle = currentTask.Title.ToLower();
                string lowerCategory = currentTask.Category.ToLower();

                // Check if keyword matches title or category
                if (lowerTitle.Contains(lowerKeyword) || lowerCategory.Contains(lowerKeyword))
                {
                    matchCount++;
                }
            }

            // Display results
            if (matchCount == 0)
            {
                Console.WriteLine("\nNo tasks found matching: " + searchKeyword);
            }
            else
            {
                // Create array to store search results
                PlannerItem[] searchResults = new PlannerItem[matchCount];
                int arrayIndex = 0;

                // Second pass: store matching tasks in array
                for (int i = 0; i < myPlanner.Items.Count; i++)
                {
                    PlannerItem currentTask = myPlanner.Items[i];
                    string lowerTitle = currentTask.Title.ToLower();
                    string lowerCategory = currentTask.Category.ToLower();

                    if (lowerTitle.Contains(lowerKeyword) || lowerCategory.Contains(lowerKeyword))
                    {
                        searchResults[arrayIndex] = currentTask;
                        arrayIndex++;
                    }
                }

                // Display results from array
                Console.WriteLine("\n*** SEARCH RESULTS ***");
                Console.WriteLine("Found " + searchResults.Length + " task(s):\n");

                for (int i = 0; i < searchResults.Length; i++)
                {
                    PlannerItem task = searchResults[i];
                    if (task.IsCompleted)
                    {
                        Console.Write("[DONE] ");
                    }
                    else
                    {
                        Console.Write("[TODO] ");
                    }
                    Console.WriteLine((i + 1) + ". " + task.GetDetails());
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ========================================
        // DELETE OPERATION
        // ========================================
        static void DeleteTask()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   DELETE TASK");
            Console.WriteLine("========================================");

            // Check if list is empty
            if (myPlanner.Items.Count == 0)
            {
                Console.WriteLine("No tasks found. The list is empty.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Display all tasks
            Console.WriteLine("All tasks:\n");
            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                PlannerItem currentTask = myPlanner.Items[i];
                Console.WriteLine((i + 1) + ". " + currentTask.Title + " (" + currentTask.Category + ") - " + currentTask.Date.ToString("dd/MM/yyyy"));
            }

            // Get task number from user
            Console.Write("\nEnter task number to delete (0 to cancel): ");
            string taskNumberInput = Console.ReadLine();

            // Validate input
            int selectedTaskNumber;
            bool isValidNumber = int.TryParse(taskNumberInput, out selectedTaskNumber);

            if (!isValidNumber)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            else if (selectedTaskNumber == 0)
            {
                Console.WriteLine("Delete operation cancelled.");
            }
            else if (selectedTaskNumber < 1 || selectedTaskNumber > myPlanner.Items.Count)
            {
                Console.WriteLine("Error: Task number out of range.");
            }
            else
            {
                // Confirm deletion
                PlannerItem taskToDelete = myPlanner.Items[selectedTaskNumber - 1];
                Console.WriteLine("\nAre you sure you want to delete this task?");
                Console.WriteLine(taskToDelete.Title + " (" + taskToDelete.Category + ")");
                Console.Write("Type 'YES' to confirm: ");
                string confirmation = Console.ReadLine();

                if (confirmation != null && confirmation.Trim().ToUpper() == "YES")
                {
                    // Delete task and save
                    myPlanner.Items.RemoveAt(selectedTaskNumber - 1);
                    SavePlannerData();

                    Console.WriteLine("\n*** SUCCESS! ***");
                    Console.WriteLine("Task deleted successfully!");
                }
                else
                {
                    Console.WriteLine("\nDelete operation cancelled.");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ========================================
        // EDIT OPERATION
        // ========================================
        static void EditTask()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   EDIT TASK");
            Console.WriteLine("========================================");

            // Check if list is empty
            if (myPlanner.Items.Count == 0)
            {
                Console.WriteLine("No tasks found. The list is empty.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Display all tasks
            Console.WriteLine("All tasks:\n");
            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                PlannerItem currentTask = myPlanner.Items[i];
                Console.WriteLine((i + 1) + ". " + currentTask.Title + " (" + currentTask.Category + ") - " + currentTask.Date.ToString("dd/MM/yyyy"));
            }

            // Get task number from user
            Console.Write("\nEnter task number to edit (0 to cancel): ");
            string taskNumberInput = Console.ReadLine();

            // Validate input
            int selectedTaskNumber;
            bool isValidNumber = int.TryParse(taskNumberInput, out selectedTaskNumber);

            if (!isValidNumber)
            {
                Console.WriteLine("Error: Please enter a valid number.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }
            else if (selectedTaskNumber == 0)
            {
                Console.WriteLine("Edit operation cancelled.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }
            else if (selectedTaskNumber < 1 || selectedTaskNumber > myPlanner.Items.Count)
            {
                Console.WriteLine("Error: Task number out of range.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Get task to edit
            PlannerItem oldTask = myPlanner.Items[selectedTaskNumber - 1];
            
            Console.WriteLine("\nEditing: " + oldTask.Title);
            Console.WriteLine("Current details: " + oldTask.GetDetails());

            // Get new values from user
            string newTitle = GetValidTitle();
            string newSubject = GetValidSubject();
            int newMinutes = GetValidMinutes();
            DateTime newDate = GetValidDate();
            Priority newPriority = GetValidPriority();

            try
            {
                PlannerItem newTask;

                // Create new task based on original type
                if (oldTask is StudySession)
                {
                    string newTopic = GetValidTopic();
                    newTask = new StudySession(
                        newDate,
                        newTitle,
                        newSubject,
                        newMinutes,
                        newPriority,
                        newTopic
                    );
                }
                else
                {
                    TaskType newTaskType = GetValidTaskType();
                    newTask = new DeadlineTask(
                        newDate,
                        newTitle,
                        newSubject,
                        newMinutes,
                        newTaskType,
                        newPriority
                    );
                }

                // Preserve completion status
                if (oldTask.IsCompleted)
                {
                    newTask.MarkCompleted();
                }

                // Replace old task with new one and save
                myPlanner.Items[selectedTaskNumber - 1] = newTask;
                SavePlannerData();

                Console.WriteLine("\n*** SUCCESS! ***");
                Console.WriteLine("Task updated successfully!");
                Console.WriteLine("New details: " + newTask.GetDetails());
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: Could not update the task.");
                Console.WriteLine("Reason: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ========================================
        // UPDATE STATUS OPERATION
        // ========================================
        static void UpdateTaskStatus()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   UPDATE TASK STATUS");
            Console.WriteLine("========================================");

            // Check if list is empty
            if (myPlanner.Items.Count == 0)
            {
                Console.WriteLine("No tasks found. The list is empty.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // Display all tasks with their status
            Console.WriteLine("All tasks:\n");
            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                PlannerItem currentTask = myPlanner.Items[i];
                string status = currentTask.IsCompleted ? "[COMPLETED]" : "[INCOMPLETE]";
                Console.WriteLine((i + 1) + ". " + status + " " + currentTask.Title + " (" + currentTask.Category + ")");
            }

            // Get task number from user
            Console.Write("\nEnter task number to update status (0 to cancel): ");
            string taskNumberInput = Console.ReadLine();

            // Validate input
            int selectedTaskNumber;
            bool isValidNumber = int.TryParse(taskNumberInput, out selectedTaskNumber);

            if (!isValidNumber)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            else if (selectedTaskNumber == 0)
            {
                Console.WriteLine("Update operation cancelled.");
            }
            else if (selectedTaskNumber < 1 || selectedTaskNumber > myPlanner.Items.Count)
            {
                Console.WriteLine("Error: Task number out of range.");
            }
            else
            {
                PlannerItem selectedTask = myPlanner.Items[selectedTaskNumber - 1];

                // Check if already completed
                if (selectedTask.IsCompleted)
                {
                    Console.WriteLine("\nThis task is already marked as completed.");
                    Console.Write("Do you want to mark it as incomplete? (YES/NO): ");
                    string response = Console.ReadLine();

                    if (response != null && response.Trim().ToUpper() == "YES")
                    {
                        Console.WriteLine("Note: Cannot unmark completed tasks in current system design.");
                        Console.WriteLine("Please delete and recreate the task if needed.");
                    }
                }
                else
                {
                    // Mark as completed using Planner method
                    bool success = myPlanner.MarkCompletedByIndex(selectedTaskNumber - 1);
                    
                    if (success)
                    {
                        Console.WriteLine("\n*** SUCCESS! ***");
                        Console.WriteLine("Task marked as completed: " + selectedTask.Title);
                    }
                    else
                    {
                        Console.WriteLine("Error: Could not update task status.");
                    }
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ========================================
        // SAVE DATA HELPER
        // ========================================
        static void SavePlannerData()
        {
            try
            {
                // Save all data to JSON file
                FileStorage storage = new FileStorage("planner-data.json");
                storage.Save(myPlanner.Items, myPlanner.WeeklyGoalMinutes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Warning: Could not save data.");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // ========================================
        // SHOW OVERDUE TASKS
        // ========================================
        static void ShowOverdueTasks()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   OVERDUE TASKS");
            Console.WriteLine("========================================");

            // Check if list is empty
            if (myPlanner.Items.Count == 0)
            {
                Console.WriteLine("No tasks found. The list is empty.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            // First pass: count overdue tasks
            int overdueCount = 0;
            DateTime today = DateTime.Today;

            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                PlannerItem currentTask = myPlanner.Items[i];
                if (currentTask.IsOverdue(today))
                {
                    overdueCount++;
                }
            }

            // Display results
            if (overdueCount == 0)
            {
                Console.WriteLine("Great! No overdue tasks found.");
            }
            else
            {
                // Create array to store overdue tasks
                PlannerItem[] overdueTasks = new PlannerItem[overdueCount];
                int arrayIndex = 0;

                // Second pass: store overdue tasks in array
                for (int i = 0; i < myPlanner.Items.Count; i++)
                {
                    PlannerItem currentTask = myPlanner.Items[i];
                    if (currentTask.IsOverdue(today))
                    {
                        overdueTasks[arrayIndex] = currentTask;
                        arrayIndex++;
                    }
                }

                // Display overdue tasks from array
                Console.WriteLine("WARNING: You have " + overdueTasks.Length + " overdue task(s):\n");

                for (int i = 0; i < overdueTasks.Length; i++)
                {
                    PlannerItem task = overdueTasks[i];
                    // Calculate days overdue
                    int daysOverdue = (today - task.Date.Date).Days;
                    Console.WriteLine((i + 1) + ". " + task.Title + 
                                    " (" + task.Category + ") - " + 
                                    daysOverdue + " day(s) overdue");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void SetWeeklyGoal()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   SET WEEKLY STUDY GOAL");
            Console.WriteLine("========================================");

            Console.WriteLine("Current weekly goal: " + myPlanner.WeeklyGoalMinutes + " minutes");
            Console.Write("\nEnter new weekly study goal (minutes): ");

            int goal = GetValidMinutes();
            myPlanner.WeeklyGoalMinutes = goal;
            SavePlannerData();

            Console.WriteLine("\n*** SUCCESS! ***");
            Console.WriteLine("Weekly goal set to: " + goal + " minutes (" + (goal / 60) + " hours)");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ShowUpcomingDeadlines()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   UPCOMING DEADLINES (NEXT 3 DAYS)");
            Console.WriteLine("========================================");

            if (myPlanner.Items.Count == 0)
            {
                Console.WriteLine("No tasks found. The list is empty.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            DateTime today = DateTime.Today;
            DateTime threeDaysLater = today.AddDays(3);

            int upcomingCount = 0;
            for (int i = 0; i < myPlanner.Items.Count; i++)
            {
                PlannerItem currentTask = myPlanner.Items[i];
                if (!currentTask.IsCompleted &&
                    currentTask.Date.Date >= today &&
                    currentTask.Date.Date <= threeDaysLater)
                {
                    upcomingCount++;
                }
            }

            if (upcomingCount == 0)
            {
                Console.WriteLine("No upcoming deadlines in the next 3 days.");
            }
            else
            {
                PlannerItem[] upcomingTasks = new PlannerItem[upcomingCount];
                int arrayIndex = 0;

                for (int i = 0; i < myPlanner.Items.Count; i++)
                {
                    PlannerItem currentTask = myPlanner.Items[i];
                    if (!currentTask.IsCompleted &&
                        currentTask.Date.Date >= today &&
                        currentTask.Date.Date <= threeDaysLater)
                    {
                        upcomingTasks[arrayIndex] = currentTask;
                        arrayIndex++;
                    }
                }

                Console.WriteLine("ALERT: You have " + upcomingTasks.Length + " upcoming deadline(s):\n");

                for (int i = 0; i < upcomingTasks.Length; i++)
                {
                    PlannerItem task = upcomingTasks[i];
                    int daysUntil = (task.Date.Date - today).Days;
                    string urgency = daysUntil == 0 ? "TODAY!" : daysUntil + " day(s) left";

                    Console.WriteLine((i + 1) + ". " + task.Title +
                                    " (" + task.Category + ") - " +
                                    task.Date.ToString("dd/MM/yyyy") + " - " + urgency);
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}