using UnityEngine;
using System.Collections;


namespace Pamux
{
  public class Enemy : MonoBehaviour
  {
    internal EnemyStyleInfo styleInfo;
    public LevelItemData lid;
    internal Transform weapons;

    void SetStyle(string name)
    {
      lid.stylable.SetStyle(name);
      styleInfo = lid.stylable.styleInfo as EnemyStyleInfo;
    }

    void Awake()
    {
      weapons = MyExtensions.GetChild(this.transform, "Weapons");
    }

    void OnTriggerExit(Collider other)
    {
      if (other.GetComponent <ReleaseByBoundary>() == null)
      {
        return;
      }

      Pamux.Zodiac.GameController.INSTANCE.ResetComboLevel();
    }
    void Start()
    {
      weapons.gameObject.SetActive(true);
    }
  }
}