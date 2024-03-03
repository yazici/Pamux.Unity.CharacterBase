using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


namespace Pamux.Zodiac
{

    public class Outro : Abstracts.GamePlayState
    {
        internal override IEnumerator DoRun()
        {
            //GamePlay.INSTANCE.lblStatus.text = "OUTRO";

            yield return new WaitForSeconds(1);

            _doRunCompleted = Time.time;
            _isComplete = true;
        }

    }
}