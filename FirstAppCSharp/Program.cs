using System.Globalization;

namespace FirstAppCSharp
{
    internal partial class Program
    {
        internal class Task
        {
            public int Id { get; }

            public string? name { get; set; }

            public bool? done = false;

            public static int Count
            {
                get { return taskList.Count; }
            }

            public static List<Task> taskList = new List<Task>();

            public static int nextInt = 1;

            public Task(string? _name, int assignedId = -1, bool assignedDone = false)
            {
                name = _name;
                if (assignedId > -1 && assignedDone)
                {
                    Id = assignedId;
                    done = assignedDone;
                }
                else
                {

                    Id = nextInt++;
                }
                taskList.Add(this);
            }

            public static void PrintAllTasks()
            {
                foreach (Task task in taskList)
                {
                    Console.WriteLine($"{task.Id}. {task.name} - {task.done}");

                }
                Console.WriteLine($"Number of tasks are: {taskList.Count}");
            }
        }

        internal class ToDo
        {
            public int Id { get; }
            public string? Name { get; set; }

            public static List<string> listNames = new();

            public static List<ToDo> AllToDoLists = new();

            public List<Task> list = new();

            public static int HowManyLists = 0;

            private static int nextId = 1;

            public ToDo(string? _Name, int assignedId = -1)
            {
                if (assignedId < -1)
                {

                    Id = assignedId;
                }
                else
                {
                    Id = nextId++;
                    HowManyLists++;

                }
                Name = _Name;
                if (!string.IsNullOrEmpty(Name)) listNames.Add(Name);
                AllToDoLists.Add(this);
            }

            public static void PrintAllLists()
            {
                foreach (ToDo toDo in AllToDoLists)
                {
                    Console.WriteLine($"\nID: {toDo.Id}. {toDo.Name?.ToUpper()}\n");

                    toDo.list.ForEach(task => Console.WriteLine($"{task.Id,3}. {task.name,-15} - {task.done}"));
                }
                Console.WriteLine();
            }

            public void AddTask(string name, int assignedId = -1, bool assignedDone = false)
            {
                if (assignedId > -1 && assignedDone) list.Add(new Task(name, assignedId, assignedDone));

                list.Add(new Task(name));
            }

            public void RemoveTask(int Id)
            {
                // Find the task with the specified Id
                Task? taskToRemove = list.Find(t => t.Id == Id);

                if (taskToRemove != null)
                {
                    list.Remove(taskToRemove);
                }
                else
                {
                    Console.WriteLine($"Task with Id {Id} not found in '{Name}' list.");
                    Console.WriteLine("Id' that are avialable are: ");
                    list.ForEach(el =>
                    {
                        Console.Write($"{Id} ");
                    });
                }
            }

            public void SwitchDoneById(int Id)
            {
                // Find the task with the specified Id
                Task? taskToUpdate = list.Find(t => t.Id == Id);

                if (taskToUpdate != null)
                {
                    taskToUpdate.done = !taskToUpdate.done;
                }
                else
                {
                    Console.WriteLine($"Task with Id {Id} not found in '{Name}' list.");
                    Console.WriteLine("Id' that are avialable are: ");
                    list.ForEach(el =>
                    {
                        Console.Write($"{el.Id} ");
                    });
                    Console.WriteLine("\n");
                }
            }

            public void AllDone()
            {
                list.ForEach(el => el.done = true);
            }

            public void AllFalse()
            {
                list.ForEach(el => el.done = false);
            }

