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

                markdown += $"#   {"Name".PadRight(longestName, ' ')}   # Speed Est. # {"HP".PadRight(longestHp)} # Artifact\n";
                foreach (Combatant c in combatants)
                {
                    double percentage = c.Readiness / 100;

                    // estimating the highs and lows, first number is my character 
                    // double speed00 = Math.Ceiling((YourSpeed) * (percentage));
                    // double speed05 = Math.Ceiling((YourSpeed) * (percentage)) - Math.Ceiling((YourSpeed * percentage) * 0.05);
                    // double speed50 = Math.Ceiling((YourSpeed * 1.05) * percentage);
                    // double speed55 = Math.Ceiling((YourSpeed * 1.05) * percentage) + Math.Ceiling((YourSpeed * 1.05) * percentage *  0.05);
                    // double averageSpeed = Math.Ceiling((speed00 + speed05 + speed50 + speed55) / 4);

                    double formulaLow = Math.Ceiling(YourSpeed * percentage / 1.05);
                    double formulaHigh = Math.Ceiling(YourSpeed * 1.05 * percentage);
                    string speedRange = $"{formulaLow} - {formulaHigh}";

                    markdown += $"| < {c.Name.PadRight(longestName, ' ')} > | {speedRange.PadLeft(10, ' ')} | {c.HP.PadLeft(longestHp, ' ')} | {c.Artifact}\n";
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