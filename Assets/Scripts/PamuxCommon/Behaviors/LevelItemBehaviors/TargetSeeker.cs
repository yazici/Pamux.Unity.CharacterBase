using UnityEngine;
using System.Collections;


namespace Pamux
{
  public class TargetSeeker : MonoBehaviour
  {
      internal bool targetedBefore = false;
      internal Targetable target;
      public float targetingSpeed = 30f;
  
      Transform mTrans;
  
      void Start()
      {
          mTrans = transform;
      }
  
      void LateUpdate()
      {
          if (target == null && !targetedBefore)
          {
              target = Zodiac.GameController.INSTANCE.mainGamePlay.GetSeekableTarget(this.transform.position.z);
              if (target != null)
              {
                  targetedBefore = true;
                  target.targetedBy = this;
              }
          }
  
          if (target != null)
          {
              mTrans.position = new Vector3(mTrans.position.x, target.transform.position.y, mTrans.position.z);
              Vector3 dir = target.transform.position - mTrans.position;
  
              float mag = dir.magnitude;
  
              if (mag > 0.001f)
              {
                  Quaternion lookRot = Quaternion.LookRotation(dir);
                  Debug.DrawRay(this.transform.position, dir, Color.green);
                  mTrans.rotation = Quaternion.Slerp(mTrans.rotation, lookRot, Mathf.Clamp01(5 * Time.deltaTime));
                  Debug.DrawRay(this.transform.position, mTrans.rotation.eulerAngles, Color.yellow);
              }
          }
      }
  }
}