            public static string fileName = $"todo-backup.txt";
            public static void SaveToFile()
            {
                // Getting the date using en-GB format
                CultureInfo cultureInfo = new CultureInfo("en-GB");
                DateTime dateTime = DateTime.Now;
                string dateNow = dateTime.ToString(cultureInfo);
                Console.WriteLine($"Saved at {dateNow}");


                try
                {
                    using (StreamWriter writer = new StreamWriter(fileName, false))
                    {
                        foreach (ToDo todo in AllToDoLists)
                        {
                            writer.WriteLine($"List:{todo.Name}:{todo.Id}");

                            foreach (Task task in todo.list)
                            {
                                writer.WriteLine($"Task:{task.Id}:{task.name}:{task.done}");
                            }

                            writer.WriteLine();
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while saving data: {ex.Message}");
                }


            }
            public static List<ToDo> ReadFromFile()
            {
                AllToDoLists.Clear();
                List<ToDo> restoredList = new List<ToDo>();

                try
                {
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string? line;
                        ToDo? currentToDo = null;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith("List:"))
                            {
                                // Extract ToDo information from the line
                                string[] listParts = line.Split(':');
                                string name = listParts[1];
                                int id = int.Parse(listParts[2]);

                                // Create a new ToDo instance
                                currentToDo = new ToDo(name, id);
                                restoredList.Add(currentToDo);

                                // Update the nextId static variable
                                ToDo.nextId = Math.Max(ToDo.nextId, id + 1);

                            }
                            else if (line.StartsWith("Task:") && currentToDo != null)
                            {
                                // Extract Task information from the line
                                string[] taskParts = line.Split(':');
                                int taskId = int.Parse(taskParts[1]);
                                string taskName = taskParts[2];
                                bool taskDone = bool.Parse(taskParts[3]);

                                // Add the task information to the current ToDo (but don't create new Task instances here)
                                currentToDo.list.Add(new Task(taskName, taskId, taskDone));
                            }
                            else if (string.IsNullOrWhiteSpace(line))
                            {
                                // Empty line indicates separation between lists

                                currentToDo = null;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while reading data: {ex.Message}");
                }

                return restoredList;
            }
            public static void SyncToDo()
            {
                AllToDoLists = ReadFromFile();
            }
            public static void DeleteFile()
            {
                File.Delete(fileName);
            }
            public static void Initialize()
            {
                Task.nextInt = 1;
                ToDo.nextId = 1;
                ToDo.AllToDoLists.Clear();

            }

        }

        internal static void Main(string[] args)
        {


            ToDo.Initialize();
            ToDo.SyncToDo();




            editMode();

        }

        internal class Command
        {
            public string? Name { get; set; }

            public string? Description { get; set; }

            public Command(string _Name, string _Description)
            {
                Name = _Name;
                Description = _Description;
            }
        }

        internal class Commands
        {
            public Command[] list = new Command[] {
        new Command("0", "New list"),
        new Command("1", "Add task"),
        new Command("2", "Mark done"),
        new Command("3", "All done"),
        new Command("4", "All undone"),
        new Command("5", "Del list"),
        new Command("6", "Del all"),
        new Command("p", "Print"),
        new Command("e", "Exit")
    };
        }

        public static void editMode()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Create your todos!");
            Console.ResetColor();
            ToDo.PrintAllLists();
            Console.WriteLine("Here are the commands you can use: ");

            while (true)
            {
                ShowCommands();
                ToDo.SaveToFile();
                int number = ToDo.AllToDoLists.Count;
                Console.WriteLine("Total lists: " + number);
                int howManyTasks = Task.taskList.Count;
                Console.WriteLine("Total tasks: " + howManyTasks);
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Type a command: ");
                Console.ResetColor();
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    if (input == "e")
                    {
                        break;
                    }

                    else if (input == "p")
                    {
                        ToDo.PrintAllLists();
                    }
                    else
                    {
                        switch (input)
                        {
                            case "0":
                                Console.Write("You are creating a new list, give it a name: ");
                                string? name = Console.ReadLine();
                                if (!string.IsNullOrEmpty(name))
                                {
                                    ToDo newToDo = new(name);
                                    Console.WriteLine($"\nYou created a new list with name '{newToDo.Name}'");


                                }
                                break;
                            case "5":
                                ShowCurrentList("You are deleting a list, choose the name: ");
                                string? toDoInputRemove = Console.ReadLine();
                                if (!string.IsNullOrEmpty(toDoInputRemove))
                                {
                                    for (int i = 0; i < ToDo.AllToDoLists.Count; i++)
                                    {
                                        ToDo toDo = ToDo.AllToDoLists[i];
                                        if (toDo.Name == toDoInputRemove)
                                        {
                                            Console.WriteLine($"List with name '{toDo.Name}' will be removed!");

                                            for (int j = 0; j < toDo.list.Count; j++)
                                            {
                                                Task task = toDo.list[j];
                                                Task.taskList.Remove(task);
                                            }
                                            {

                                            }

                                            ToDo.AllToDoLists.Remove(toDo);
                                            ToDo.listNames.Remove(toDo.Name);
                                            Console.WriteLine($"List '{toDoInputRemove}' removed!");
                                        }
                                    }
                                    Console.WriteLine();

                                }
                                break;
                            case "6":
                                ShowCurrentList("You are deleting all lists!");
                                ToDo.listNames.RemoveAll(toDo => toDo != "");
                                ToDo.AllToDoLists.RemoveAll(toDo => toDo.Name != "");
                                Task.taskList.RemoveAll(task => task.name != "");
                                ToDo.DeleteFile();

                                Console.WriteLine();

                                break;
                            case "1":
                                ShowCurrentList("Here are the current lists in which you can add a task: ");
                                string listIndexString = InputWithDescription("Type the ID of the list: ");
                                int listIndex = Convert.ToInt32(listIndexString);
                                ToDo.AllToDoLists.ForEach(list =>
                                {
                                    if (listIndex != 0)
                                    {
                                        if (listIndex == list.Id)
                                        {
                                            Console.WriteLine($"Found list with name '{list.Name}'");

                                            while (true)
                                            {
                                                string? newTask = InputWithDescription($"Add task to '{list.Name}' / 'e' to exit : ");
                                                if (newTask == "e")
                                                {
                                                    break;
                                                }

                                                if (!string.IsNullOrEmpty(newTask))
                                                {
                                                    list.AddTask(newTask);

                                                }
                                            }
                                        }
                                    }
                                });

                                break;
                            case "2":
                                string inputIdString = InputWithDescription("Introduce a task ID to mark as 'done': ");
                                int inputId = Convert.ToInt32(inputIdString);
                                if (inputId != 0)
                                {
                                    foreach (ToDo toDo in ToDo.AllToDoLists)
                                    {
                                        foreach (Task task in toDo.list)
                                        {
                                            if (task.Id != inputId)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                task.done = true;
                                                Console.WriteLine(task.name + " ," + task.done);
                                            }
                                        }
                                    }

                                }
                                break;
                            case "3":
                                ShowCurrentList("");
                                string toDoInput = InputWithDescription("Type ID of the list you want to mark all done: ");
                                int toDoInputIndex = Convert.ToInt32(toDoInput);

                                if (toDoInputIndex != 0)
                                {
                                    foreach (ToDo toDo in ToDo.AllToDoLists)
                                    {
                                        if (toDo.Id == toDoInputIndex)
                                        {
                                            toDo.AllDone();
                                        }
                                    }
                                }

                                break;
                            case "4":
                                ShowCurrentList("");
                                string toDoInputUndone = InputWithDescription("Type the name of the list you want to mark all un-done: ");
                                int toDoInputIndexUndone = Convert.ToInt32(toDoInputUndone);

                                if (toDoInputIndexUndone != 0)
                                {
                                    foreach (ToDo toDo in ToDo.AllToDoLists)
                                    {
                                        if (toDo.Id == toDoInputIndexUndone)
                                        {
                                            toDo.AllFalse();
                                        }
                                    }
                                }

                                break;

                            default:
                                Console.WriteLine("Add a valid command");
                                break;
                        }

                    }
                }
            }

            static string InputWithDescription(string description)
            {
                Console.Write(description);
                string? inputValue = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputValue))
                {

                    return inputValue;
                }
                return "";
            }

            static void ShowCurrentList(string notification)
            {
                if (!string.IsNullOrEmpty(notification))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(notification);
                    Console.ResetColor();
                }
                Console.WriteLine("Lists names are : ");
                ToDo.AllToDoLists.ForEach(el => Console.WriteLine($"{el.Id}. '{el.Name}'"));
            }
        }

        internal static void ShowCommands()
        {

            Commands commands = new Commands();

            Console.WriteLine();
            foreach (Command command in commands.list)
            {
                if (command.Name == "e")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (command.Name == "p")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.Write(command.Name);

                Console.ForegroundColor = ConsoleColor.Blue;

                Console.Write($":{command.Description} ");

                Console.ResetColor();
            }
            Console.WriteLine("\n");
        }
    }
}
