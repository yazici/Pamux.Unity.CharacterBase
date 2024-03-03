using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


namespace Pamux
{
  namespace Zodiac
  {

    internal class LevelItem : IComparable<LevelItem>
    {
      private ObjectPool itemPool;
      internal float time;
      internal Vector3[] itemPath;
      private float itemPathTime;
      private float itemPathDelay;
      private string itemStyle;
      private float itemBaseEnergy;
      private int itemBaseScore;
      private int itemBaseFunds;
      private int groupSize;
      private float groupTime;
      private int groupId;
      public LevelItem(ObjectPool itemPool, string itemStyle, int groupId, int groupSize, float groupTime, float time, float itemBaseEnergy, int itemBaseScore, int itemBaseFunds, float itemPathTime, float itemPathDelay)
      {
        this.itemPool = itemPool;
        this.time = time;
        this.groupSize = groupSize;
        this.groupTime = groupTime;
        this.groupId = groupId;
        this.itemPathTime = itemPathTime;
        this.itemPathDelay = itemPathDelay;
        this.itemStyle = itemStyle;
        this.itemBaseEnergy = itemBaseEnergy;
        this.itemBaseScore = itemBaseScore;
        this.itemBaseFunds = itemBaseFunds;
      }

      public GameObject Spawn()
      {

        GameObject item = itemPool.Acquire(true, itemPath[0], Quaternion.identity, Vector3.one) as GameObject;

        LevelItemData lid = item.GetComponent<LevelItemData>();
        lid.SetData(groupSize, groupTime);

        if (lid.stylable != null && itemStyle != null)
        {
          lid.stylable.SetStyle(itemStyle);
        }


        if (lid.energy != null)
        {
          lid.energy.amount = itemBaseEnergy;
          lid.energy.maxAmount = Mathf.Max(lid.energy.amount, lid.energy.maxAmount);
          lid.energy.score = itemBaseScore;
          lid.energy.funds = itemBaseFunds;
        }

        item.MoveTo(itemPath, itemPathTime, itemPathDelay, EaseType.linear);

        return item;
      }

      public int CompareTo(LevelItem other)
      {
        if (other == null) return 1;

        if (time == other.time)
        {
          return 0;
        }

        if (time < other.time)
        {
          return -1;
        }

        return 1;
      }
    }
  }
}