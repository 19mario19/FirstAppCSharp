namespace FirstAppCSharp
{
    internal class Program
    {

        class MadLib
        {
            public string[] Adjectives = new string[9] { "amazing", "exciting", "adventurous", "thrilling", "daring", "wonderful", "fantastic", "incredible", "marvelous" };
            public string[] Nouns = new string[8] { "hero", "explorer", "adventurer", "traveler", "journey", "quest", "voyage", "expedition" };
            public string[] Verbs = new string[3] { "embark", "venture", "discover" };
            public string[] PluralNouns = new string[2] { "treasures", "wonders" };
            public string? Exclamation { get; set; } = "Awesome!";
            public string? Name { get; set; } = "Brave Explorer";

            public void AddData()
            {
                Console.Write("Write your name: ");
                string YourName = Console.ReadLine();
                if (string.IsNullOrEmpty(YourName))
                {
                    Name = YourName;
                }

                Console.WriteLine("Adding adjectives:");
                for (int i = 0; i < Adjectives.Length; i++)
                {
                    Console.Write($"Adjective {i} adding: ");
                    string value = Console.ReadLine();
                    if (!string.IsNullOrEmpty(value))
                    {
                        Adjectives[i] = value;
                    }
                }
                Console.WriteLine("Adding nouns:");
                for (int i = 0; i < Nouns.Length; i++)
                {
                    Console.Write($"Noun {i} adding: ");
                    string value = Console.ReadLine();
                    if (!string.IsNullOrEmpty(value))
                    {
                        Nouns[i] = value;
                    }
                }
                Console.WriteLine("Adding verbs:");
                for (int i = 0; i < Verbs.Length; i++)
                {
                    Console.Write($"Verb {i} adding: ");
                    string value = Console.ReadLine();
                    if (!string.IsNullOrEmpty(value))
                    {
                        Verbs[i] = value;
                    }
                }
                Console.WriteLine("Adding plural nouns:");
                for (int i = 0; i < PluralNouns.Length; i++)
                {
                    Console.Write($"Plural noun {i} adding: ");
                    string value = Console.ReadLine();
                    if (!string.IsNullOrEmpty(value))
                    {
                        PluralNouns[i] = value;
                    }
                }
                Console.WriteLine("Adding exclamation:");
                string value2 = Console.ReadLine();
                if (!string.IsNullOrEmpty(value2))
                {
                    Exclamation = value2;
                }


            }

            public void FormatText()
            {
                Console.WriteLine("The Magical Adventure");
                Console.WriteLine($@"
\----

One {Adjectives[0]} day, a {Nouns[0]} named {Name} decided to go on a {Adjectives[1]} adventure.
They packed their {Nouns[1]} andset off into the {Adjectives[2]} forest.
As they were {Verbs[0]} through the trees, they stumbled upon a {Adjectives[3]} {Nouns[2]}.
It was a magical {Nouns[3]} that could grant {PluralNouns[0]}!
""{Exclamation},"" said {Name}. ""I wish for {PluralNouns[1]}!""
Suddenly, {PluralNouns[0]} appeared out of nowhere and began {Verbs[1]} around them. 
{Name} continued their journey and came across a {Adjectives[4]} {Nouns[4]}.
Inside, they found a {Adjectives[5]} {Nouns[5]} that could {Verbs[2]}.
After {Verbs[0]} with the {Nouns[5]}, {Name} felt {Adjectives[6]} and decided to head back home.
On their way back, they encountered a {Adjectives[7]} {Nouns[6]} that could {Verbs[2]}.
{Name} {Verbs[2]} it and continued their journey.
When they finally arrived home, {Name} realized that their {Adjectives[6]} adventure had been {Adjectives[7]} and {Adjectives[8]}.

\-----
");
            }
        }



        internal static void Main(string[] args)
        {


            MadLib matlib = new MadLib();
            Console.WriteLine("Add your data?");
            Console.WriteLine("Y or Yes for introducing own data, anything else for defaults");
            string Question = Console.ReadLine();
            if (!string.IsNullOrEmpty(Question))
            {
                if (Question.ToLower() == "yes" || Question.ToLower() == "y")
                {
                    matlib.AddData();
                }
            }
            matlib.FormatText();

        }
    }
}

