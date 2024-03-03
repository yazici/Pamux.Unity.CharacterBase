using UnityEngine;
using System.Collections;

namespace Pamux
{
  namespace Zodiac
  {

    public class DifficultySelect : MonoBehaviour
    {

      public Difficulties difficulty = Difficulties.Easy;

      private bool _isSelected = false;
      public bool isSelected
      {
        get { return _isSelected; }
        set
        {
          if (_isSelected == value)
          {
            return;
          }
          _isSelected = value;

        }
      }
      //private UISprite sprite;
      private TweenRotation tweenRotation;
      public DifficultySelect[] others = new DifficultySelect[2];

      void Awake()
      {
        //sprite = this.gameObject.GetComponent<UISprite>();
        tweenRotation = this.gameObject.GetComponent<TweenRotation>();
      }

      void OnClick()
      {
        foreach (var other in others)
        {
          other.isSelected = false;
        }
        this.isSelected = true;
      }
    }
  }
}