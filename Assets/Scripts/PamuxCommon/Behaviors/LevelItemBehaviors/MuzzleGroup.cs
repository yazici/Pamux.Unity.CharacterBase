using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pamux
{
  public class MuzzleGroup : MonoBehaviour
  {

    public Transform pivot;
    public bool followPlayer;
    public float angularSpeed = 15.0f;
    public Transform[] muzzleEnds;
    public string groupIndices;
    public int currentIndex = 0;
    private List<List<int>> parsedGroups = new List<List<int>>();
    internal Weapon weapon;
    public int starMuzzlePowerOfTwo;
    private AudioSource fireAudioSource;
    public AudioClip fireSound;
    void Awake()
    {
      if (muzzleEnds.Length == 16)
      {
        SetupStarMuzzles();
      }
      ParseGroupIndices();

      if (fireSound != null)
      {
        var sfxVolume = weapon is PlayerWeapon ? App.INSTANCE.playerWeaponSfxVolume : App.INSTANCE.enemyWeaponSfxVolume;

        if (sfxVolume != 0)
        { 
          fireAudioSource = gameObject.AddComponent<AudioSource>();
          fireAudioSource.priority = 255;
          fireAudioSource.volume = sfxVolume;
          fireAudioSource.clip = fireSound;
        }
      }
    }

    internal void EnableMuzzles(int fireCount)
    {
      if (muzzleEnds.Length != 3)
      {
        return;
      }

      switch (fireCount)
      {
        case 0:
          muzzleEnds[0].gameObject.SetActive(false);
          muzzleEnds[1].gameObject.SetActive(false);
          muzzleEnds[2].gameObject.SetActive(false);
          break;
        case 1:
          muzzleEnds[0].gameObject.SetActive(false);
          muzzleEnds[1].gameObject.SetActive(true);
          muzzleEnds[2].gameObject.SetActive(false);
          break;
        case 2:
          muzzleEnds[0].gameObject.SetActive(true);
          muzzleEnds[1].gameObject.SetActive(false);
          muzzleEnds[2].gameObject.SetActive(true);
          break;
        case 3:
          muzzleEnds[0].gameObject.SetActive(true);
          muzzleEnds[1].gameObject.SetActive(true);
          muzzleEnds[2].gameObject.SetActive(true);
          break;
      }
    }


    public float damping = 6.0f;
    void LateUpdate()
    {
      if (followPlayer)
      {
        Quaternion rotation = Quaternion.LookRotation(Player.INSTANCE.transform.position - pivot.transform.position);
        pivot.transform.rotation = Quaternion.Slerp(pivot.transform.rotation, rotation, Time.deltaTime * damping);
      }
      else
      {
        pivot.Rotate(Vector3.up, angularSpeed * Time.deltaTime);
      }
    }

    private void SetupStarMuzzles()
    {
      SetActiveAll(starMuzzlePowerOfTwo >= 4);

      switch (starMuzzlePowerOfTwo)
      {
        case 0: //1
          muzzleEnds[0].gameObject.SetActive(true);
          break;
        case 1: //2
          muzzleEnds[0].gameObject.SetActive(true);
          muzzleEnds[8].gameObject.SetActive(true);
          break;
        case 2: //4
          muzzleEnds[0].gameObject.SetActive(true);
          muzzleEnds[4].gameObject.SetActive(true);
          muzzleEnds[8].gameObject.SetActive(true);
          muzzleEnds[12].gameObject.SetActive(true);
          break;
        case 3: //8
          muzzleEnds[0].gameObject.SetActive(true);
          muzzleEnds[2].gameObject.SetActive(true);
          muzzleEnds[4].gameObject.SetActive(true);
          muzzleEnds[6].gameObject.SetActive(true);
          muzzleEnds[8].gameObject.SetActive(true);
          muzzleEnds[10].gameObject.SetActive(true);
          muzzleEnds[12].gameObject.SetActive(true);
          muzzleEnds[14].gameObject.SetActive(true);
          break;
        case 4: //16
          muzzleEnds[0].gameObject.SetActive(true);
          muzzleEnds[1].gameObject.SetActive(true);
          muzzleEnds[2].gameObject.SetActive(true);
          muzzleEnds[3].gameObject.SetActive(true);
          muzzleEnds[4].gameObject.SetActive(true);
          muzzleEnds[5].gameObject.SetActive(true);
          muzzleEnds[6].gameObject.SetActive(true);
          muzzleEnds[7].gameObject.SetActive(true);
          muzzleEnds[8].gameObject.SetActive(true);
          muzzleEnds[9].gameObject.SetActive(true);
          muzzleEnds[10].gameObject.SetActive(true);
          muzzleEnds[11].gameObject.SetActive(true);
          muzzleEnds[12].gameObject.SetActive(true);
          muzzleEnds[13].gameObject.SetActive(true);
          muzzleEnds[14].gameObject.SetActive(true);
          muzzleEnds[15].gameObject.SetActive(true);
          break;
      }
    }

    private void SetActiveAll(bool active)
    {
      foreach (var muzzle in muzzleEnds)
      {
        muzzle.gameObject.SetActive(active);
      }
    }



    private void ParseGroupIndices()
    {
      if (groupIndices == "")
      {
        for (int i = 1; i < muzzleEnds.Length; ++i)
        {
          groupIndices += i + ",";
        }
        groupIndices += muzzleEnds.Length;
      }

      if (groupIndices.IndexOf(';') == -1)
      {
        List<int> g = new List<int>();
        CreateGroup(groupIndices, ref g);
        parsedGroups.Add(g);
      }
      else
      {
        foreach (var group in groupIndices.Split(';'))
        {
          List<int> g = new List<int>();
          CreateGroup(group, ref g);
          parsedGroups.Add(g);
        }
      }
    }

    private void CreateGroup(string group, ref List<int> g)
    {
      if (group.IndexOf(',') == -1)
      {
        g.Add(int.Parse(group.Trim()));
      }
      else
      {
        foreach (var gi in group.Split(','))
        {
          g.Add(int.Parse(gi.Trim()));
        }
      }
    }

    internal void Fire()
    {
      if (parsedGroups.Count == 0)
      {
        return;
      }

      if (fireAudioSource != null)
      {
        fireAudioSource.Play();
      }
      FireGroup(parsedGroups[currentIndex]);
      currentIndex++;
      if (parsedGroups.Count == currentIndex)
      {
        currentIndex = 0;
      }
    }

    private void FireGroup(List<int> group)
    {
      foreach (int muzzle in group)
      {
        FireMuzzle(muzzleEnds[muzzle - 1]);
      }
    }

    public void FireMuzzle(Transform muzzle)
    {
      if (!muzzle.gameObject.activeInHierarchy)
      {
        return;
      }

      GameObject go = Zodiac.GameController.INSTANCE.weaponFirePool.Acquire(true, muzzle.position, followPlayer ? pivot.rotation : muzzle.rotation * pivot.rotation, Vector3.one);
      go.layer = weapon is PlayerWeapon ? LayerMask.NameToLayer("PlayerWeaponFire") : LayerMask.NameToLayer("EnemyWeaponFire");

      WeaponFire wf = go.GetComponent<WeaponFire>();
      wf.Fire(weapon.fireStyle, weapon.fireEnergy);

    }
  }
}
