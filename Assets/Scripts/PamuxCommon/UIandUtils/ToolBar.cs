using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pamux
{
  public class ToolBar : MonoBehaviour
  {
      public int count = 6;
      public int width = 80;
      public int height = 80;
      public int gap = 10;
      public int innerWidth = 56;
      public int innerHeight = 56;
      public int firstButtonLeft = -225;
      public UIAtlas atlas;
  
      internal List<ToolBarButton> buttons = new List<ToolBarButton>();
  
      public EventDelegate evDel;
      void Awake()
      {
          for (int i = 0; i < count; ++i)
          {
              GameObject outer = new GameObject("btn" + i);
  
              outer.transform.parent = this.gameObject.transform;
              outer.transform.localPosition = new Vector3(firstButtonLeft + i * (width + gap), 0.0f, 0.0f);
              outer.transform.localRotation = Quaternion.identity;
              outer.transform.localScale = Vector3.one;
  
              UISprite innerSprite;
              innerSprite = outer.AddComponent<UISprite>();
              innerSprite.atlas = atlas;
              innerSprite.spriteName = "";
              innerSprite.SetDimensions(innerWidth, innerHeight);
  
              UISprite borderSprite;
              borderSprite = outer.AddComponent<UISprite>();
              borderSprite.atlas = atlas;
              borderSprite.spriteName = "BrokenCircle";
              borderSprite.SetDimensions(width, height);
  
  
              BoxCollider boxCollider = outer.AddComponent<BoxCollider>();
              boxCollider.isTrigger = true;
              boxCollider.size = new Vector3(width, height);
  
              UIButton uiButton = outer.AddComponent<UIButton>();
              uiButton.defaultColor = Color.cyan;
              uiButton.hover = Color.yellow;
              uiButton.pressed = Color.red;
              uiButton.disabledColor = Color.gray;
  
              ToolBarButton tbButton = outer.AddComponent<ToolBarButton>();
              tbButton.parent = this;
              tbButton.index = i;
              tbButton.innerSprite = innerSprite;
  
              buttons.Add(tbButton);
              EventDelegate.Add(uiButton.onClick, tbButton.OnClick);
          }
          Add("Shield");
          Add("Bolt");
          Add("Nuke");
      }
  
      internal void OnClick(int index, UISprite innerSprite)
      {
          if (innerSprite.spriteName.Length != 0)
          {
              Use(index, innerSprite.spriteName);
          }
      }
  
      internal void Add(string spriteName)
      {
          foreach (var button in buttons)
          {
              if (button.innerSprite.spriteName.Length == 0)
              {
                  button.innerSprite.spriteName = spriteName;
                  break;
              }
          }
      }
  
      internal void Use(int index, string spriteName)
      {
          if (spriteName == "Bolt")
          {
              Player.INSTANCE.laser.Use();
          }
          else if (spriteName == "Nuke")
          {
              Player.INSTANCE.nuke.Use();
          } else if (spriteName == "Shield")
          {
              Player.INSTANCE.shield.Use();
          }
          //Remove(index);
      }
  
      internal void Remove(int index)
      {
  
          //buttons[index].innerSprite.spriteName = "";
  
          for (int i = index; i < count-1; ++i)
          {
              buttons[i].innerSprite.spriteName = buttons[i + 1].innerSprite.spriteName;
          }
  
          buttons[count - 1].innerSprite.spriteName = "";
      }
  }
}
