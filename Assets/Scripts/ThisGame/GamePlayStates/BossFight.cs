using UnityEngine;
using System.Collections;

namespace Pamux.Zodiac
{
    public class BossFight : Abstracts.GamePlayState
    {
        internal override IEnumerator DoRun()
        {
            yield return new WaitForSeconds(1);

            _doRunCompleted = Time.time;
            _isComplete = true;
        }
    }
}