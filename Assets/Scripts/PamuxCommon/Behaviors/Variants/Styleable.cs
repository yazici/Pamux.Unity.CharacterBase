using UnityEngine;
using System.Collections;

namespace Pamux
{
  public static class MyExtensions
  {
      static public Transform GetChild(this Transform parent, string childName)
      {
          childName = childName.ToLower();
          for (int i = 0; i < parent.childCount; ++i)
          {
              Transform child = parent.GetChild(i);
              if (child.name.ToLower() == childName)
              {
                  return child;
              }
          }
          // throw new System.Exception("Child transform doesn't exist: " + childName);
          return null;
      }
  
      static public Transform GetGrandChild(this Transform parent, string childName, string grandChildName)
      {
          Transform child = parent.GetChild(childName);
          if (child == null)
          {
              return null;
          }
          return child.GetChild(grandChildName);
      }
      static public void DeactivateChildren(this Transform parent)
      {
          for (int i = 0; i < parent.childCount; ++i)
          {
              Transform child = parent.GetChild(i);
              child.gameObject.SetActive(false);
          }
      }

    static public void ActivateOnlyOne(this Transform parent, string prefix, string activeName)
    {
      for (int i = 0; i < parent.childCount; ++i)
      {
        Transform child = parent.GetChild(i);
        child.gameObject.SetActive(false);
      }
    }
  }
  
  public class Styleable : MonoBehaviour
  {
      private BoxCollider boxCollider;
      private CapsuleCollider capsuleCollider;
      private SphereCollider sphereCollider;
      private MeshCollider meshCollider;
  
      internal StyleInfo styleInfo;
      internal Transform stylesRoot;
      public LevelItemData lid;
  
      internal void SetStyle(string styleName) // Case sensitive
      {
  
          if (stylesRoot == null)
          {
              stylesRoot = this.gameObject.transform.GetChild("Style");
              if (stylesRoot == null)
              {
                  throw new System.Exception("Style root not found for: " + this.gameObject.name);
              }
              stylesRoot.gameObject.layer = this.gameObject.layer;
              stylesRoot.DeactivateChildren();
          }
          Transform style = stylesRoot.GetChild(styleName);
          if (style == null)
          {
              throw new System.Exception("Style not found: " + styleName);
          }
          this.styleInfo = style.gameObject.GetComponent<StyleInfo>();
          if (styleInfo == null)
          {
              throw new System.Exception("StyleInfo not found: " + styleName + " GO:" + style.gameObject.name);
          }
  
          if (this.styleInfo.initialTransform!= null)
          {
              transform.position = this.styleInfo.initialTransform.position;
              //transform.rotation = this.styleInfo.initialTransform.rotation;
              transform.localScale = this.styleInfo.initialTransform.localScale;
  
          }
          style.gameObject.layer = this.gameObject.layer;
  
          StylizePhysics(style);
  
  
          Zodiac.ContactDamage cd = style.gameObject.GetComponent<Zodiac.ContactDamage>();
          if (cd == null)
          {
              cd = style.gameObject.AddComponent<Zodiac.ContactDamage>();
              cd.lid = lid;
          }
          cd.enabled = true;
  
          style.gameObject.SetActive(true);
      }
  
      private void StylizePhysics(Transform style)
      {
          if (GetComponent<Collider>() != null)
          {
              return;
          }
  
          if (GetComponent<Rigidbody>() != null)
          {
              boxCollider = this.gameObject.AddComponent<BoxCollider>();
              capsuleCollider = this.gameObject.AddComponent<CapsuleCollider>();
              sphereCollider = this.gameObject.AddComponent<SphereCollider>();
              meshCollider = this.gameObject.AddComponent<MeshCollider>();
  
  
              if (styleInfo.mass != 0.0f)
              {
                  GetComponent<Rigidbody>().mass = styleInfo.mass;
              }
              GetComponent<Rigidbody>().drag = styleInfo.drag;
              GetComponent<Rigidbody>().angularDrag = styleInfo.angularDrag;
          }
  
          if (style.GetComponent<Collider>() is BoxCollider)
          {
              BoxCollider styleCollider = style.GetComponent<Collider>() as BoxCollider;
  
              boxCollider.center = styleCollider.center;
              boxCollider.size = styleCollider.size;
              boxCollider.isTrigger = true;
  
              boxCollider.enabled = true;
              capsuleCollider.enabled = false;
              sphereCollider.enabled = false;
              meshCollider.enabled = false;
          }
          else if (style.GetComponent<Collider>() is CapsuleCollider)
          {
              CapsuleCollider styleCollider = style.GetComponent<Collider>() as CapsuleCollider;
  
              capsuleCollider.center = styleCollider.center;
              capsuleCollider.radius = styleCollider.radius;
              capsuleCollider.height = styleCollider.height;
              capsuleCollider.direction = styleCollider.direction;
              capsuleCollider.isTrigger = true;
  
              boxCollider.enabled = false;
              capsuleCollider.enabled = true;
              sphereCollider.enabled = false;
              meshCollider.enabled = false;
          }
          else if (style.GetComponent<Collider>() is SphereCollider)
          {
              SphereCollider styleCollider = style.GetComponent<Collider>() as SphereCollider;
  
              sphereCollider.center = styleCollider.center;
              sphereCollider.radius = styleCollider.radius;
              sphereCollider.isTrigger = true;
  
              boxCollider.enabled = false;
              capsuleCollider.enabled = false;
              sphereCollider.enabled = true;
              meshCollider.enabled = false;
          }
          else if (style.GetComponent<Collider>() is MeshCollider)
          {
              MeshCollider styleCollider = style.GetComponent<Collider>() as MeshCollider;
  
              meshCollider.sharedMesh = styleCollider.sharedMesh;
              meshCollider.sharedMaterial = styleCollider.sharedMaterial;
              meshCollider.convex = styleCollider.convex;
              meshCollider.smoothSphereCollisions = styleCollider.smoothSphereCollisions;
              meshCollider.isTrigger = true;
  
              boxCollider.enabled = false;
              capsuleCollider.enabled = false;
              sphereCollider.enabled = false;
              meshCollider.enabled = true;
          }
  
      }
  
  
  }
}
