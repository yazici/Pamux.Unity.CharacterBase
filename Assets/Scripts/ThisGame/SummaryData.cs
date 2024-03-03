using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Pamux
{
  namespace Zodiac
  {
    public class SummaryData : Abstracts.SummaryDataBase
    {
      public int extractablesExtracted;
      public int totalExtractables;
      public int enemiesKilled;
      public int totalEnemies;

      public string EnemiesInfo { get { return string.Format("{0:000}/{1:000}", GameController.INSTANCE.summaryData.enemiesKilled, GameController.INSTANCE.summaryData.totalEnemies); } }
      public string ExtractablesInfo { get { return string.Format("{0:000}/{1:000}", GameController.INSTANCE.summaryData.extractablesExtracted, GameController.INSTANCE.summaryData.totalExtractables); } }
    }
  }
}