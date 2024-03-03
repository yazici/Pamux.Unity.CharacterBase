using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pamux
{
  public class FeatureLevels
  {
      private static Dictionary<string, int[]> featureUpgradePrices = new Dictionary<string, int[]>();
      static FeatureLevels()
      {
          featureUpgradePrices["Magnet"] = new int[] { 100, 200, 300, 400, 800, 1000, 1500, 2000, 2500, 3000 };
          featureUpgradePrices["Shield"] = new int[] { 100, 200, 300, 400, 800, 1000, 1500, 2000, 2500, 3000 };
          featureUpgradePrices["Energy"] = new int[] { 100, 200, 300, 400, 800, 1000, 1500, 2000, 2500, 3000 };
          featureUpgradePrices["Weapon.Laser"] = new int[] { 100, 200, 450, 750, 1250, 1750, 2500, 3250, 4000, 5000 };
          featureUpgradePrices["Weapon.Missiles"] = new int[] { 100, 200, 350, 550, 850, 1250, 1700, 2200, 2800, 3500 };
          featureUpgradePrices["Weapon.Nukes"] = new int[] { 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 5000, 6000 };
          featureUpgradePrices["Weapon.Center"] = new int[] { 100, 150, 200, 300, 400, 850, 1150, 1500, 1750, 2000 };
          featureUpgradePrices["Weapon.Side"] = new int[] { 200, 300, 400, 600, 800, 1700, 2300, 3000, 3500, 4000 };
      }
  
      internal static int GetPrice(string featureSuffix, int featureLevel)
      {
          if (featureLevel == 10)
          {
              return 0;
          }
  
          return featureUpgradePrices[featureSuffix][featureLevel];
      }
  }
}
