namespace HeistPartTwo
{
    public interface IRobber
{
    string Name {get;}

    string Specialty {get;}

    int SkillLevel {get;}

    int PercentageCut {get;}

    void PerformSkill(Bank b);

}

}
 