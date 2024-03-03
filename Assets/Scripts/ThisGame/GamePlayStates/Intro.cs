using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


namespace Pamux.Zodiac
{
    public class Intro : Abstracts.GamePlayState
    {
      protected override void DoBeforeStart()
      {
        UI.GamePlay.INSTANCE.pnlHUD.enabled = false;
        UI.GamePlay.INSTANCE.pnlToolbar.enabled = false;
        UI.GamePlay.INSTANCE.pnlSummary.enabled = false;

        GameController.INSTANCE.spaceBackgroundCamera.SetIntroValues();
      }

      internal override IEnumerator DoRun()
      {
        yield return new WaitForSeconds(5);
        _doRunCompleted = Time.time;
        _isComplete = true;
      }

    }
}