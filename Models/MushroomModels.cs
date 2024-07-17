
using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MushroomPocket
{
    public class MushroomMaster
    {
        public string Name { get; set; }
        public int NoToTransform { get; set; }
        public string TransformTo { get; set; }

        public MushroomMaster(string name, int noToTransform, string transformTo)
        {
            this.Name = name;
            this.NoToTransform = noToTransform;
            this.TransformTo = transformTo;
        }
    }
    public abstract class Character
    {
        [Key]
        public int Id { get; set; }
        public int Hp { get; set; }
        public int Exp { get; set; }
        public string Skill { get; set; }

        protected Character(int hp, int exp)
        {
            Hp = hp;
            Exp = exp;
        }

        public abstract void PrintInfo();

        
    }

    public class Waluigi : Character
    {
        public Waluigi(int hp, int exp) : base(hp, exp)
        {
            Skill = "Agility";
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"------------------------");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: Waluigi ");
            Console.WriteLine($"Hp: {Hp}");
            Console.WriteLine($"Exp: {Exp}");
            Console.WriteLine($"Skill: {Skill}");
            Console.WriteLine("------------------------");
        }
    }

    public class Daisy : Character
    {
        public Daisy(int hp, int exp) : base(hp, exp)
        {
            Skill = "Leadership";
        }

        public override void PrintInfo()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: Daisy");
            Console.WriteLine($"Hp: {Hp}");
            Console.WriteLine($"Exp: {Exp}");
            Console.WriteLine($"Skill: {Skill}");
            Console.WriteLine("------------------------");
        }
    }

    public class Wario : Character
    {
        public Wario(int hp, int exp) : base(hp, exp)
        {
            Skill = "Strength";
        }

        public override void PrintInfo()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: Wario");
            Console.WriteLine($"Hp: {Hp}");
            Console.WriteLine($"Exp: {Exp}");
            Console.WriteLine($"Skill: {Skill}");
            Console.WriteLine("------------------------");
        }
    }

    public class Luigi : Character
    {
        public Luigi(int hp, int exp) : base(hp, exp)
        {
            Skill = "Precision and Accuracy";
        }

        public override void PrintInfo()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: Luigi");
            Console.WriteLine($"Hp: {Hp}");
            Console.WriteLine($"Exp: {Exp}");
            Console.WriteLine($"Skill: {Skill}");
            Console.WriteLine("------------------------");
        }
    }

    public class Peach : Character
    {
        public Peach(int hp, int exp) : base(hp, exp)
        {
            Skill = "Magic Abilities";
        }

        public override void PrintInfo()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: Peach");
            Console.WriteLine($"Hp: {Hp}");
            Console.WriteLine($"Exp: {Exp}");
            Console.WriteLine($"Skill: {Skill}");
            Console.WriteLine("------------------------");
        }
    }

    public class Mario : Character
    {
        public Mario(int hp, int exp) : base(hp, exp)
        {
            Skill = "Combat Skills";
        }

        public override void PrintInfo()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: Mario");
            Console.WriteLine($"Hp: {Hp}");
            Console.WriteLine($"Exp: {Exp}");
            Console.WriteLine($"Skill: {Skill}");
            Console.WriteLine("------------------------");
        }
    }
}
