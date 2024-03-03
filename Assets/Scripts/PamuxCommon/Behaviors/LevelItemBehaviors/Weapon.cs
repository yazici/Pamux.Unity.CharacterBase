using UnityEngine;
using System.Collections;

namespace Pamux
{
  public abstract class Weapon : MonoBehaviour
  {
      public MuzzleGroup muzzleGroup;
  
      private int currentGroupCount;
      public WeaponFireStyles fireStyle;
      private float nextFire;
  
  
  
      public abstract float fireEnergy { get; }
      public abstract int muzzleCount { get;  }
  
      [System.Serializable]
      public class FireRateInfo
      {
          public float fireRate;
          public float fireGroupRate;
          public int fireGroupCount;
  
          public FireRateInfo(float fireRate, float fireGroupRate, int fireGroupCount)
          {
              this.fireRate = fireRate;
              this.fireGroupRate = fireGroupRate;
              this.fireGroupCount = fireGroupCount;
          }
  
          public FireRateInfo(float fireRate)
          {
              this.fireRate = fireRate;
              this.fireGroupRate = 0.0f;
              this.fireGroupCount = 0;
          }
      }
  
      protected abstract FireRateInfo fireRate { get; }
  
  
      void Start()
      {
          currentGroupCount = 0;
          nextFire = Time.time + fireRate.fireGroupRate;
  
          if (muzzleGroup != null)
          {
              muzzleGroup.weapon = this;
          }
  
  
          muzzleGroup.EnableMuzzles(muzzleCount);
      }
  
      void Update()
      {
          if (ShouldFireNow())
          {
              currentGroupCount++;
              //Debug.Log(currentGroupCount);
              //Debug.Log(nextFire);
              if (currentGroupCount >= fireRate.fireGroupCount && fireRate.fireGroupCount != 0)
              {
                  currentGroupCount = 0;
                  nextFire = Time.time + fireRate.fireGroupRate;
              }
              else
              {
                  nextFire = Time.time + fireRate.fireRate;
              }
  
              muzzleGroup.Fire();
          }
      }
  
      private bool ShouldFireNow()
      {
          //target = GameController.INSTANCE.level.GetSeekableTarget(this.transform.position.z);
          return muzzleGroup != null
                  && muzzleGroup.enabled
                  && Time.time > nextFire
                  && Abstracts.GameControllerBase.INSTANCE != null
                  && Zodiac.GameController.INSTANCE.mainGamePlay != null
                  && Zodiac.GameController.INSTANCE.mainGamePlay.IsRunning()
                  && vShouldFireNow();
      }
  
      protected abstract bool vShouldFireNow();
  
  
  
  
  }
}
