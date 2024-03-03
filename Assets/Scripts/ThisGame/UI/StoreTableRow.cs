using UnityEngine;
using System.Collections;
using System;

namespace Pamux
{
  namespace Zodiac
  {

    namespace UI
    {
      public class StoreTableRow : MonoBehaviour
      {
        const int MAX_UPGRADE_LEVEL = 10;

        internal string featureSuffix;
        internal string feature;
        internal int price;
        internal int featureLevel;

        private UILabel lblFeature;
        private UILabel lblPrice;
        private UISprite spriteUpgradeLevel;
        private UISprite spriteUpgradeFiller;
        private UILabel lblUpgradeLevel;
        private UIButton btnBuyUpgrade;




        void Awake()
        {
          lblFeature = MyExtensions.GetChild(this.transform, "lblFeature").GetComponent<UILabel>();
          lblPrice = MyExtensions.GetChild(this.transform, "lblPrice").GetComponent<UILabel>();

          btnBuyUpgrade = MyExtensions.GetChild(this.transform, "spriteBuyUpgrade").GetComponent<UIButton>();

          //UIWidget containerUpgradeLevel = MyExtensions.GetChild(this.transform, "containerUpgradeLevel").GetComponent<UIWidget>();
          spriteUpgradeLevel = MyExtensions.GetChild(this.transform, "spriteUpgradeLevel").GetComponent<UISprite>();
          spriteUpgradeFiller = MyExtensions.GetChild(spriteUpgradeLevel.transform, "spriteUpgradeFiller").GetComponent<UISprite>();
          lblUpgradeLevel = MyExtensions.GetChild(spriteUpgradeLevel.transform, "lblUpgradeLevel").GetComponent<UILabel>();
        }



        internal void SetRowData(string featureSuffix, string feature, int price, int featureLevel)
        {
          this.featureSuffix = featureSuffix;
          this.feature = feature;
          this.price = price;
          this.featureLevel = featureLevel;

          lblFeature.text = feature;

          UpdatePrice();

          lblUpgradeLevel.text = featureLevel + "/" + MAX_UPGRADE_LEVEL;

          int fillerWidth = featureLevel == 0 ? 0 : 7 + 23 * featureLevel;
          spriteUpgradeFiller.transform.localPosition = new Vector3(fillerWidth / 2, 0.0f, 0.0f);
          spriteUpgradeFiller.width = fillerWidth;
        }

        public void OnClickUpgradeFeature()
        {
          App.INSTANCE.ppd.funds -= price;
          price = FeatureLevels.GetPrice(featureSuffix, featureLevel + 1);
          this.SetRowData(featureSuffix, feature, price, this.featureLevel + 1);
          App.INSTANCE.ppd.SetFeatureLevel(featureSuffix, this.featureLevel);
          Store.INSTANCE.UpdateFunds();
        }

        internal void UpdatePrice()
        {
          if (featureLevel == MAX_UPGRADE_LEVEL)
          {
            lblPrice.text = "MAX";
          }
          else
          {
            lblPrice.text = "$" + price;
          }
        }
        internal void UpdateBuyability()
        {
          btnBuyUpgrade.isEnabled = this.featureLevel < MAX_UPGRADE_LEVEL && App.INSTANCE.ppd.funds >= this.price;
          UpdatePrice();
        }
      }
    }
  }
}