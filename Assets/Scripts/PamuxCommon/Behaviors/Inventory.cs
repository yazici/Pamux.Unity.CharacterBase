using UnityEngine;
using System.Collections.Generic;

namespace Pamux
{
  public class Inventory : MonoBehaviour
  {
      public IDictionary<PickableTypes, int> items = new Dictionary<PickableTypes, int>();

      internal int Count(PickableTypes type)
      {
          return items.ContainsKey(type) ? items[type] : 0;
      }
  
      internal void Add(PickableTypes type, int count = 1)
      {
          items[type] = Count(type) + count;
  
          switch (type)
          {
              case PickableTypes.Funds:
                  App.INSTANCE.ppd.funds += count;
                  Abstracts.GameControllerBase.INSTANCE.summaryData.fundsCollected += count;
                  break;
              case PickableTypes.FireRateUpgrade:
                  Player.INSTANCE.UpgradeFireRate(count);
                  break;
          }
  
      }
  
  
  
  }
}
