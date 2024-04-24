namespace FirstAppCSharp
{
    internal class Program
    {


        internal static void Main(string[] args)
        {

            Person person = new Person();
            person.Name = "Alina";
            if (person.Name != "Alina")
            {
                person.Name = "Random";
            }
            else
            {
                Console.WriteLine(person.Name);

            }
        }
    }
}
