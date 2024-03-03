using UnityEngine;
using System.Collections;
using System;

namespace Pamux
{
  namespace Zodiac
  {
    namespace UI
    {
      public sealed class HighScores : Abstracts.UI
      {
        protected override bool DoSetMember(Transform go)
        {
          return false;
        }

        void Awake()
        {
          SetMembers();
        }

        public void OnClickLeaderboards()
        {

        }

        public void OnClickAchievements()
        {

        }

        public void OnClickChallenges()
        {

        }
      }
    }
  }
}