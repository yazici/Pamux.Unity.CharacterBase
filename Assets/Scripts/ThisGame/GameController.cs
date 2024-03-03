using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Pamux
{
  namespace Zodiac
  {
    public class GameController : Abstracts.GameControllerBase
    {
      //const string RESOLUTION_RETINA = "2048x1536"; // 512(4x3)
      //const string RESOLUTION_1080p = "1920x1080"; //120(16x9)
      public new static GameController INSTANCE = null;

      public new SummaryData summaryData
      {
        get
        {
          return (SummaryData)base.summaryData;
        }
      }

      public GameObject asteroidPrefab;
      public GameObject enemyPrefab;
      public GameObject extractablePrefab;
      public GameObject pickupPrefab;
      public GameObject turretPrefab;
      public GameObject satellitePrefab;
      public GameObject planetPrefab;
      public GameObject weaponFirePrefab;
      public GameObject backgroundObjectPrefab;

      internal ObjectPool asteroidPool;
      internal ObjectPool enemyPool;
      internal ObjectPool extractablePool;
      internal ObjectPool pickupPool;
      internal ObjectPool turretPool;
      internal ObjectPool satellitePool;
      internal ObjectPool planetPool;
      internal ObjectPool weaponFirePool;
      internal ObjectPool backgroundObjectPool;
      internal SpaceBackgroundCamera spaceBackgroundCamera;
      internal MainGamePlay mainGamePlay;


      private static GameObject GameObjectFactory(GameObject prefab)
      {
        return Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
      }
      private static GameObject asteroidFactory()
      {
        return GameObjectFactory(INSTANCE.asteroidPrefab);
      }
      private static GameObject enemyFactory()
      {
        return GameObjectFactory(INSTANCE.enemyPrefab);
      }
      private static GameObject extractableFactory()
      {
        return GameObjectFactory(INSTANCE.extractablePrefab);
      }
      private static GameObject pickupFactory()
      {
        return GameObjectFactory(INSTANCE.pickupPrefab);
      }
      private static GameObject turretFactory()
      {
        return GameObjectFactory(INSTANCE.turretPrefab);
      }
      private static GameObject satelliteFactory()
      {
        return GameObjectFactory(INSTANCE.satellitePrefab);
      }
      private static GameObject planetFactory()
      {
        return GameObjectFactory(INSTANCE.planetPrefab);
      }

      private static GameObject weaponFireFactory()
      {
        return GameObjectFactory(INSTANCE.weaponFirePrefab);
      }
      private static GameObject backgroundFactory()
      {
        return GameObjectFactory(INSTANCE.backgroundObjectPrefab);
      }


      void Awake()
      {
        INSTANCE = this;
        Abstracts.GameControllerBase.INSTANCE = this;

        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource.loop = true;
        musicAudioSource.priority = 128;
        musicAudioSource.clip = backgroundMusic;
        musicAudioSource.playOnAwake = true;

        arcadeAudioSource = gameObject.AddComponent<AudioSource>();
        computerAudioSource = gameObject.AddComponent<AudioSource>();

        asteroidPool = new ObjectPool(100, 10, 100, 10, asteroidFactory);
        enemyPool = new ObjectPool(20, 10, 20, 10, enemyFactory);
        extractablePool = new ObjectPool(10, 10, 200, 10, extractableFactory);
        pickupPool = new ObjectPool(10, 10, 200, 10, pickupFactory);
        turretPool = new ObjectPool(10, 10, 50, 10, turretFactory);
        satellitePool = new ObjectPool(10, 10, 50, 10, satelliteFactory);
        planetPool = new ObjectPool(10, 10, 50, 10, planetFactory);
        weaponFirePool = new ObjectPool(500, 100, 1000, 100, weaponFireFactory);
        backgroundObjectPool = new ObjectPool(500, 100, 1000, 100, backgroundFactory);

        LoadSpaceScene(App.INSTANCE.selectedLevel);
      }


      private void LoadSpaceScene(int level)
      {
        var resourcePath = string.Format("SpaceScenes/SpaceScene_{0:D2}", level);

        GameObject spaceScenePrefab = Resources.Load<GameObject>(resourcePath);
        GameObject spaceScene = Instantiate<GameObject>(spaceScenePrefab);
        spaceScene.transform.parent = this.transform;
        spaceBackgroundCamera = spaceScene.GetComponentInChildren<SpaceBackgroundCamera>();
      }

      private void ExecuteGamePlay(int level, Difficulties difficulty)
      {
        base.summaryData = new SummaryData();
        summaryData.level = level;
        summaryData.difficulty = difficulty;

        mainGamePlay = this.gameObject.AddComponent<MainGamePlay>();
        LevelGenerator lg = new LevelGenerator();

        if (lg.Generate(mainGamePlay, summaryData.level, summaryData.difficulty))
        {
          RunStates(new Abstracts.GamePlayState[] {
            this.gameObject.AddComponent<Intro>(),
            mainGamePlay,
            this.gameObject.AddComponent<BossMonologue>(),
            this.gameObject.AddComponent<BossFight>(),
            this.gameObject.AddComponent<BossWillBeBack>(),
            this.gameObject.AddComponent<Outro>(),
            this.gameObject.AddComponent<GameOver>(),
            this.gameObject.AddComponent<Summary>()
          });
        }
      }

      void Start()
      {
        musicAudioSource.volume = App.INSTANCE.musicVolume;
        ExecuteGamePlay(App.INSTANCE.selectedLevel, App.INSTANCE.selectedDifficulty);
      }

      private void RunStates(Abstracts.GamePlayState[] states)
      {
        for (int i = 1; i < states.Length; ++i)
        {
          states[i].previousState = states[i - 1];
        }

        for (int i = 0; i < states.Length; ++i)
        {
          StartCoroutine(states[i].Run(i == states.Length - 1));
        }
      }

      public AudioClip backgroundMusic;
      public AudioClip comboResetSound;
      public AudioClip[] comboLevelSounds;
      public string[] comboLevelNames;
      private int m_comboLevel;

      public void ResetComboLevel()
      {
        SetComboLevel(0);
      }
      public void AddComboLevel()
      {
        SetComboLevel(m_comboLevel + 1);
      }

      public string ComboLevelName
      {
        get
        {
          return m_comboLevel == 0 ? "" : comboLevelNames[m_comboLevel - 1];
        }
      }
      public string WeaponLevel
      {
        get
        {
          return "0";
        }
      }

      public void SetComboLevel(int comboValue)
      {
        if (m_comboLevel == comboValue)
        {
          return;
        }

        if (comboValue == 0)
        {
          ComputerAnnounce(comboResetSound);
        }
        else if (comboValue != m_comboLevel + 1)
        {
          // TODO throw
          return;
        }
        else
        {
          ArcadeAnnounce(comboLevelSounds[comboValue - 1]);
        }
        m_comboLevel = comboValue;
      }
    }
  }
}