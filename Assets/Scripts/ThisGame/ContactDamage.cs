using UnityEngine;
using System.Collections;

namespace Pamux
{
  namespace Zodiac
  {

    public class ContactDamage : MonoBehaviour
    {
      public AudioClip collisionAudio;
      private AudioSource collisionAudioSource;
      public bool destroyOnAnyCollision;
      public LevelItemData lid;


      void Start()
      {

        collisionAudioSource = gameObject.AddComponent<AudioSource>();
        collisionAudioSource.loop = false;
        collisionAudioSource.priority = 1;
        collisionAudioSource.volume = App.INSTANCE.sfxVolume;
        


        /*foreach (var a in this.gameObject.GetComponents<AudioSource>())
        {
          if (a != GetComponent<AudioSource>() && a.clip == null)
          {
            collisionAudioSource = a;
            collisionAudioSource.mute = false;
            
            break;
          }
        }*/

        //thisEnergyComponent = lid.energy;
        //if (thisEnergyComponent == null)
        //{
        //    //Debug.Log("USING PARENTS ENERGY" + this.gameObject.name);
        //    //thisEnergyComponent = this.gameObject.GetComponentInParent<Energy>() as Energy;
        //}
      }


      void OnTriggerEnter(Collider other)
      {

        //Debug.Log(gameObject.name + " :CDD: " + other.gameObject.name);
        if (lid.energy == null || lid.energy.IsEmpty)
        {
          return;
        }
        LevelItemData otherLID = other.gameObject.GetComponent<LevelItemData>();
        if (otherLID == null)
        {
          //Debug.Log("NOLID " + other.gameObject.name);
        }

        Energy otherEnergyComponent = other.gameObject.GetComponent<Energy>();

        if (otherEnergyComponent == null)
        {
          otherEnergyComponent = other.gameObject.GetComponentInParent<Energy>();
        }
        if (otherEnergyComponent == null || otherEnergyComponent.IsEmpty)
        {
          return;
        }
        if (collisionAudio != null)
        {
          if (lid.isPlayer)
          {
            //Player.INSTANCE.gameObject.ShakeRotation(Vector3.one , 2f, 0.0f);
            GameController.INSTANCE.uiCamera.gameObject.ShakePosition(Vector3.one / 10f, 0.2f, 0.0f);
            //GameController.INSTANCE.fxCamera.gameObject.ShakePosition(Vector3.one/10f, 0.2f, 0.0f);
            //GameController.INSTANCE.spaceBackgroundCamera.SetCollisionValues();
            //gameObject.RotateAdd(Vector3.one / 10f, 0.2f, 0.0f);
            //GameController.INSTANCE.mainCamera.gameObject.RotateAdd(Vector3.one / 10f, 0.2f, 0.0f);
          }
          collisionAudioSource.clip = collisionAudio;
          collisionAudioSource.Play();
        }
        otherEnergyComponent.Exchange(lid.energy);

        if (destroyOnAnyCollision)
        {
          ObjectPool.Release(this.gameObject);
        }
      }

    }
  }
}