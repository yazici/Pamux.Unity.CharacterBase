using UnityEngine;
using System.Collections;

namespace Pamux
{
  namespace Zodiac
  {
    public class LevelSelectItem : MonoBehaviour
    {
      private int _id;
      public int id
      {
        get { return _id; }

        set
        {
          _id = value;
          _isUnlocked = App.INSTANCE.ppd.achievements.IsLevelUnlocked(id, Difficulties.Easy);
        }
      }
      private bool _isUnlocked = true;
      public bool isPlayable { get { return _isUnlocked; } }
    }
  }
}