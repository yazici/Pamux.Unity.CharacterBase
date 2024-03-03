using UnityEngine;
using System.Collections;

namespace Pamux
{
  public enum WeaponFireStyles
  {
      None,
      //UnityBoltOrange,
      //UnityBoltCyan,
      //AcidBullet,
      StarBullet,
      RailBullet,
      SmokyRocket, // Cool
      //LightningBeam, // Wrong axis
      FieryRocket,
      //SonicWave,
      //VerySimpleBullet,
      PlasmaMissile,
      //PurpleBullet,
      //Meteor,
      //VulcanBullet,
      //TractorBeam,
      MiniRocket,
      //BaseBullet,
      ElectricBullet,
      //RedLaser
  }
  
  
  public class WeaponFire : MonoBehaviour
  {
      internal WeaponFireStyleInfo styleInfo;
      public LevelItemData lid;
  
  
      public void Fire(WeaponFireStyles fireStyle, float fireEnergy)
      {
          lid.stylable.SetStyle(fireStyle.ToString());
          styleInfo = lid.stylable.styleInfo as WeaponFireStyleInfo;
          if (styleInfo == null)
          {
              return;
          }
          lid.energy.amount = fireEnergy;
          if (styleInfo.muzzleEffect != null)
          {
              Instantiate(styleInfo.muzzleEffect, transform.position, transform.rotation);
          }
          //Debug.Log("FIRE: " + gameObject.name);
          GetComponent<TargetSeeker>().enabled = styleInfo.targetSeeker;
  
          GetComponent<Rigidbody>().velocity = Vector3.zero;
          GetComponent<Rigidbody>().AddForce(transform.forward * styleInfo.impulse, ForceMode.Impulse);
          if (styleInfo.randomTorque != 0.0f)
          {
              GetComponent<Rigidbody>().AddTorque(Random.Range(-styleInfo.randomTorque, styleInfo.randomTorque),
                                      Random.Range(-styleInfo.randomTorque, styleInfo.randomTorque),
                                      Random.Range(-styleInfo.randomTorque, styleInfo.randomTorque), ForceMode.Impulse);
          }
      }
  
  
      void LateUpdate()
      {
          if (styleInfo != null)
          {
              GetComponent<Rigidbody>().AddForce(transform.forward * styleInfo.continuousForce * Time.deltaTime, ForceMode.Impulse);
          }
      }
  
      void OnCollisionEnter(Collision collision)
      {
          if (styleInfo.explosionEffect != null)
          {
              Instantiate(styleInfo.explosionEffect, transform.position, transform.rotation);
          }
  
          if (styleInfo.detachOnDeath != null)
          {
              for (int i = 0; i < styleInfo.detachOnDeath.Length; i++)
              {
                  styleInfo.detachOnDeath[i].transform.parent = null;
                  ParticleSystem PS = styleInfo.detachOnDeath[i].GetComponent<ParticleSystem>();
                  PS.enableEmission = false;
                  Destroy(styleInfo.detachOnDeath[i], 5);
              }
          }
  
          ObjectPool.Release(gameObject);
      }
  }
}
