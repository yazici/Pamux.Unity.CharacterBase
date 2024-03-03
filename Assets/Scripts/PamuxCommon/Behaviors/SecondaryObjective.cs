using UnityEngine;

namespace Pamux.Abstracts
{
    public abstract class SecondaryObjective : MonoBehaviour
    {
      private bool _isAchieved = false;

      protected abstract bool vIsAchieved();

      internal bool IsAchieved()
      {
        _isAchieved = _isAchieved || vIsAchieved();
        return _isAchieved;
      }
    }

    public class LevelCompletedUntouched : SecondaryObjective
    {
      protected override bool vIsAchieved()
      {
        return GameControllerBase.INSTANCE.summaryData.untouched;
      }
    }
}