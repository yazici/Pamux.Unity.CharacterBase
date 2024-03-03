using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace Pamux.Zodiac
  {
    public class DifficultyParameters
    {
      public float enemyBaseEnergyMultiplier = 1.0f;
      public float enemyFireRateMultiplier = 1.0f;
      public float enemyFirePowerMultiplier = 1.0f;
      public int enemyPackSizeMinSkew = 0;
      public int enemyPackSizeMaxSkew = 0;
      public float enemySpeedMultiplier = 1.0f;
      public float playerFireRateAwardIntervalMultiplier = 1.0f;
      public int scoreAwardMultiplier = 1;
      public int fundAwardMultiplier = 1;
      public Difficulties difficulty;

      public DifficultyParameters(Difficulties difficulty)
      {
        this.difficulty = difficulty;
        switch (difficulty)
        {
          case Difficulties.Easy:
            break;
          case Difficulties.Hard:
            enemyBaseEnergyMultiplier = 1.2f;
            enemyFireRateMultiplier = 1.2f;
            enemyFirePowerMultiplier = 1.2f;
            enemyPackSizeMinSkew = 1;
            enemyPackSizeMaxSkew = 2;
            enemySpeedMultiplier = 2.0f;
            playerFireRateAwardIntervalMultiplier = 1.2f;
            scoreAwardMultiplier = 5;
            fundAwardMultiplier = 3;
            break;
          case Difficulties.Impossible:
            enemyBaseEnergyMultiplier = 1.5f;
            enemyFireRateMultiplier = 1.5f;
            enemyFirePowerMultiplier = 1.5f;
            enemyPackSizeMinSkew = 2;
            enemyPackSizeMaxSkew = 4;
            enemySpeedMultiplier = 3.0f;
            playerFireRateAwardIntervalMultiplier = 1.3f;
            scoreAwardMultiplier = 7;
            fundAwardMultiplier = 5;
            break;
        }
      }
    }
  }
