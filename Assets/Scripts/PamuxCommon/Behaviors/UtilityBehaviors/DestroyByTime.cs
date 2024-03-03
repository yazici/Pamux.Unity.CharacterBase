using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class DestroyByTime : MonoBehaviour
  {
  	public float lifetime;
  
  	void Start ()
  	{
  		Destroy (gameObject, lifetime);
  	}
  }
}
