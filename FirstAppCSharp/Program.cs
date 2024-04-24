namespace FirstAppCSharp
{
    class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        private string? _name = "SECRET";

        public string getPrivateName()
        {
            if(Name != null)
            {
                return _name;
            }
            return null;
        }

        public void Greeting()
        {
            Console.WriteLine($"My name is {Name} and I am {Age} years old!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.Name = "Alina";
            
            person.Age = 12;

            Console.WriteLine(person.Name);
            int number = 5;
            if(number == 3)
            {

            Console.WriteLine("Hello, World!");
            } else
            {
                Console.WriteLine($"And the private name is {person.getPrivateName()}");
                Console.WriteLine("Is different than three!");
                person.Greeting();
            }
        }
    }
}