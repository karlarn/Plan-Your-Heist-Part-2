using System;
namespace HeistPartTwo
{

    public class CrewMember 
    {
        public string Name {get; set;}

        public int SkillLevel {get; set;}

        public int PercentageCut {get; set;} 

        public string Specialty {get; set;}

        public virtual void PerformSkill(Bank b)
    {
        int newScore = b.AlarmScore-SkillLevel;
        b.AlarmScore = newScore;
        Console.WriteLine($"{Name} is dealing with the alarm system. Decreased alarm score by {SkillLevel} points.");
        if(newScore<=0)
        {
            Console.WriteLine($"{Name} has disabled the alarm.");
        }
    }
    }
  
  public class Hacker : CrewMember, IRobber
{
    
    

    public Hacker(string n, int s, int p)
    {
        Name = n;
        SkillLevel = s;
        PercentageCut = p;
        Specialty = "Disables the alarm.";
    }

}

public class Muscle : CrewMember, IRobber
{
    public override void PerformSkill(Bank b)
    {
       int newScore = b.SecurityGuardScore-SkillLevel;
       b.SecurityGuardScore = newScore;
       Console.WriteLine($"{Name} is dealing with the security guard. Decreased security guard score by {SkillLevel} points.");
       if(newScore<=0)
        {
            Console.WriteLine($"{Name} has permanently disabled the security guard.");
        }
    }

    public Muscle(string n, int s, int p)
    {
        Name = n;
        SkillLevel = s;
        PercentageCut = p;
        Specialty = "Combats the security guard.";
    }

}

public class LockSpecialist : CrewMember, IRobber
{
    public override void PerformSkill(Bank b)
    {
       int newScore = b.VaultScore-SkillLevel;
       b.VaultScore = newScore;
       Console.WriteLine($"{Name} is dealing with the vault. Decreased vault score by {SkillLevel} points.");
       if(newScore<=0)
        {
            Console.WriteLine($"{Name} has opened the vault.");
        }
    }

    public LockSpecialist(string n, int s, int p)
    {
        Name = n;
        SkillLevel = s;
        PercentageCut = p;
        Specialty = "Opens the Vault.";
    }
}
}
