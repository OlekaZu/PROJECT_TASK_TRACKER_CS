#pragma warning  disable CS8509

using TaskTracker.Tasks;

namespace TaskTracker
{
    internal class Menu
    {
        public List<TaskTracker.Tasks.Task> allTasks { get; set; } = new List<TaskTracker.Tasks.Task>();

        private void PrintError()
        {
            Console.WriteLine("Input error. Check the input data and repeat the request.");
        }
        public int RunProgram()
        {
            int flag = 0;
            Console.WriteLine("Enter commands(add, list, done, wontdo, quit/q):");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "add":
                    Console.WriteLine("Enter a title");
                    string? title = Console.ReadLine();
                    while (string.IsNullOrEmpty(title))
                    {
                        Console.WriteLine("Title can't be empty. Enter a title again");
                        title = Console.ReadLine();
                    }

                    Console.WriteLine("Enter a description");
                    string? summary = Console.ReadLine();
                    if (summary is null)
                    {
                        flag = 1;
                        return flag;
                    }

                    Console.WriteLine("Enter the deadline");
                    string? deadlineStr = Console.ReadLine();
                    DateTime deadline;
                    while (DateTime.TryParse(deadlineStr, out deadline) == false && string.IsNullOrEmpty(deadlineStr) == false)
                    {
                        Console.WriteLine("Error date. Enter the deadline again or pass this stage");
                        deadlineStr = Console.ReadLine();
                    }

                    Console.WriteLine("Enter the type: Work, Study or Personal");
                    string? typeStr = Console.ReadLine();
                    while (typeStr != "Work" && typeStr != "Study" && typeStr != "Personal" || string.IsNullOrEmpty(typeStr))
                    {
                        Console.WriteLine("Error type. Enter type again.");
                        typeStr = Console.ReadLine();
                    }

                    TaskType type = typeStr switch
                    {
                        "Work" => TaskType.Work,
                        "Study" => TaskType.Study,
                        "Personal" => TaskType.Personal
                    };


                    Console.WriteLine("Assign the priority: High, Normal, Low or pass this stage");
                    string? priorityStr = Console.ReadLine();
                    while (priorityStr != "High" && priorityStr != "Normal" && priorityStr != "Low" && string.IsNullOrEmpty(priorityStr) == false)
                    {
                        Console.WriteLine("Error priority. Enter priority again or pass this stage.");
                        priorityStr = Console.ReadLine();
                    }

                    TaskPriority priority = priorityStr switch
                    {
                        "High" => TaskPriority.High,
                        "Normal" => TaskPriority.Normal,
                        "Low" => TaskPriority.Low,
                        _ => TaskPriority.Normal
                    };
                    TaskTracker.Tasks.Task newRecord = new(title, summary, deadline, type, priority);
                    allTasks.Add(newRecord);
                    Console.WriteLine(newRecord.ToString());
                    break;
                case "list":
                    if (allTasks.Count == 0)
                    {
                        Console.WriteLine("The task list is still empty.");
                    }
                    else
                    {
                        foreach (var element in allTasks)
                            Console.WriteLine($"{element}");
                    }
                    break;
                case "done":
                    Console.WriteLine("Enter a title");
                    string? inputC = Console.ReadLine();
                    while (string.IsNullOrEmpty(inputC))
                    {
                        Console.WriteLine("Try again");
                        inputC = Console.ReadLine();
                    }
                    if (allTasks.Count == 0)
                        Console.WriteLine("The task list is still empty.");
                    foreach (var element in allTasks)
                    {
                        var foundTask = allTasks.Where(n => n.Title == inputC).FirstOrDefault();
                        if (foundTask != null && foundTask.Status != TaskState.Irrelevant)
                        {
                            int index = allTasks.IndexOf(foundTask);
                            allTasks[index].Status = TaskState.Completed;
                            Console.WriteLine("The task [{0}] is completed!", foundTask.Title);
                            break;
                        }
                        else
                        {
                            PrintError();
                            break;
                        }
                    }
                    break;
                case "wontdo":
                    Console.WriteLine("Enter a title");
                    string? inputI = Console.ReadLine();
                    while (string.IsNullOrEmpty(inputI))
                    {
                        Console.WriteLine("Try again");
                        inputI = Console.ReadLine();
                    }
                    if (allTasks.Count == 0)
                        Console.WriteLine("The task list is still empty.");
                    foreach (var element in allTasks)
                    {
                        var foundTask = allTasks.Where(n => n.Title == inputI).FirstOrDefault();
                        if (foundTask != null && foundTask.Status != TaskState.Irrelevant && foundTask.Status != TaskState.Completed)
                        {
                            int index = allTasks.IndexOf(foundTask);
                            allTasks[index].Status = TaskState.Irrelevant;
                            Console.WriteLine("The task [{0}] is no longer relevant!", foundTask.Title);
                            break;
                        }
                        else
                        {
                            PrintError();
                            break;
                        }
                    }
                    break;
                case "quit" or "q":
                    flag = 1;
                    break;
                default:
                    PrintError();
                    break;
            };
            return flag;
        }
    }
}
