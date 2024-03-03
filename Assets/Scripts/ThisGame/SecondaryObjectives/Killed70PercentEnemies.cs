namespace Pamux.Zodiac.Objectives
{
    using Pamux.Abstracts;

    public class Killed70PercentEnemies : SecondaryObjective
    {
        protected override bool vIsAchieved()
        {
            return (GameController.INSTANCE.summaryData.enemiesKilled * 100 / GameController.INSTANCE.summaryData.totalEnemies) >= 70;
        }
    }
}
