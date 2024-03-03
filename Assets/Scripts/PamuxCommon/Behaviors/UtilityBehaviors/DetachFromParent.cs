using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class DetachFromParent : MonoBehaviour
  {
  	void Awake ()
  	{
  		transform.parent = null;
  	}
  }
}
