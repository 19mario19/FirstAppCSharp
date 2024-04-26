namespace FirstAppCSharp
{
    internal partial class Program
    {
        class Task
        {
            public int Id = 0;
            public string? name { get; set; }
            public bool? done = false;
            private static int Count = 0;

            public Task(string? _name)
            {
                name = _name;
                Id++;
                Count++;
            }

            public void Print()
            {
                Console.WriteLine($"Task name: {name} is done: {done}");
            }

            public int HowManyTasks()
            {
                Console.WriteLine($"{Count} tasks are created!");
                return Count;
            }

            public void TaskSwitch()
            {
                done = !done;
            }

            public void TaskDone()
            {
                done = true;
            }
            public void TaskNotDone()
            {
                done = false;
            }

        }
        class ToDo
        {
            List<Task> list = new List<Task>();
            public int HowManyLists = 0;
            public ToDo()
            {
                HowManyLists++;
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
                    Console.WriteLine($"Task with Id {Id} not found.");
                }
            }

        }


        internal static void Main(string[] args)
        {

            Task task = new Task("fasdf", false);
            task.TaskSwitch();
            task.Print();
            task.TaskSwitch();
            task.Print();

            // Create a to do list 



        }
    }
}

