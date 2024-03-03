using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class ReleaseByBoundary : MonoBehaviour
  {
    void OnTriggerExit(Collider other)
    {
      ObjectPool.Release(other.transform.gameObject);
    }
  }
}
