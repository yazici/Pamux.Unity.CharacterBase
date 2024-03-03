using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Pamux
{
  namespace Zodiac
  {
    namespace UI
    {
      public sealed class Store : Abstracts.UI
      {
        public static Store INSTANCE = null;
        public GameObject rowPrefab;
        private UISprite spriteBlueScreen;
        private UITable tableFeatures;
        private UILabel lblFunds;
        private StoreTableRow energy;
        private StoreTableRow centerWeapon;
        private StoreTableRow sideWeapon;
        private StoreTableRow missiles;
        private StoreTableRow laser;
        private StoreTableRow shield;
        private StoreTableRow nukes;
        private StoreTableRow magnet;
        protected override bool DoSetMember(Transform go)
        {
          return SetMember(go, "spriteBlueScreen", ref spriteBlueScreen);
        }

        private StoreTableRow CreateRow(string featureSuffix, string feature, int defaultLevel = 0)
        {
          GameObject go = Instantiate(rowPrefab, Vector3.zero, Quaternion.identity) as GameObject;
          go.transform.parent = tableFeatures.transform;
          go.transform.localScale = Vector3.one;

          int featureLevel = App.INSTANCE.ppd.GetFeatureLevel(featureSuffix, defaultLevel);
          int price = FeatureLevels.GetPrice(featureSuffix, featureLevel);
          StoreTableRow r = go.GetComponent<StoreTableRow>();
          r.SetRowData(featureSuffix, feature, price, featureLevel);
          return r;
        }

        void Awake()
        {
          INSTANCE = this;
          SetMembers();

          tableFeatures = MyExtensions.GetChild(spriteBlueScreen.transform, "tableFeatures").GetComponent<UITable>();
          Transform innerTable = MyExtensions.GetChild(tableFeatures.transform, "Table");
          lblFunds = MyExtensions.GetChild(innerTable, "lblFunds").GetComponent<UILabel>();

          energy = CreateRow("Energy", "Energy", 1);
          centerWeapon = CreateRow("Weapon.Center", "Main Weapon", 1);
          sideWeapon = CreateRow("Weapon.Side", "Side Weapon");
          magnet = CreateRow("Magnet", "Magnet", 1);
          missiles = CreateRow("Weapon.Missiles", "Missiles");
          laser = CreateRow("Weapon.Laser", "Laser");
          shield = CreateRow("Shield", "Shield");
          nukes = CreateRow("Weapon.Nukes", "Nukes");

          UpdateFunds();
        }


        internal void UpdateFunds()
        {
          lblFunds.text = "you have $" + App.INSTANCE.ppd.funds;

          energy.UpdateBuyability();
          centerWeapon.UpdateBuyability();
          sideWeapon.UpdateBuyability();
          magnet.UpdateBuyability();
          missiles.UpdateBuyability();
          laser.UpdateBuyability();
          shield.UpdateBuyability();
          nukes.UpdateBuyability();
        }

        public void OnClickDELETEALL()
        {
          PlayerPrefs.DeleteAll();
          PlayerPrefs.SetInt("Player.Funds", 10000);
          SceneManager.LoadScene(Application.loadedLevel);
        }
      }
    }
  }
}