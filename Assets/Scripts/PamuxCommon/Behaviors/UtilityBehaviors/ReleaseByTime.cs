using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class ReleaseByTime : MonoBehaviour
  {
      public float lifetime;
  
  	void Start ()
  	{
          StartCoroutine(ObjectPool.Release(this.transform.gameObject, lifetime));
  	}
  }
}
