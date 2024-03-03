using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pamux
{
  public class MakeRootOnAwake : MonoBehaviour
  {
      void Awake()
      {
          this.transform.parent = null;
      }
  }
}
