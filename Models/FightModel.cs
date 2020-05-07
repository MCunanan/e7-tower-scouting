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

                List<string> hps = new List<string>() { Enemy1.HP, Enemy2.HP, Enemy3.HP };
                int longestHp = hps.Aggregate("HP", (max, cur) => max.Length > cur.Length ? max : cur).Length;

                Combatant[] combatants = new List<Combatant>() { Enemy1, Enemy2, Enemy3 }.OrderByDescending(x => x.Readiness).ToArray();

                string markdown = "";

                markdown += $"#   {"Name".PadRight(longestName, ' ')}   # {"Speed".PadRight(9)} # {"HP".PadRight(longestHp)} # Artifact\n";
                foreach (Combatant c in combatants)
                {
                    double percentage = c.Readiness / 100;
                    double speedLow = Math.Ceiling((YourSpeed) * (percentage));
                    double speedHigh = Math.Ceiling((YourSpeed * 1.05) * percentage) + Math.Ceiling((YourSpeed * 1.05) * percentage *  0.05);
                    markdown += $"| < {c.Name.PadRight(longestName, ' ')} > | {string.Format("{0} - {1}", speedLow, speedHigh).PadLeft(9, ' ')} | {c.HP.PadLeft(longestHp, ' ')} | {c.Artifact}\n";
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
            HP = "";
            Artifact = "";
        }

        public string Name { get; set; }
        public double Readiness { get; set; }
        public string HP { get; set; }
        public string Artifact { get; set; }
    }
}