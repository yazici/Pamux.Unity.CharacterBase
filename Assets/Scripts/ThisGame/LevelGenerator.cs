using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace Pamux.Zodiac
{
    using Pamux.Interfaces;
    public class LevelGenerator : ITextAssetHandler
    {
        private List<LevelGeneratorParameters> lgps = new List<LevelGeneratorParameters>();
        private Dictionary<int, int> levelStartIndices = new Dictionary<int, int>();

        // Helper members (instead of passing as parameters)
        private DifficultyParameters difficultyParameters;
        private MainGamePlay mainGamePlay;
        private List<string> attackPatterns = new List<string>();
        private HashSet<string> attackPatternsSet = new HashSet<string>();
        private PathVariants futureVariant = PathVariants.None;
        private LevelGeneratorParameters lgp;

        public LevelGenerator()
        {
            TextAssetHelper.Load("LevelGenerationParameters", this);
            levelStartIndices[1] = 0;
        }

        #region ITextAssetHandler
        public void SetVariable(string name, string value)
        {

        }

        public void AddItems(IDictionary<string, int> headerNameToColMap, string[] fields)
        {
            var lgp = new LevelGeneratorParameters(headerNameToColMap, fields);

            if (!levelStartIndices.ContainsKey(lgp.levelId))
            {
                levelStartIndices[lgp.levelId] = lgps.Count;
            }

            lgps.Add(lgp);
        }

        public void OnItemsReady()
        {

        }
        #endregion


        internal bool Generate(MainGamePlay mainGamePlay, int levelId, Difficulties difficulty)
        {
            this.mainGamePlay = mainGamePlay;
            this.difficultyParameters = new DifficultyParameters(difficulty);
            attackPatterns.Clear();

            int levelStartIndex = levelStartIndices[levelId];
            int levelEndIndex = levelStartIndices.ContainsKey(levelId + 1) ? levelStartIndices[levelId + 1] : lgps.Count;

            int currentTime = 0;
            int currentIndex = 0;

            for (; currentIndex < levelStartIndex; ++currentIndex)
            {
                this.lgp = lgps[currentIndex];
                introduceAttackPattern(this.lgp.attackPatternsIntroductions);
                currentTime += this.lgp.duration;
            }

            mainGamePlay.startFrom = currentTime;

            for (; currentIndex < levelEndIndex; ++currentIndex)
            {
                this.lgp = lgps[currentIndex];
                introduceAttackPattern(this.lgp.attackPatternsIntroductions);
                GenerateWaves(currentTime, currentTime + this.lgp.duration);
                currentTime += this.lgp.duration;
            }

            return true;
        }

        private void introduceAttackPattern(string attackPatternsIntroductions)
        {
            if (!attackPatternsSet.Contains(attackPatternsIntroductions))
            {
                attackPatternsSet.Add(attackPatternsIntroductions);
                attackPatterns.Add(attackPatternsIntroductions);
            }
        }

        private void GenerateWaves(int startTime, int endTime)
        {
            int waveId = 0;

            for (int t = startTime; t < endTime; ++t)
            {
                if (t % 5 == 0)
                {
                    waveId++;
                    GenerateBasicEnemyWaves(waveId, t);

                    lgp.GetFutureVariant(ref futureVariant);
                }

                if (lgp.extractableCount > 0) // TODO
                {
                    if (UnityEngine.Random.Range(1, 100) > 100 / (lgp.extractableCount + 1))
                    {
                        --lgp.extractableCount;
                        GenerateExtractables(waveId, t);
                    }
                }

                if (lgp.crateCount > 0) // TODO
                {
                    if (UnityEngine.Random.Range(1, 100) > 100 / (lgp.crateCount + 1))
                    {

                        --lgp.crateCount;
                        GenerateBackgroundObjects(waveId, t, "Crate");
                    }
                }

                if (lgp.lighthouseSatelliteCount > 0) // TODO
                {
                    if (UnityEngine.Random.Range(1, 100) > 100 / (lgp.lighthouseSatelliteCount + 1))
                    {

                        --lgp.lighthouseSatelliteCount;
                        GenerateBackgroundObjects(waveId, t, "Lighthouse");
                    }
                }

                if (lgp.helicopterAttacksPackSize > 0) // TODO
                {
                    if (UnityEngine.Random.Range(1, 100) > 100 / (lgp.helicopterAttacksPackSize + 1))
                    {
                        GenerateAdvancedEnemyWaves(waveId, t, string.Format("MiniBoss{0:00}", UnityEngine.Random.Range(1, 8)));
                        lgp.helicopterAttacksPackSize = 0;
                    }
                }

            }
        }

        private void GenerateBasicEnemyWaves(int waveId, int t)
        {
            LevelItemInfo lii = new LevelItemInfo(waveId, t, ItemTypes.Enemy, string.Format("Enemy{0:00}", UnityEngine.Random.Range(1, 8)));

            lii.itemDelay = 0.3f;
            lii.itemCount = lgp.basicEnemiesInAWave.getRandom(difficultyParameters.enemyPackSizeMinSkew, difficultyParameters.enemyPackSizeMaxSkew);
            lii.itemBaseEnergy = lgp.baseEnergy.getRandom() * difficultyParameters.enemyBaseEnergyMultiplier;
            lii.itemBaseScore *= difficultyParameters.scoreAwardMultiplier;
            lii.itemBaseFunds *= difficultyParameters.fundAwardMultiplier;

            lii.itemGenerationType = StartPointGeneration.Ordered;

            lii.itemPathName = attackPatterns[UnityEngine.Random.Range(0, attackPatterns.Count - 1)];
            lii.itemPathOffset = Vector3.zero;
            lii.itemPathVariant = futureVariant;

            //lii.itemPathEase = EaseType.linear;
            lii.itemPathTime /= difficultyParameters.enemySpeedMultiplier;
            //lii.itemPathDelay = 0.0f;

            int direction = -1;
            for (int i = 0; i < lii.itemCount; ++i)
            {
                mainGamePlay.Add(lii.GetLevelItem(i, ref direction));
            }
        }
        private void GenerateExtractables(int waveId, int t)
        {
            LevelItemInfo lii = new LevelItemInfo(waveId, t, ItemTypes.Extractable, "MiningPost");


            lii.itemCount = 1;
            lii.itemGenerationType = StartPointGeneration.Random;
            lii.itemPathName = "T2B";
            lii.itemPathOffset = Vector3.zero;
            lii.itemPathVariant = PathVariants.None;

            //lii.itemPathEase = EaseType.linear;
            lii.itemPathTime = 40.0f; // TODO Difficulty

            //lii.itemPathDelay = 0.0f;

            int direction = -1;
            for (int i = 0; i < lii.itemCount; ++i)
            {
                mainGamePlay.Add(lii.GetLevelItem(i, ref direction));
            }
        }
        private void GenerateBackgroundObjects(int waveId, int t, string style)
        {
            LevelItemInfo lii = new LevelItemInfo(waveId, t, ItemTypes.Background, style);

            lii.itemCount = 1;
            lii.itemGenerationType = StartPointGeneration.Random;
            lii.itemPathName = "T2B";
            lii.itemPathOffset = Vector3.zero;
            lii.itemPathVariant = PathVariants.None;
            lii.itemBaseEnergy = 15000.0f;
            //lii.itemPathEase = EaseType.linear;
            lii.itemPathTime = 20.0f;

            //lii.itemPathDelay = 0.0f;

            int direction = -1;
            for (int i = 0; i < lii.itemCount; ++i)
            {
                mainGamePlay.Add(lii.GetLevelItem(i, ref direction));
            }

        }
        private void GenerateAdvancedEnemyWaves(int waveId, int t, string style)
        {
            LevelItemInfo lii = new LevelItemInfo(waveId, t, ItemTypes.Enemy, style);

            lii.itemDelay = 2.5f;
            lii.itemCount = lgp.helicopterAttacksPackSize;
            lii.itemBaseEnergy = lgp.baseEnergy.getRandom() * difficultyParameters.enemyBaseEnergyMultiplier * lgp.helicopterEnergyMultiplier;
            lii.itemBaseScore *= difficultyParameters.scoreAwardMultiplier;
            lii.itemBaseFunds *= difficultyParameters.fundAwardMultiplier;

            //lii.itemGenerationType = StartPointGeneration.Ordered;

            lii.itemPathName = "TR2BL";
            lii.itemPathOffset = Vector3.zero;
            lii.itemPathVariant = PathVariants.None;

            //lii.itemPathEase = EaseType.linear;
            lii.itemPathTime /= difficultyParameters.enemySpeedMultiplier;
            //lii.itemPathDelay = 0.0f;

            int direction = -1;
            for (int i = 0; i < lii.itemCount; ++i)
            {
                mainGamePlay.Add(lii.GetLevelItem(i, ref direction));
            }
        }
    }
}
