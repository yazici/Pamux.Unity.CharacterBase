namespace Pamux.Zodiac.Objectives
{
    using Pamux.Abstracts;

    public class CollectedAllExtractables : SecondaryObjective
    {
        protected override bool vIsAchieved()
        {
            return GameController.INSTANCE.summaryData.totalExtractables == GameController.INSTANCE.summaryData.extractablesExtracted;
        }
    }
}
