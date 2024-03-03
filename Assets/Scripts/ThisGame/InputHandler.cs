using UnityEngine;
using System.Collections;


namespace Pamux
{
  namespace Zodiac
  {

    public class InputHandler : MonoBehaviour
    {
      public float speed;
      public float tilt;

      void FixedUpdate()
      {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;
        GetComponent<Rigidbody>().position = GameArea2D.INSTANCE.Clamp(GetComponent<Rigidbody>().position);
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
      }
    }
  }
}