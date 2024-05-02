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

            public Task(string? _name)
            {
                name = _name;
                Id = nextInt++;
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

            public static List<string> listNames = new();

            public static List<ToDo> AllToDoLists = new();

            public string? Name { get; set; }

            public List<Task> list = new();

            public static int HowManyLists = 0;

            private static int nextId = 1;

            public ToDo(string? _Name)
            {
                Id = nextId++;
                HowManyLists++;
                Name = _Name;
                if (!string.IsNullOrEmpty(Name)) listNames.Add(Name);
                AllToDoLists.Add(this);
            }

            public static void PrintAllLists()
            {
                Console.WriteLine("\n");
                foreach (ToDo toDo in AllToDoLists)
                {
                    Console.WriteLine($"\nList with name '{toDo.Name}', ID: {toDo.Id} ");

                    toDo.list.ForEach(task => Console.WriteLine($"{task.Id}.{"",-1}{task.name,-15} - {task.done}"));
                }
                Console.WriteLine("\n");
            }

            public void AddTask(string name)
            {
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
        }

        internal static void Main(string[] args)
        {

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
            Console.WriteLine("Here are the commands you can use: ");

            while (true)
            {
                ShowCommands();
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
                                    Console.WriteLine();

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
                if (inputValue != null)
                {

                    return inputValue;
                }
                return "";
            }

            static void ShowCurrentList(string notification)
            {
                if (notification != null)
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
                else if (command.Name == "h")
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
