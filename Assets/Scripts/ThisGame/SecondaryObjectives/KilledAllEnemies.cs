namespace Pamux.Zodiac.Objectives
{
    using Pamux.Abstracts;

    public class KilledAllEnemies : SecondaryObjective
    {
        protected override bool vIsAchieved()
        {
            return GameController.INSTANCE.summaryData.totalEnemies == GameController.INSTANCE.summaryData.enemiesKilled;
        }
    }
}
