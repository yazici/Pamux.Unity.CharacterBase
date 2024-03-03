using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class Gauge : MonoBehaviour
  {
    public UILabel label;
    public bool hiddenOnAwake = true;

    public float fullValue = 100.0f;
    protected float _v;


    protected GameObject fgObject;
    protected MeshRenderer bgMR;
    protected MeshRenderer fgMR;




    protected virtual void SetFG() { }
    public float v
    {
      get
      {
        return _v;
      }
      set
      {
        if (_v == value)
        {
          return;
        }
        _v = value;
        SetFG();
        if (label != null)
        {
          label.text = string.Format("{0}%", (int)((_v * 100.0f) / fullValue));
          label.gameObject.transform.localPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position) - Zodiac.UI.GamePlay.uiCameraOffset;
        }
      }
    }

    protected void BaseAwake()
    {
      fgObject = new GameObject(this.gameObject.name + "FG");
      fgObject.layer = this.gameObject.layer;
      fgObject.transform.parent = this.gameObject.transform;
      fgObject.transform.localPosition = Vector3.zero;
      fgObject.transform.localRotation = Quaternion.identity;
      fgObject.transform.localScale = Vector3.one;

      if (hiddenOnAwake)
      { 
        Hide();
      }
    }

    public void Hide()
    {
      if (label != null)
      {
        label.gameObject.SetActive(false);
      }
      this.gameObject.SetActive(false);
    }

    internal IEnumerator HideLater(float duration)
    {
      yield return new WaitForSeconds(duration);
      Hide();
    }

    public void Show(float duration = 0.0f)
    {
      if (label != null)
      {
        label.gameObject.SetActive(true);
      }
      this.gameObject.SetActive(true);

      if (duration != 0.0f)
      {
        StartCoroutine(HideLater(duration));
      }
    }
  }
}
