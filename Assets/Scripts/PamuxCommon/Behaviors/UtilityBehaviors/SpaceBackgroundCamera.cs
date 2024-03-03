using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class SpaceBackgroundCamera : MonoBehaviour
  {
      public float rotationSpeed = 30.0f;
      public Vector3 rotationCenter = Vector3.zero;
      public Vector3 rotationAxis = Vector3.up;
      bool isIntro = true;
      void LateUpdate()
      {
          if (isIntro)
          {
              transform.RotateAround(rotationCenter, rotationAxis, rotationSpeed * Time.deltaTime);
          }
          else
          {
              if (Player.IsAlive())
              {
                  transform.RotateAround(Player.INSTANCE.transform.position, Player.INSTANCE.transform.forward, Player.INSTANCE.transform.position.x * rotationSpeed * Time.deltaTime);
                  transform.RotateAround(Player.INSTANCE.transform.position, Player.INSTANCE.transform.right, -Player.INSTANCE.transform.position.z * rotationSpeed * Time.deltaTime);
              }
          }
      }
  
      public void SetIntroValues()
      {
          isIntro = true;
          rotationSpeed = 30.0f + Random.Range(-10f, 10f);
          rotationCenter = Random.onUnitSphere;
          rotationAxis = Random.onUnitSphere;
      }
  
      public void SetGamePlayValues()
      {
          isIntro = false;
          rotationSpeed = 1f;
          rotationCenter = Vector3.zero;
          rotationAxis = Vector3.down;
      }
  
      internal IEnumerator SetGamePlayValues(float duration)
      {
          yield return new WaitForSeconds(duration);
          SetGamePlayValues();
      }
  
      public void SetCollisionValues()
      {
          isIntro = false;
          rotationSpeed = 10f;
          rotationCenter = Vector3.zero;
          rotationAxis = Random.onUnitSphere;
          StartCoroutine(SetGamePlayValues(1.0f));
      }
  }
}
