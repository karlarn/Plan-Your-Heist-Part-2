using System;
using System.Collections.Generic;
using System.Linq;

namespace HeistPartTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IRobber> rolodex = new List<IRobber>();
            rolodex.Add(new Hacker("Crendor", 50, 25));
            rolodex.Add(new Hacker("Jesse", 60, 25));
            rolodex.Add(new Muscle("Dodger", 45, 25));
            rolodex.Add(new Muscle("Rudy", 50, 25));
            rolodex.Add(new LockSpecialist("Doug", 65, 25));
            rolodex.Add(new LockSpecialist("Hank", 55, 25));
            string keepAdding = "y";

            while (keepAdding == "y")
            {
                Console.WriteLine($"{rolodex.Count} robbers are in your rolodex.");
                Console.WriteLine("---------------------");
                Console.Write("Lets add a new robber to your rolodex. If you do not wish to add a robber just press 'Enter'. Please add a name:");
                string newName = Console.ReadLine();
                if (newName == "")
                {
                    keepAdding = "n";
                }
                else
                {
                    Console.WriteLine(@"reply the number that corelates to the specialty:
            1 Hacker (Disables alarms)
            2 Muscle (Disarms Guards)
            3 Lock Specialist (Cracks vault)");
                    string robberType = Console.ReadLine();
                    Console.Write($"On a scale of 1 to 100 what is {newName}'s skill level? ");
                    int robberSkillLevel = Int32.Parse(Console.ReadLine());
                    Console.Write($"How much of the cut does {newName} want? Just the numbers of a percentage will do.");
                    int robberPercentCut = Int32.Parse(Console.ReadLine());

                    if (robberType.Contains("1"))
                    {
                        rolodex.Add(new Hacker(newName, robberSkillLevel, robberPercentCut));

                        Console.WriteLine($"{newName} has been added to your rolodex.");
                    }
                    else if (robberType.Contains("2"))
                    {
                        rolodex.Add(new Muscle(newName, robberSkillLevel, robberPercentCut));
                        Console.WriteLine($"{newName} has been added to your rolodex.");
                    }
                    else if (robberType.Contains("3"))
                    {
                        rolodex.Add(new LockSpecialist(newName, robberSkillLevel, robberPercentCut));
                        Console.WriteLine($"{newName} has been added to your rolodex.");
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong.");
                    }
                    Console.WriteLine("Do you want to make another robber? y or n ");
                    keepAdding = Console.ReadLine();

                }
            }
                Console.WriteLine(@"Lets talk about the bank we want to hit...
            
            ");

                Bank bankToRob = new Bank
                {
                    AlarmScore = new Random().Next(101),
                    VaultScore = new Random().Next(101),
                    SecurityGuardScore = new Random().Next(101),
                    CashOnHand = new Random().Next(15000, 1000001)
                };

                Dictionary<string, int> scoreDict = new Dictionary<string, int>();
                scoreDict.Add("Alarm", bankToRob.AlarmScore);
                scoreDict.Add("Vault", bankToRob.VaultScore);
                scoreDict.Add("Security", bankToRob.SecurityGuardScore);

                List<KeyValuePair<string, int>> orderedScoreList = scoreDict.OrderByDescending(i => i.Value).ToList();

                Console.WriteLine($@"Recon has been done on the bank for you.
            Highest Score: {orderedScoreList.First().Key}
            Lowest Score: {orderedScoreList.Last().Key}");

                string addingCrew = "y";
                List<IRobber> crew = new List<IRobber>();
                int totalPercentCutOfCrew = 0;

                while (addingCrew == "y")
                {

                    Console.WriteLine(@"Your Rolodex:
            -----------------------------");

                    for (int i = 0; i < rolodex.Count; i++)
                    {
                        if(rolodex[i].PercentageCut+totalPercentCutOfCrew>100)
                        {
                            rolodex.Remove(rolodex[i]);
                        }
                        else
                        {
                            Console.WriteLine($@"{i}: {rolodex[i].Name} {rolodex[i].Specialty}
                SkillLevel: {rolodex[i].SkillLevel} | {rolodex[i].PercentageCut}% of loot required");
                        }

                        
                    }

                    if(rolodex.Count==0 || totalPercentCutOfCrew==100)
                    {
                        break;
                    }


                    Console.WriteLine($"Your crew count is: {crew.Count}.");
                    Console.WriteLine("To add someone enter their Rolodex #:");
                    int index = Int32.Parse(Console.ReadLine());
                    totalPercentCutOfCrew =totalPercentCutOfCrew + rolodex.ElementAt(index).PercentageCut; 
                    crew.Add(rolodex.ElementAt(index));
                    rolodex.Remove(rolodex.ElementAt(index));
                    Console.WriteLine("Add another crew member? y or n");
                    addingCrew = Console.ReadLine();
                    Console.WriteLine("Sounds good.");
                }

                foreach (IRobber i in crew)
                {
                    i.PerformSkill(bankToRob);
                }

                if (bankToRob.IsSecure)
                {
                    Console.WriteLine("Your heist failed. hah.");
                }
                else
                {
                    Console.WriteLine($"You got ${bankToRob.CashOnHand} from the heist!"); 
                    foreach (IRobber i in crew)
                    {
                        decimal crewCut = bankToRob.CashOnHand*(i.PercentageCut/100m);
                        Console.WriteLine($"{i.Name} got a cut of ${crewCut}");
                    }
                    decimal yourCut = (100-totalPercentCutOfCrew)/100m;
                    decimal yourMoney = bankToRob.CashOnHand*yourCut;
                    Console.WriteLine($"That leaves ${yourMoney} for you.");  
                }

            }
        }
    }
