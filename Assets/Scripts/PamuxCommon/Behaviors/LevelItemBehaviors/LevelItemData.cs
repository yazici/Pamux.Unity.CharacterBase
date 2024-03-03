using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace Pamux
{
    public class LevelItemData : MonoBehaviour // TODO: Rename,should be common to all gameObjects.
    {
      public bool isPlayer = false;

      // Common to most game objects
      internal int groupId;
      internal float groupTime = 0.0f;
      internal int groupSize = 1;
      public Styleable stylable;
      public Energy energy;

      // Player ONLY
      public Inventory inventory;
      public Attractor attractor;

      internal void SetData(int groupSize, float groupTime)
      {
        this.groupSize = groupSize;
        this.groupTime = groupTime;
      }
    }
}