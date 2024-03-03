using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class SphericalMenu : MonoBehaviour
  {
      public float tumble = 1;
      public Transform innerSphere;
  
      public Transform selectionPoint;
      public float speed = 1f;
  
      Transform mTrans;
      Transform[] levelPoints = new Transform[12];
      void Start()
      {
          mTrans = transform;
      }
  
      void Awake()
      {
          Transform levelPointsParent = this.transform.GetChild("LevelPoints");
  
          levelPoints[0] = levelPointsParent.GetChild("01-Aries");
          levelPoints[1] = levelPointsParent.GetChild("02-Taurus");
          levelPoints[2] = levelPointsParent.GetChild("03-Gemini");
          levelPoints[3] = levelPointsParent.GetChild("04-Cancer");
          levelPoints[4] = levelPointsParent.GetChild("05-Leo");
          levelPoints[5] = levelPointsParent.GetChild("06-Virgo");
          levelPoints[6] = levelPointsParent.GetChild("07-Libra");
          levelPoints[7] = levelPointsParent.GetChild("08-Scorpio");
          levelPoints[8] = levelPointsParent.GetChild("09-Saggitarius");
          levelPoints[9] = levelPointsParent.GetChild("10-Capricorn");
          levelPoints[10] = levelPointsParent.GetChild("11-Aquarius");
          levelPoints[11] = levelPointsParent.GetChild("12-Pisces");
      }
      void Update()
      {
          //rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
          //innerSphere.rigidbody.angularVelocity = -rigidbody.angularVelocity;
  
          //Vector3 mousePositionSP = Input.mousePosition;
          ///Vector3 mousePositionWP = Camera.main.ScreenToWorldPoint(mousePositionSP);
  
          var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
  
          Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
          RaycastHit hit;
  
          if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "LevelSelector")
          {
              Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
  
              if (Input.GetMouseButton(0))
              {
                  Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
  
  
              }
          }
          else
          {
              Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
          }
  
  
  
          Transform closestLevelPoint = GetClosestLevelPoint();
  
      }
  
      private Transform GetClosestLevelPoint()
      {
          float closestDistance = float.MaxValue;
          Transform closestGO = null;
  
          foreach (var lp in levelPoints)
          {
              float d = Vector3.Distance(selectionPoint.position, lp.position);
              if (closestGO == null || d < closestDistance)
              {
                  closestGO = lp;
                  closestDistance = d;
              }
          }
  
          return closestGO;
      }
  
  
  
  
      void OnPress()
      {
          //UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
          //rigidbody.angularVelocity = Vector3.zero;
      }
      void OnDrag(Vector2 delta)
      {
          UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
  
  
          GetComponent<Rigidbody>().angularVelocity = new Vector3(0.5f * delta.y * speed, -0.5f * delta.x * speed, 0f);
          //innerSphere.rigidbody.angularVelocity = -rigidbody.angularVelocity;
          //mTrans.localRotation = Quaternion.Euler(0.5f * delta.y * speed, -0.5f * delta.x * speed, 0f) * mTrans.localRotation;
  
      }
  
  
  }
}
