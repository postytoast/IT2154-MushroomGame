using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
namespace MushroomPocket
{
    class Program
    {
        //Used to compare charcter name input to type
        public static readonly Dictionary<string, Type> characterTypes = new Dictionary<string, Type>()
        {
            { "waluigi", typeof(Waluigi) },
            { "daisy", typeof(Daisy) },
            { "wario", typeof(Wario) },
            { "luigi", typeof(Luigi) },
            { "peach", typeof(Peach) },
            { "mario", typeof(Mario) },
        };

        //prints the menu
        public static void PrintMenu()
        {
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("Welcome to Mushroom Pocket App");
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("(1). Add Mushroom's character to my pocket");
            Console.WriteLine("(2). List character(s) in my Pocket");
            Console.WriteLine("(3). Check if I can transform my characters");
            Console.WriteLine("(4). Transform character(s)");
            Console.WriteLine("(5). Remove a character from my pocket");
            Console.WriteLine("(6). Edit a character from my pocket");
            Console.WriteLine("Enter (q) to quit");
        }

        //Read the choice of the user at the menu and prevents any invalid inputs
        public static char ReadChoice()
        {
            while (true)
            {
                Console.Write("Enter (1,2,3,4,5,6) or q to quit: ");
                var choices = new List<char> { '1', '2', '3', '4', '5', '6', 'q' };
                if (!char.TryParse(Console.ReadLine(), out char choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    continue;
                }

                choice = char.ToLower(choice);
                if (!choices.Contains(choice))
                {
                    Console.WriteLine("Invalid option. Please enter again.");
                    continue;
                }

                return choice;
            }
        }
        
        //Check whether the charcter name is part of the 3 allowed
        public static string CheckCharacterName()
        {
            while (true)
            {
                Console.Write("Enter Character's Name: ");
                string name = Console.ReadLine().ToLower();
                if(name != "mario" && name != "luigi" && name != "peach"){
                if (characterTypes.ContainsKey(name))
                {
                    return name;
                }
                else
                {
                    Console.WriteLine("Invalid character name. Only enter Waluigi, Daisy, Wario.");
                }}
                else{
                    Console.WriteLine("Invalid character name. Only enter Waluigi, Daisy, Wario.");
                }
            }
        }
        //Checks if user input hp is valid
        public static int CheckCharacterHp()
        {
            while (true)
            {
                Console.Write("Enter Character's HP: ");
                if (int.TryParse(Console.ReadLine(), out int hp) && hp > 0)
                {
                    return hp;
                }
                else
                {
                    Console.WriteLine("Invalid HP. Please enter a positive integer.");
                }
            }
        }


        //check if user input of exp is valid
        public static int CheckCharacterExp()
        {
            while (true)
            {
                Console.Write("Enter Character's EXP: ");
                if (int.TryParse(Console.ReadLine(), out int exp) && exp >= 0)
                {
                    return exp;
                }
                else
                {
                    Console.WriteLine("Invalid EXP. Please enter a non-negative integer.");
                }
            }
        }
        //check if both inputs of hp and exp in user input is valid
        private static int CheckEditCharacterInput(string prompt, int currentValue)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    return currentValue;
                }
                if (int.TryParse(input, out int value) && value > 0)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Please enter a valid value");
                }
            }
        }

        //Add character to db
        public static void AddCharacter(MushroomContext db)
        {
            string name = CheckCharacterName();
            int hp = CheckCharacterHp();
            int exp = CheckCharacterExp();

            Type characterType = characterTypes[name];
            Character character = (Character)Activator.CreateInstance(characterType, hp, exp);

            db.Characters.Add(character);
            db.SaveChanges();

            Console.WriteLine($"{name} has been added to the database.");
        }
        //Edit Character using id
        public static void EditCharacter(MushroomContext db)
        {
            Console.WriteLine("Enter the Character's ID you want to edit:");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var character = db.Characters.Find(id);
                if (character != null)
                {
                    Console.WriteLine("Editing character:");
                    character.PrintInfo();

                    int newHp = CheckEditCharacterInput("Enter new HP (or press Enter to keep current): ", character.Hp);
                    int newExp = CheckEditCharacterInput("Enter new EXP (or press Enter to keep current): ", character.Exp);

                    character.Hp = newHp;
                    character.Exp = newExp;

                    db.SaveChanges();
                    Console.WriteLine("Character successfully updated");
                }
                else
                {
                    Console.WriteLine("Character does not exist");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID");
            }
        }
        // List all charcters 
        public static void ListCharacters(MushroomContext db)
        {
            var characters = db.Characters.ToList();
            if (characters.Any())
            {
                foreach (var character in characters)
                {
                    character.PrintInfo();
                }
            }
            else
            {
                Console.WriteLine("No characters in the pocket.");
            }
        }
        //Removes charcter from db using id
        public static void RemoveCharacter(MushroomContext db)
        {
            Console.Write("Enter the ID of the character to remove: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var character = db.Characters.Find(id);
                if (character != null)
                {
                    db.Characters.Remove(character);
                    db.SaveChanges();
                    Console.WriteLine($"{character.GetType()} removed.");
                }
                else
                {
                    Console.WriteLine("Character not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }
        // Check transformation of current characters
        public static void CheckTransformations(List<MushroomMaster> mushroomMasters, List<Character> characters)
        {
            foreach (var mushroomMaster in mushroomMasters)
            {
                int occurrences = characters.Count(character => character.GetType() == characterTypes[mushroomMaster.Name]);
                if (occurrences >= mushroomMaster.NoToTransform)
                {
                    int possibleTransformations = occurrences / mushroomMaster.NoToTransform;
                    for (int i = 0; i < possibleTransformations; i++)
                    {
                        Console.WriteLine($"{mushroomMaster.Name} --> {mushroomMaster.TransformTo}");
                    }
                }
            }
        }
        //Transforms characters that can be transformed
        public static void TransformCharacters(List<MushroomMaster> mushroomMasters, MushroomContext db)
        {
            var characters = db.Characters.ToList();

            foreach (var mushroomMaster in mushroomMasters)
            {
                var charactersToTransform = characters.Where(c => c.GetType() == characterTypes[mushroomMaster.Name]).ToList();
                int transformCount = charactersToTransform.Count / mushroomMaster.NoToTransform;

                if (transformCount > 0)
                {
                    var charactersToRemove = charactersToTransform.Take(transformCount * mushroomMaster.NoToTransform).ToList();

                    foreach (var character in charactersToRemove)
                    {
                        db.Characters.Remove(character);
                        characters.Remove(character);
                    }

                    for (int i = 0; i < transformCount; i++)
                    {
                        Type characterType = characterTypes[mushroomMaster.TransformTo];
                        var newCharacter = (Character)Activator.CreateInstance(characterType, 100, 0);
                        db.Characters.Add(newCharacter);
                    }
                }
            }

            db.SaveChanges();
        }
        

        static void Main(string[] args)
        {
            using (var db = new MushroomContext())
            {
                db.Database.EnsureCreated();

                var mushroomMasters = new List<MushroomMaster>
                {
                    new MushroomMaster("daisy", 2, "peach"),
                    new MushroomMaster("wario", 3, "mario"),
                    new MushroomMaster("waluigi", 1, "luigi")
                };

                bool running = true;
                while (running)
                {
                    PrintMenu();
                    char choice = ReadChoice();

                    switch (choice)
                    {
                        case '1':
                            AddCharacter(db);
                            break;
                        case '2':
                            ListCharacters(db);
                            break;
                        case '3':
                            var characters = db.Characters.ToList();
                            CheckTransformations(mushroomMasters, characters);
                            break;
                        case '4':
                            TransformCharacters(mushroomMasters, db);
                            break;
                        case '5':
                            RemoveCharacter(db);
                            break;
                        case '6':
                            EditCharacter(db);
                            break;
                        case 'q':
                            Console.WriteLine("Goodbye!");
                            running = false;
                            break;
                    }
                }
            }
        }
    }
}
