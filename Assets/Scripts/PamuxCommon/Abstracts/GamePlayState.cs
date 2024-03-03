using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace Pamux
{
  namespace Abstracts
  {
    public abstract class GamePlayState : MonoBehaviour
    {
      protected bool _isComplete = false;
      public float _doRunCompleted = 0.0f;
      public float _doRunStarted = 0.0f;
      internal GamePlayState previousState = null;
      internal virtual bool vIsComplete() { return _isComplete; }
      protected virtual void DoBeforeStart() { }
      protected virtual void DoAfterCompleted() { }

      internal bool IsRunning()
      {
        return IsStarted() && !IsComplete();
      }
      internal bool IsComplete()
      {
        if (_isComplete)
        {
          return true;
        }

        if (previousState != null)
        {
          if (!previousState.IsComplete())
          {
            return false;
          }

        }
        return IsStarted() && _doRunCompleted != 0.0f && vIsComplete();
      }
      internal abstract IEnumerator DoRun();

      internal IEnumerator Run(bool finalState = false)
      {
        if (previousState != null)
        {
          for (; !previousState.IsComplete();)
          {
            yield return new WaitForSeconds(1);
          }

          previousState.DoAfterCompleted();
        }

        _doRunStarted = Time.time;

        DoBeforeStart();

        StartCoroutine(DoRun());

        if (finalState)
        {
          for (; !IsComplete();)
          {
            yield return new WaitForSeconds(1);
          }

          DoAfterCompleted();
        }
      }
      protected bool IsStarted()
      {
        if (previousState != null)
        {
          return previousState.IsComplete();
        }
        return _doRunStarted != 0.0f;
      }

    }
  }
}