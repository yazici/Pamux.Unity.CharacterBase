using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class PlayerWeapon : Weapon
  {
      [System.Serializable]
      public class WeaponLevelsParameters
      {
          public int fireCount = 1;
          public float fireEnergy = 100f;
  
          internal WeaponLevelsParameters(int fireCount, float fireEnergy)
          {
              this.fireCount = fireCount;
              this.fireEnergy = fireEnergy;
          }
      }
  
      public WeaponLevelsParameters[] levelParameters = new WeaponLevelsParameters[]
      {
          new WeaponLevelsParameters(1, 100f),
          new WeaponLevelsParameters(2, 100f),
          new WeaponLevelsParameters(2, 150f),
          new WeaponLevelsParameters(3, 150f),
          new WeaponLevelsParameters(3, 200f),
          new WeaponLevelsParameters(3, 250f),
          new WeaponLevelsParameters(3, 300f),
          new WeaponLevelsParameters(3, 400f),
          new WeaponLevelsParameters(3, 450f),
          new WeaponLevelsParameters(3, 500f),
      };
  
  
  
      public FireRateInfo[] fireRates;
  
  
      protected override FireRateInfo fireRate { get { return fireRates[fireRateIndex]; } }
  
      private int _featureLevel;
      public bool autoFire = true;
      private int fireRateIndex = 0;
  
      public int featureLevel { set { _featureLevel = value - 1; } }
      public override float fireEnergy { get { return _featureLevel < 0 ? 0.0f : levelParameters[_featureLevel].fireEnergy; } }
      public override int muzzleCount { get { return _featureLevel < 0 ? 0 : levelParameters[_featureLevel].fireCount; } }
  
      void Awake()
      {
          if (fireRates == null || fireRates.Length == 0)
          {
              fireRates = new FireRateInfo[]
              {
                  new FireRateInfo(0.50f),
                  new FireRateInfo(0.40f),
                  new FireRateInfo(0.35f),
                  new FireRateInfo(0.30f),
                  new FireRateInfo(0.28f),
                  new FireRateInfo(0.25f),
                  new FireRateInfo(0.22f),
                  new FireRateInfo(0.20f),
                  new FireRateInfo(0.12f),
                  new FireRateInfo(0.05f)
              };
          }
      }
  
  
      internal void UpgradeFireRate(int count)
      {
          fireRateIndex = Mathf.Min(fireRateIndex + count, fireRates.Length - 1);
      }
  
      internal bool HasMaxFireRate()
      {
          return fireRateIndex >= fireRates.Length - 1;
      }

    internal int FireRateIndex
    {
      get
      {
        return fireRateIndex;
      }
    }

    protected override bool vShouldFireNow() { return (autoFire || Input.GetButton("Fire1")) && !Player.INSTANCE.isExtracting; }
  }
}
