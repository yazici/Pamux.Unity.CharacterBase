using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


namespace Pamux.Zodiac
{
    public class BossWillBeBack : Abstracts.GamePlayState
    {
        internal override IEnumerator DoRun()
        {
            yield return new WaitForSeconds(1);

            _doRunCompleted = Time.time;
            _isComplete = true;
        }
    }
}