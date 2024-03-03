using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Pamux
{
  [System.Serializable]
  public class HelpItem
  {
      public Sprite sprite;
      public Color color;
      public string description;
      public bool scaleAnimation;
      public bool rotationAnimation;
      public float baseScale = 1.0f;
  
      internal void Instantiate(GameObject prefabHelpItem)
      {
      }
  }
  
  
  public class HelpItems : MonoBehaviour
  {
      public GameObject prefabHelpItem;
      public HelpItem[] items = new HelpItem[10];
      void Awake()
      {
          if (items == null)
          {
              return;
          }
  
          int i = 0;
          foreach (HelpItem item in items)
          {
              int xPos = -8 + (i % 2) * 9;
              int zPos = 6 - Mathf.FloorToInt(i / 2) * 3;
              Vector3 position = new Vector3(xPos, 0f, zPos);
  
              GameObject go = Instantiate(prefabHelpItem, position, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
              //go.transform.parent = this.transform;
              Transform sprite = go.transform.GetChild("sprite");
              Transform label = go.transform.GetChild("lblDescription");
              label.GetComponent<TextMesh>().text = item.description;
              if (item.scaleAnimation)
              {
                  TweenScale ts = sprite.GetComponent<TweenScale>();
                  ts.from = new Vector3(item.baseScale, item.baseScale, item.baseScale);
                  ts.to = ts.from * 1.2f;
                  ts.enabled = true;
              }
              sprite.GetComponent<TweenRotation>().enabled = item.rotationAnimation;
              SpriteRenderer sr = sprite.GetComponent<SpriteRenderer>();
              sr.sprite = item.sprite;
              sr.color = item.color;
              ++i;
          }
      }
  }
}
