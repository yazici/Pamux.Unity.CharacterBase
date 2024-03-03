using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pamux
{
  public class PersistentPlayerData
  {
      public const int MAX_LIVES = 10;
      public const float FUNDS_PER_SECOND_EXCHANGE_RATE = 1.0f / 60.0f;
  
      public float timeCostOfLife = 1000.0f;
      public float fundsPerSecondExchangeRate = FUNDS_PER_SECOND_EXCHANGE_RATE;
  
      public Abstracts.Achievements achievements = new Abstracts.Achievements();
  
      public float timeToNewShip
      {
          get
          {
              return Time.realtimeSinceStartup + timeCostOfLife - timeOfLastLifeAdded;
          }
      }
  
      public int fundsToNewShip
      {
          get
          {
              return (int) (timeToNewShip * fundsPerSecondExchangeRate);
          }
      }
  
  
      public float timeOfLastLifeAdded
      {
          get { return PlayerPrefs.HasKey("Player.LastLifeTime") ? PlayerPrefs.GetFloat("Player.LastLifeTime") : 0.0f; }
          set { PlayerPrefs.SetFloat("Player.LastLifeTime", value); }
      }
  
      public int funds
      {
          get { return 10000 + GetFeatureLevel("Funds"); }
          set { SetFeatureLevel("Funds", value); }
      }
  
      public int centerWeaponLevel
      {
          get { return GetFeatureLevel("Weapon.Center", 1); }
          set { SetFeatureLevel("Weapon.Center", value); }
      }
  
      public int sideWeaponLevel
      {
          get { return GetFeatureLevel("Weapon.Side"); }
          set { SetFeatureLevel("Weapon.Side", value); }
      }
  
      public int missileLevel
      {
          get { return GetFeatureLevel("Weapon.Missile"); }
          set { SetFeatureLevel("Weapon.Missile", value); }
      }
  
      public int laserLevel
      {
          get { return GetFeatureLevel("Weapon.Laser"); }
          set { SetFeatureLevel("Weapon.Laser", value); }
      }
      public int shieldLevel
      {
          get { return GetFeatureLevel("Shield"); }
          set { SetFeatureLevel("Shield", value); }
      }
      public int energyLevel
      {
          get { return GetFeatureLevel("Energy", 1); }
          set { SetFeatureLevel("Energy", value); }
      }
      public int nukeLevel
      {
          get { return GetFeatureLevel("Weapon.Nuke"); }
          set { SetFeatureLevel("Weapon.Nuke", value); }
      }
  
      public int lives
      {
          get { return GetFeatureLevel("Lives", 10); }
          set { SetFeatureLevel("Lives", value); }
      }
      public string name
      {
          get { return PlayerPrefs.HasKey("Player.Name") ? PlayerPrefs.GetString("Player.Name") : ""; }
          set { PlayerPrefs.SetString("Player.Name", value); }
      }
      public int magnetLevel
      {
          get { return GetFeatureLevel("Magnet", 1); }
          set { SetFeatureLevel("Magnet", value); }
      }
      public void Save()
      {
          PlayerPrefs.Save();
      }
  
  
      internal void BuyShip()
      {
          lives++;
          funds -= fundsToNewShip;
          timeOfLastLifeAdded = Time.realtimeSinceStartup;
      }
  
      internal void Merge(Abstracts.SummaryDataBase summaryData)
      {
          funds += summaryData.fundsCollected;
  
          if (Player.IsAlive())
          {
              achievements.MarkLevelComplete(summaryData.level, summaryData.difficulty);
          }
  
          Save();
      }
  
      internal void SetFeatureLevel(string featureSuffix, int newLevel)
      {
          PlayerPrefs.SetInt("Player." + featureSuffix, newLevel);
      }
  
      internal int GetFeatureLevel(string featureSuffix, int defaultLevel = 0)
      {
          return PlayerPrefs.HasKey("Player." + featureSuffix) ? PlayerPrefs.GetInt("Player." + featureSuffix) : defaultLevel;
      }
  
  }
}
