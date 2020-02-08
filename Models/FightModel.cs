using System;
using System.Collections.Generic;
using System.Linq;

namespace e7_tower_scouting.Models
{
    public class Fight
    {
        public Fight()
        {
            One = new Round();
            Two = new Round();
        }

        public string Name { get; set; }
        public Round One { get; set; }
        public Round Two { get; set; }
    }

    public class Round
    {
        public Round()
        {
            Enemy1 = new Combatant();
            Enemy2 = new Combatant();
            Enemy3 = new Combatant();
        }

        public Combatant Enemy1 { get; set; }
        public Combatant Enemy2 { get; set; }
        public Combatant Enemy3 { get; set; }
        public string Notes { get; set; }
        public double YourSpeed { get; set; }
        public string DiscordMarkdown 
        { 
            get
            {
                List<string> names = new List<string>() { Enemy1.Name, Enemy2.Name, Enemy3.Name };
                int longestName = names.Aggregate("Name", (max, cur) => max.Length > cur.Length ? max : cur).Length;

                Combatant[] combatants = new List<Combatant>() { Enemy1, Enemy2, Enemy3 }.OrderByDescending(x => x.Readiness).ToArray();

                string markdown = "";

                markdown += $"| {"Name".PadRight(longestName, ' ')} | Speed | {"HP".PadRight(5)} | Artifact\n";
                foreach (Combatant c in combatants)
                {
                    markdown += $"| {c.Name.PadRight(longestName, ' ')} | {Math.Ceiling((YourSpeed) * (c.Readiness / 100)).ToString().PadLeft(5, ' ')} | {c.HP.ToString().PadLeft(5)} | {c.Artifact}\n";
                }
                markdown += $"\n{Notes}\n";

                return markdown;
            }
        }
    }

    public class Combatant
    {
        public Combatant()
        {
            Name = "";
            Readiness = 0;
            HP = 0;
            Artifact = "N/A";
        }

        public string Name { get; set; }
        public double Readiness { get; set; }
        public int HP { get; set; }
        public string Artifact { get; set; }
    }
}