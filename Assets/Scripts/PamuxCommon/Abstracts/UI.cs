using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Pamux
{
  namespace Abstracts
  {
    public class UI : MonoBehaviour
    {
      public static Vector3 uiCameraOffset;
      protected Camera uiCamera;
      protected UIButton btnBack;

      protected bool SetMember<T>(Transform go, string name, ref T member) where T : Component
      {
        if (go != null && go.name == name)
        {
          //Debug.Log(name);
          member = go.GetComponent<T>();
          return true;
        }
        return false;
      }

      protected virtual bool DoSetMember(Transform go) { return false; }
      protected bool SetMember(Transform go)
      {
        return SetMember(go, "Camera", ref uiCamera)
            || SetMember(go, "btnBack", ref btnBack)
            || DoSetMember(go);
      }

      private void SetMembers(Transform transform)
      {
        for (int i = 0; i < transform.childCount; ++i)
        {
          Transform child = transform.GetChild(i);

          SetMember(child);
          if (child.childCount != 0)
          {
            SetMembers(child);
          }
        }
      }

      protected void SetMembers()
      {
        SetMembers(this.transform);
        uiCameraOffset = uiCamera.WorldToScreenPoint(Vector3.zero);
      }

      public void OnClickBackToMainMenu()
      {
        SceneManager.LoadScene("MainMenu");
      }
    }
  }
}