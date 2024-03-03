using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


namespace Pamux.Zodiac
{
    public class GameOver : Abstracts.GamePlayState
    {
        protected override void DoBeforeStart()
        {
            if (Player.INSTANCE == null)
            {
                UI.GamePlay.INSTANCE.lblStatus.text = "GAME OVER";
                UI.GamePlay.INSTANCE.lblEnergy.text = "";
            }
            else
            {
                UI.GamePlay.INSTANCE.lblStatus.text = "AWESOME!";
            }
        }

        internal override IEnumerator DoRun()
        {
            yield return new WaitForSeconds(3);
            _doRunCompleted = Time.time;
            _isComplete = true;
        }

        protected override void DoAfterCompleted()
        {
        }

    }
}