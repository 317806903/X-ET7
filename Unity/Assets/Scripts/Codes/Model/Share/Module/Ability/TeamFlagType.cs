namespace ET.Ability
{
    public enum TeamFlagType
    {
        Monster = 1,
        Team1 = Monster << 1,
        Team2 = Team1 << 1,
        Team3 = Team2 << 1,
        Team4 = Team3 << 1,
        Team5 = Team4 << 1,
        Team6 = Team5 << 1,
        Team7 = Team6 << 1,
        Team8 = Team7 << 1,
    }
}