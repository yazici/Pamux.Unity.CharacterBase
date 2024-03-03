using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class SFXVolumeSetter : MonoBehaviour
  {
      public bool inSpaceShip = false;
      void Awake()
      {
          if (GetComponent<AudioSource>() != null)
          {
              if (inSpaceShip || App.INSTANCE.soundCanTravelInSpace)
              {
                  GetComponent<AudioSource>().mute = false;
                  GetComponent<AudioSource>().volume = App.INSTANCE.sfxVolume;
              }
              else
              {
                  GetComponent<AudioSource>().mute = true;
                  GetComponent<AudioSource>().volume = 0f;
              }
          }
      }
  }
}
