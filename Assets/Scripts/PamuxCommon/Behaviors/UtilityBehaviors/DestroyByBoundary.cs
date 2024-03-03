using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class DestroyByBoundary : MonoBehaviour
  {
      void DestroyFromRoot(Transform go)
      {
          if (go.transform.parent == null)
          {
              Destroy(go.gameObject);
          }
          else
          {
              DestroyFromRoot(go.transform.parent);
          }
      }
  
  	void OnTriggerExit(Collider other)
      {
          //Debug.Log("DBB " + other.gameObject.name);
          DestroyFromRoot(other.transform);
  	}
  }
}
