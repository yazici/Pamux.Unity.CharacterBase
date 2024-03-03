namespace Pamux.Zodiac.Objectives
{
    using Pamux.Abstracts;

    public class CollectedAllFunds : SecondaryObjective
    {
        protected override bool vIsAchieved()
        {
            return GameController.INSTANCE.summaryData.totalFunds == GameController.INSTANCE.summaryData.fundsCollected && GameController.INSTANCE.summaryData.totalEnemies == GameController.INSTANCE.summaryData.enemiesKilled;
        }
    }
}
