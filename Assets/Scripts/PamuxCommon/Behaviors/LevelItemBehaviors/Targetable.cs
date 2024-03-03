using UnityEngine;

namespace Pamux
{
  public class Targetable : MonoBehaviour
  {
      internal TargetSeeker targetedBy;
  
      void OnDestroy()
      {
          if (targetedBy != null)
          {
              targetedBy.target = null;
              this.gameObject.transform.position = Vector3.zero;
          }
      }
  }
}
