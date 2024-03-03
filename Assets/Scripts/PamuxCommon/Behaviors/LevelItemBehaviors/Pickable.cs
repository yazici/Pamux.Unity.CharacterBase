using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pamux
{
  public enum PickableTypes
  {
      None,
      FireRateUpgrade,
      Funds,
      FundsX3,
      FundsX5,
      FundsX10,
      Life,
      Energy,
      Shield,
      Laser,
      Nuke,
      Max
  };
  
  public class Pickable : MonoBehaviour
  {
      private static float s_LastFireRateUpgradeRelease;
      private static bool s_Init = false;
  
      internal PickableStyleInfo styleInfo;
      public LevelItemData lid;
  
      void Awake()
      {
          if (!s_Init)
          {
              s_Init = true;
              s_LastFireRateUpgradeRelease = Time.time;
          }
      }
      void SetStyle(PickableTypes style)
      {
          lid.stylable.SetStyle(style.ToString());
          styleInfo = lid.stylable.styleInfo as PickableStyleInfo;
          if (styleInfo == null)
          {
              return;
          }
          (GetComponent<Renderer>() as SpriteRenderer).sprite = styleInfo.sprite;
          (GetComponent<Renderer>() as SpriteRenderer).color = styleInfo.tint;
          //transform.position = this.styleInfo.initialTransform.position;
          transform.rotation = this.styleInfo.initialTransform.rotation;
          transform.localScale = this.styleInfo.initialTransform.localScale;
      }
  
      private static void ReleaseReward(Vector3 position, PickableTypes style)
      {
          GameObject go = Zodiac.GameController.INSTANCE.pickupPool.Acquire(true, position, Quaternion.identity, Vector3.one);
  
          Pickable p = go.GetComponent<Pickable>();
  
          p.SetStyle(style);
          Abstracts.GameControllerBase.INSTANCE.summaryData.totalFunds += p.styleInfo.FundCount;
  
          Vector3 r = Random.insideUnitSphere;
          r.z = Mathf.Clamp(r.z, -0.4f, 0f);
          r.x *= 2;
          r.y = 0.0f;
          go.GetComponent<Rigidbody>().velocity = Vector3.back + r;
      }
  
  
  
      private static bool ShouldRewardFireRateUpgrade()
      {
          // max out player in first half of the game
          if (Player.INSTANCE.HasMaxFireRate())
          {
              return false;
          }
  
          return Time.time >= s_LastFireRateUpgradeRelease + 7.0f;
      }
  
      public static HashSet<float> s_ReleasedGroupRewards = new HashSet<float>();
  
      public static void ReleaseReward(Vector3 position, float groupTime, int groupSize, int count)
      {
          position.y = 0.0f;
  
          while (count> 0)
          {
              if (s_ReleasedGroupRewards.Contains(groupTime))
              {
                  ReleaseReward(position, PickableStyleInfo.GetTypeFromCount(ref count));
              }
              else
              {
                  if (ShouldRewardFireRateUpgrade())
                  {
                      s_LastFireRateUpgradeRelease = Time.time;
                      ReleaseReward(position, PickableTypes.FireRateUpgrade);
                  }
                  else
                  {
                      ReleaseReward(position, (PickableTypes) Random.Range((int)PickableTypes.Life, (int)PickableTypes.Nuke));
                  }
                  --count;
                  s_ReleasedGroupRewards.Add(groupTime);
              }
          }
      }
  
  
      void OnTriggerEnter(Collider other)
      {
          if (other.gameObject != Player.INSTANCE.gameObject)
          {
              return;
          }
  
          if (Player.LID.inventory != null)
          {
              Player.LID.inventory.Add(this.styleInfo.type);
          }
  
          ObjectPool.Release(this.gameObject);
  
          if (GetComponent<AudioSource>() != null)
          {
              AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
          }
      }
  }
}
