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
}