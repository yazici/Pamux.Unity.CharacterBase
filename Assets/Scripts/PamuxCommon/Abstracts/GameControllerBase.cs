using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Pamux.Abstracts
{
  public abstract class GameControllerBase : MonoBehaviour
  {
    public const string RESOLUTION_4K = "???2048x1536"; // 512(4x3)
    public const string RESOLUTION_RETINA = "2048x1536"; // 512(4x3)
    public const string RESOLUTION_1080p = "1920x1080"; //120(16x9)
    public static GameControllerBase INSTANCE = null;

    public SummaryDataBase summaryData;

    public Camera uiCamera;
    public Camera mainCamera;
    public Camera fxCamera;

    protected AudioSource arcadeAudioSource;
    protected AudioSource computerAudioSource;
    protected AudioSource musicAudioSource;

    public void ComputerAnnounce(AudioClip clip)
    {
      if (clip != null && computerAudioSource != null)
      {
        computerAudioSource.PlayOneShot(clip);
      }
    }

    public void ArcadeAnnounce(AudioClip clip)
    {
      if (clip != null && arcadeAudioSource != null)
      {
        arcadeAudioSource.PlayOneShot(clip);
      }
    }
  }
}
