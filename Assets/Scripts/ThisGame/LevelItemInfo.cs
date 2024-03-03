using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using Pamux.Assets.Scripts.Pamux.Extensions;

namespace Pamux
{
  namespace Zodiac
  {

    public class LevelItemInfo
    {
      const int COL_ITEM_TIME = 0;
      const int COL_ITEM_TYPE = 1;
      const int COL_ITEM_STYLE = 2;
      const int COL_ITEM_DELAY = 3;
      const int COL_ITEM_COUNT = 4;
      const int COL_ITEM_BASE_ENERGY = 5;
      const int COL_ITEM_BASE_SCORE = 6;
      const int COL_ITEM_BASE_FUNDS = 7;
      const int COL_ITEM_GENERATION_TYPE = 8;
      const int COL_ITEM_PATH_NAME = 9;
      const int COL_ITEM_PATH_OFFSET_X = 10;
      const int COL_ITEM_PATH_OFFSET_Y = 11;
      const int COL_ITEM_PATH_OFFSET_Z = 12;
      const int COL_ITEM_PATH_VARIANT = 13;
      const int COL_ITEM_PATH_EASE = 14;
      const int COL_ITEM_PATH_TIME = 15;
      const int COL_ITEM_PATH_DELAY = 16;

      private static Dictionary<ItemTypes, ObjectPool> itemPools = new Dictionary<ItemTypes, ObjectPool>();
      private static void EnsureItemPrefabMap()
      {
        if (itemPools.Count != 0)
        {
          return;
        }
        itemPools.Add(ItemTypes.Asteroid, GameController.INSTANCE.asteroidPool);
        itemPools.Add(ItemTypes.Enemy, GameController.INSTANCE.enemyPool);
        itemPools.Add(ItemTypes.Extractable, GameController.INSTANCE.extractablePool);
        itemPools.Add(ItemTypes.Turret, GameController.INSTANCE.turretPool);
        itemPools.Add(ItemTypes.Satellite, GameController.INSTANCE.satellitePool);
        itemPools.Add(ItemTypes.Planet, GameController.INSTANCE.planetPool);
        itemPools.Add(ItemTypes.Background, GameController.INSTANCE.backgroundObjectPool);
      }

      internal float itemTime;
      internal ItemTypes itemType = ItemTypes.None;
      internal string itemStyle;
      internal int groupId;
      internal float itemDelay = 0.5f;
      internal int itemCount = 5;
      internal float itemBaseEnergy = 100f;
      internal int itemBaseScore = 15;
      internal int itemBaseFunds = 1;
      internal StartPointGeneration itemGenerationType = StartPointGeneration.None;
      internal string itemPathName = "T2B";
      internal Vector3 itemPathOffset = Vector3.zero;
      internal PathVariants itemPathVariant = PathVariants.None;
      internal EaseType itemPathEase = EaseType.linear;
      internal float itemPathTime = 10.0f;
      internal float itemPathDelay = 0.0f;

      internal ObjectPool itemPool;

      public LevelItemInfo(int groupId, float itemTime, ItemTypes itemType, string itemStyle)
      {
        this.itemTime = itemTime;
        this.itemType = itemType;
        this.itemStyle = itemStyle;
        this.groupId = groupId;
        EnsureItemPrefabMap();
        itemPool = itemPools.ContainsKey(itemType) ? itemPools[itemType] : null;
      }
      public LevelItemInfo(int groupId, string[] header, string[] fields)
      {
        itemTime = float.Parse(fields[COL_ITEM_TIME]);

        itemType = fields[COL_ITEM_TYPE].ToEnum<ItemTypes>(ItemTypes.None);
        itemStyle = fields[COL_ITEM_STYLE];
        this.groupId = groupId;
        itemDelay = float.Parse(fields[COL_ITEM_DELAY]);
        itemCount = int.Parse(fields[COL_ITEM_COUNT]);
        itemBaseEnergy = int.Parse(fields[COL_ITEM_BASE_ENERGY]);
        itemBaseScore = int.Parse(fields[COL_ITEM_BASE_SCORE]);
        itemBaseFunds = int.Parse(fields[COL_ITEM_BASE_FUNDS]);

        itemGenerationType = fields[COL_ITEM_GENERATION_TYPE].ToEnum<StartPointGeneration>(StartPointGeneration.None);
        itemPathName = fields[COL_ITEM_PATH_NAME];
        itemPathOffset = new Vector3(float.Parse(fields[COL_ITEM_PATH_OFFSET_X]), float.Parse(fields[COL_ITEM_PATH_OFFSET_Y]), float.Parse(fields[COL_ITEM_PATH_OFFSET_Z]));

        itemPathVariant = fields[COL_ITEM_PATH_VARIANT].ToEnum<PathVariants>(PathVariants.None);
        itemPathEase = fields[COL_ITEM_PATH_EASE].ToEnum<EaseType>(EaseType.linear);

        itemPathTime = float.Parse(fields[COL_ITEM_PATH_TIME]);
        itemPathDelay = float.Parse(fields[COL_ITEM_PATH_DELAY]);

        EnsureItemPrefabMap();
        itemPool = itemPools.ContainsKey(itemType) ? itemPools[itemType] : null;
      }

      internal LevelItem GetLevelItem(int i, ref int direction)
      {
        if (itemType == ItemTypes.Enemy)
        {
          GameController.INSTANCE.summaryData.totalEnemies++;
        }
        else if (itemType == ItemTypes.Extractable)
        {
          GameController.INSTANCE.summaryData.totalExtractables++;
        }

        LevelItem wi = new LevelItem(itemPool, itemStyle, groupId, itemCount, itemTime, itemTime + itemDelay * i, itemBaseEnergy, itemBaseScore, itemBaseFunds, itemPathTime, itemPathDelay);
        Vector3 offset = Vector3.zero;
        switch (itemGenerationType)
        {
          case StartPointGeneration.Ordered:
            offset = new Vector3(itemPathOffset.x + GameArea2D.INSTANCE.left + 1.0f + (i % 5) * 3.0f, itemPathOffset.y, itemPathOffset.z);
            break;
          case StartPointGeneration.OrderedPingPong:
            if (i % 5 == 0)
            {
              direction *= -1;
            }

            if (direction == 1)
            {
              offset = new Vector3(itemPathOffset.x + GameArea2D.INSTANCE.left + 1.0f + (i % 5) * 3.0f, itemPathOffset.y, itemPathOffset.z);
            }
            else
            {
              offset = new Vector3(itemPathOffset.x + GameArea2D.INSTANCE.left + 1.0f + (4 - (i % 5)) * 3.0f, itemPathOffset.y, itemPathOffset.z);
            }


            break;
          case StartPointGeneration.Random:
            offset = new Vector3(itemPathOffset.x + GameArea2D.INSTANCE.left + 1.0f + UnityEngine.Random.Range(0.0f, 12.0f), itemPathOffset.y, itemPathOffset.z);
            break;
          case StartPointGeneration.None:
            offset = itemPathOffset;
            break;
        }

        wi.itemPath = GameArea2D.GetPath(itemPathName, itemPathVariant, offset);
        return wi;
      }
    }
  }
}