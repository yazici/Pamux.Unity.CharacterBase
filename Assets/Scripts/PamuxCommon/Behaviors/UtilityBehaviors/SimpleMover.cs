using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class SimpleMover : MonoBehaviour
  {
      public Vector3 velocity;
  
  	void Update ()
  	{
          this.gameObject.transform.position = this.gameObject.transform.position + Time.deltaTime * velocity;
  	}
  }
}
