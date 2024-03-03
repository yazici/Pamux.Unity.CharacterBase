using UnityEngine;
using System.Collections;

namespace Pamux
{
  namespace Zodiac
  {

    [System.Serializable]
    public class Leg
    {
      public float startTime;
      public Vector2 velocity;
      public Vector2 force;
    }

    public class Mover : MonoBehaviour
    {
      public Leg[] legs;
      private int currentLeg = 0;
      private float startTime;
      void Start()
      {
        ApplyLeg();
      }

      void ApplyLeg()
      {
        Leg leg = legs[currentLeg];

        startTime = Time.time;

        if (leg.force != Vector2.zero)
        {
          GetComponent<Rigidbody>().velocity = Vector3.zero;
          GetComponent<Rigidbody>().AddForce(new Vector3(legs[currentLeg].force.x, 0.0f, legs[currentLeg].force.y), ForceMode.Force);
        }
        else
        {
          GetComponent<Rigidbody>().velocity = new Vector3(legs[currentLeg].velocity.x, 0.0f, legs[currentLeg].velocity.y);
        }

      }
      void FixedUpdate()
      {
        if (currentLeg < legs.Length - 1 && Time.time - startTime > legs[currentLeg + 1].startTime)
        {
          ++currentLeg;
          ApplyLeg();
        }

      }
    }
  }
}