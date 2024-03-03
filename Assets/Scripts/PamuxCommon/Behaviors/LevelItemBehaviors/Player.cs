using UnityEngine;
using System.Collections;


namespace Pamux
{
  public class Player : MonoBehaviour
  {
    public static Player INSTANCE = null;
    public static LevelItemData LID = null;
    public ToolBar toolBar;

    public AudioClip ambientSound;
    public Usable laser;
    public Usable nuke;
    public Usable shield;

    public Gauge extractionGauge;

    Transform weapons;
    PlayerWeapon centerWeapon;
    PlayerWeapon leftWeapon;
    PlayerWeapon rightWeapon;
    PlayerWeapon leftMissile;
    PlayerWeapon rightMissile;

    public AudioClip weaponUpgradeSound;

    private AudioSource ambientAudioSource;
    
    void Awake()
    {
      INSTANCE = this;
      LID = GetComponent<LevelItemData>();
      LID.attractor.featureLevel = App.INSTANCE.ppd.magnetLevel;

      ambientAudioSource = gameObject.AddComponent<AudioSource>();
      ambientAudioSource.clip = ambientSound;
      ambientAudioSource.loop = true;
      ambientAudioSource.priority = 100; // 0: Highest, 255:Lowest
      ambientAudioSource.volume = App.INSTANCE.sfxVolume;
      ambientAudioSource.Play();


      weapons = MyExtensions.GetChild(this.transform, "Weapons");
      //weapons.gameObject.SetActive(false); // TODO
      extractionGauge.transform.parent = null;
      centerWeapon = MyExtensions.GetChild(weapons, "CenterWeapon").GetComponent<PlayerWeapon>();
      centerWeapon.featureLevel = App.INSTANCE.ppd.centerWeaponLevel;

      leftWeapon = MyExtensions.GetChild(weapons, "LeftWeapon").GetComponent<PlayerWeapon>();
      leftWeapon.featureLevel = App.INSTANCE.ppd.sideWeaponLevel;

      rightWeapon = MyExtensions.GetChild(weapons, "RightWeapon").GetComponent<PlayerWeapon>();
      rightWeapon.featureLevel = App.INSTANCE.ppd.sideWeaponLevel;

      leftMissile = MyExtensions.GetChild(weapons, "LeftMissileLauncher").GetComponent<PlayerWeapon>();
      leftMissile.featureLevel = App.INSTANCE.ppd.missileLevel;

      rightMissile = MyExtensions.GetChild(weapons, "RightMissileLauncher").GetComponent<PlayerWeapon>();
      rightMissile.featureLevel = App.INSTANCE.ppd.missileLevel;

      leftWeapon.gameObject.SetActive(App.INSTANCE.ppd.sideWeaponLevel > 0);
      rightWeapon.gameObject.SetActive(App.INSTANCE.ppd.sideWeaponLevel > 0);
      leftMissile.gameObject.SetActive(App.INSTANCE.ppd.missileLevel > 0);
      rightMissile.gameObject.SetActive(App.INSTANCE.ppd.missileLevel > 0);

      //LID.stylable.SetStyle("UnityShip");
      LID.stylable.SetStyle("PirateFighter");
    }

    public void AddScore(Vector3 position, int newScoreValue)
    {
      AddScore(newScoreValue);
      Zodiac.UI.GamePlay.INSTANCE.ReleaseNewScoreLabel(position, newScoreValue);
    }

    public void AddScore(int newScoreValue)
    {
      Abstracts.GameControllerBase.INSTANCE.summaryData.score += newScoreValue;
    }

    internal void MoveToScreenPoint(Vector3 sp)
    {
      Vector3 wp = Camera.main.ScreenToWorldPoint(sp);
      GetComponent<Rigidbody>().position = GameArea2D.INSTANCE.Clamp(wp);

      // Bring non-child objects with
      laser.transform.position = transform.position;
      shield.transform.position = transform.position;
      nuke.transform.position = transform.position;

      if (Zodiac.UI.GamePlay.INSTANCE == null)
      {
        return;
      }

      if (Zodiac.UI.GamePlay.INSTANCE.lblEnergy != null && Zodiac.UI.GamePlay.INSTANCE.lblEnergy.gameObject.activeInHierarchy)
      {
        Vector3 delta = new Vector3(Mathf.Sign(transform.position.x) * -1f, 0f, -2f);

        Zodiac.UI.GamePlay.INSTANCE.lblEnergy.transform.localPosition = Camera.main.WorldToScreenPoint(this.transform.position + delta) - Zodiac.UI.GamePlay.uiCameraOffset;
      }
      if (Zodiac.UI.GamePlay.INSTANCE.lblShield != null && Zodiac.UI.GamePlay.INSTANCE.lblShield.gameObject.activeInHierarchy)
      {
        Vector3 delta = new Vector3(Mathf.Sign(transform.position.x) * -1f, 0f, -2.5f);

        Zodiac.UI.GamePlay.INSTANCE.lblShield.transform.localPosition = Camera.main.WorldToScreenPoint(this.transform.position + delta) - Zodiac.UI.GamePlay.uiCameraOffset;
      }
    }

    internal void UseInventory(int slot)
    {
      toolBar.buttons[slot].OnClick();
    }

    internal static bool IsAlive()
    {
      return INSTANCE != null;
    }

    internal void UpgradeFireRate(int count)
    {
      Abstracts.GameControllerBase.INSTANCE.ComputerAnnounce(weaponUpgradeSound);

      centerWeapon.UpgradeFireRate(count);
      leftWeapon.UpgradeFireRate(count);
      rightWeapon.UpgradeFireRate(count);
      leftMissile.UpgradeFireRate(count);
      rightMissile.UpgradeFireRate(count);
    }
    internal int FireRateIndex
    {
      get
      {
        return centerWeapon.FireRateIndex;
      }
    }

    internal bool HasMaxFireRate()
    {
      return centerWeapon.HasMaxFireRate();
    }

    public bool isExtracting { get { return extractionGauge.transform.parent != null; } }
  }
}
