using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace Pamux
{
  namespace Zodiac
  {

    public class LevelGeneratorParameters
    {
      internal class IntRange
      {
        private int min;
        private int max;

        public IntRange(string str)
        {
          var edges = str.Trim().Split('-');

          min = int.Parse(edges[0].Substring(1));
          max = int.Parse(edges[1].Substring(0, edges[1].Length-1));

          if (edges[0].StartsWith("("))
          {
            min++;
          }
          if (edges[1].EndsWith(")"))
          {
            max--;
          }          
        }

        

        public int getRandom(int minSkew, int maxSkew)
        {
          return UnityEngine.Random.Range(min + minSkew, max + maxSkew);
        }

        public int getRandom()
        {
          return UnityEngine.Random.Range(min, max);
        }

      }


      internal int levelId;

      internal int duration;

      internal IntRange basicEnemiesInAWave;
      internal IntRange baseEnergy;
      internal IntRange baseFirePower;

      internal string attackPatternsIntroductions;

      internal int attackPatternSymmetryOdds;
      internal int oddsOf2SimultaneousWaves;
      internal int oddsOf3SimultaneousWaves;
      internal int oddsOf4SimultaneousWaves;
      internal int oddsOf5SimultaneousWaves;
      internal int extractableCount;
      internal int crateCount;
      internal int lighthouseSatelliteCount;

      internal int helicopterAttacksPackSize;
      internal int helicopterEnergyMultiplier;
      internal int helicopterFirePowerMultiplier;

      internal int enemy1AttacksPackSize;
      internal int enemy1EnergyMultiplier;
      internal int enemy1FirePowerMultiplier;

      internal int enemy2AttacksPackSize;
      internal int enemy2EnergyMultiplier;
      internal int enemy2FirePowerMultiplier;

      internal int enemy3AttacksPackSize;
      internal int enemy3EnergyMultiplier;
      internal int enemy3FirePowerMultiplier;

      class FieldParser
      {
        private string[] fields;
        IDictionary<string, int> headerNameToColMap;

        internal FieldParser(IDictionary<string, int> headerNameToColMap, string[] fields)
        {
          this.headerNameToColMap = headerNameToColMap;
          this.fields = fields;
        }

        internal int asInteger(string colName)
        {
          string str = asString(colName);

          int val;
          if (int.TryParse(str, out val))
          {
            return val;
          }
          return 0;
        }

        internal IntRange asRange(string colName)
        {
          return new IntRange(asString(colName));
        }

        internal string asString(string colName)
        {
          colName = colName.ToLower();
          if (!headerNameToColMap.ContainsKey(colName))
          {
            Debug.Log(colName + " not found.");
            return "";
          }
          return fields[headerNameToColMap[colName]];
        }
      }


      public LevelGeneratorParameters(IDictionary<string, int> headerNameToColMap, string[] fields)
      {
        var fp = new FieldParser(headerNameToColMap, fields);

        this.levelId = fp.asInteger("levelId");
        this.duration = fp.asInteger("duration");

        this.basicEnemiesInAWave = fp.asRange("basicEnemiesInAWave");

        this.attackPatternsIntroductions = fp.asString("attackPatternsIntroductions");
        this.attackPatternSymmetryOdds = fp.asInteger("attackPatternSymmetryOdds");

        this.baseEnergy = fp.asRange("baseEnergy");
        this.baseFirePower = fp.asRange("baseFirePower");

        this.oddsOf2SimultaneousWaves = fp.asInteger("oddsOf2SimultaneousWaves");
        this.oddsOf3SimultaneousWaves = fp.asInteger("oddsOf3SimultaneousWaves");
        this.oddsOf4SimultaneousWaves = fp.asInteger("oddsOf4SimultaneousWaves");
        this.oddsOf5SimultaneousWaves = fp.asInteger("oddsOf5SimultaneousWaves");

        this.extractableCount = fp.asInteger("extractableCount");

        this.crateCount = fp.asInteger("crateCount");
        this.lighthouseSatelliteCount = fp.asInteger("lighthouseSatelliteCount");

        this.helicopterAttacksPackSize = fp.asInteger("helicopterAttacksPackSize");
        this.helicopterEnergyMultiplier = fp.asInteger("helicopterEnergyMultiplier");
        this.helicopterFirePowerMultiplier = fp.asInteger("helicopterFirePowerMultiplier");

        this.enemy1AttacksPackSize = fp.asInteger("enemy1AttacksPackSize");
        this.enemy1EnergyMultiplier = fp.asInteger("enemy1EnergyMultiplier");
        this.enemy1FirePowerMultiplier = fp.asInteger("enemy1FirePowerMultiplier");

        this.enemy2AttacksPackSize = fp.asInteger("enemy2AttacksPackSize");
        this.enemy2EnergyMultiplier = fp.asInteger("enemy2EnergyMultiplier");
        this.enemy2FirePowerMultiplier = fp.asInteger("enemy2FirePowerMultiplier");

        this.enemy3AttacksPackSize = fp.asInteger("enemy3AttacksPackSize");
        this.enemy3EnergyMultiplier = fp.asInteger("enemy3EnergyMultiplier");
        this.enemy3FirePowerMultiplier = fp.asInteger("enemy3FirePowerMultiplier");
      }

      internal void GetFutureVariant(ref PathVariants futureVariant)
      {
        if (UnityEngine.Random.Range(0, 100) < attackPatternSymmetryOdds)
        {
          futureVariant = futureVariant + 1;
          if (futureVariant == PathVariants.Max)
          {
            futureVariant = PathVariants.None;
          }
        }
      }
    }
  }
}