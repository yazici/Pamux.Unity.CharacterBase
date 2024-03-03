using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Pamux
{
  namespace Abstracts
  {
    public abstract class SummaryDataBase
    {
      public int level;
      public Difficulties difficulty = Difficulties.Impossible;

      public int score;
      public int bonus { get { return 12; } }
      public int finalScore { get { return score * (100 + bonus) / 100; } }

      public int fundsCollected;
      public int totalFunds;

      public string FundsInfo
      {
        get
        {
          return string.Format("{0:000}/{1:000}", fundsCollected, totalFunds);
        }
      }
      public string FinalScoreInfo
      {
        get
        {
          return string.Format("{0:000 000}", finalScore);
        }
      }
      public string ScoreInfo
      {
        get
        {
          return string.Format("{0:000 000}", score);
        }
      }

      public string BonusInfo
      {
        get
        {
          return string.Format("{0}%", bonus);
        }
      }

      public string FireRateUpgradeInfo
      {
        get
        {
          return string.Format("...");
        }
      }

      public bool untouched = true;


    }
  }
}