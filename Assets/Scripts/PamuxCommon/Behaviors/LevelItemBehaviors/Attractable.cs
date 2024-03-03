using UnityEngine;

namespace Pamux
{
  public class Attractable : LevelItemBehavior
  {
      public Attractor attractor;
      private float clampValue = 15f;
  
      void Start()
      {
          if (attractor == null && Player.INSTANCE != null)
          {
              attractor = Player.LID.attractor;
          }
      }
  
      void FixedUpdate()
      {
          if (attractor == null)
          {
              enabled = false;
              return;
          }
          Vector3 directionVector = attractor.transform.position - transform.position;
          if (directionVector.magnitude > attractor.rangeRadius)
          {
              return;
          }
          //Debug.Log(directionVector.magnitude);
          if (directionVector.magnitude < 2)
          {
              GetComponent<Rigidbody>().velocity = directionVector * 10.0f;
              return;
          }
  
          GetComponent<Rigidbody>().velocity += directionVector.normalized * attractor.strength / directionVector.magnitude;
          GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().velocity.x, -clampValue, clampValue), 0.0f, Mathf.Clamp(GetComponent<Rigidbody>().velocity.z, -clampValue / 1.5f, clampValue));
          //Debug.Log(rigidbody.velocity);
      }
  }
}
