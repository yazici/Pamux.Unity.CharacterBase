using UnityEngine;
using System.Collections;
using Pamux.Zodiac;

namespace Pamux
{
  public class Energy : MonoBehaviour
  {

    public bool conserveEnergyOnCollision = false;
    public bool selfDestructOnEmpty = true;
    public LevelItemData lid;
    public GameObject explosion;
    public Gauge energyGauge;

    public float amount = 100.0f;
    public float maxAmount = 100.0f;
    public int score = 10;
    public int funds = 1;
    internal bool IsEmpty { get { return amount == 0; } }


    private bool isPlayer;
    private bool isEnemy;

    void Awake()
    {
      isPlayer = this.tag == "Player";
      isEnemy = this.tag == "Enemy";
      Add(0.0f);
    }

    public void Remove(float amountToRemove)
    {
      if (Abstracts.GameControllerBase.INSTANCE.summaryData.untouched && isPlayer)
      {
        Abstracts.GameControllerBase.INSTANCE.summaryData.untouched = false;
      }
      Add(-amountToRemove);
      if (amount <= 0)
      {
        OutOfEnergy();
      }

      if (Player.INSTANCE == null || Abstracts.GameControllerBase.INSTANCE == null)
      {
        return;
      }

      if (amount <= 0)
      {
        if (score > 0)
        {
          Player.INSTANCE.AddScore(this.transform.position, score);
        }
        if (funds > 0)
        {
          Pickable.ReleaseReward(this.transform.position, lid.groupTime, lid.groupSize, funds);
        }
      }
    }

    public void Add(float amountToAdd)
    {
      amount = Mathf.Clamp(amount + amountToAdd, 0.0f, maxAmount);
      if (energyGauge != null)
      {
        if (energyGauge != null)
        {
          energyGauge.transform.parent = this.gameObject.transform;
          energyGauge.transform.localPosition = Vector3.zero;
          energyGauge.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
          energyGauge.transform.localScale = Vector3.one;
          energyGauge.fullValue = 100.0f;
        }
        energyGauge.v = amount;
        energyGauge.Show(3.0f);
      }
    }

    public void Exchange(Energy other)
    {
      float temp = other.amount;
      if (!other.conserveEnergyOnCollision)
      {
        other.Remove(this.amount);
      }
      if (!conserveEnergyOnCollision)
      {
        this.Remove(temp);
      }
    }
    bool KilledAllEnemiesInAWave()
    {
      return GameController.INSTANCE.summaryData.enemiesKilled % 6 == 5;
    }

    public void OutOfEnergy()
    {
      if (explosion != null)
      {
        Instantiate(explosion, transform.position, transform.rotation);
      }

      if (selfDestructOnEmpty)
      {
        ObjectPool.Release(this.gameObject);
      }

      if (isPlayer)
      {
        Player.INSTANCE = null;
      }
      else if (isEnemy)
      {
        ++GameController.INSTANCE.summaryData.enemiesKilled;
        if (KilledAllEnemiesInAWave())
        { 
          GameController.INSTANCE.AddComboLevel();
        }
      }
    }
  }
}