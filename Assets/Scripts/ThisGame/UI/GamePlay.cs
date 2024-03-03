using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Pamux
{
  namespace Zodiac
  {
    namespace UI
    {
      public sealed class GamePlay : Abstracts.UI
      {
        public static GamePlay INSTANCE = null;

        internal UILabel lblScore;
        internal UILabel lblTime;
        internal UILabel lblEnergy;
        internal Gauge gaugeCombo;
        internal Gauge gaugeWeaponLevel;
        internal UILabel lblShield;
        internal UILabel lblStatus;
        internal UILabel lblBaseScoreValue;
        internal UILabel lblEnemiesKilledValue;
        internal UILabel lblFundsCollectedValue;
        internal UILabel lblMinesExploitedValue;

        internal UILabel clblEnemiesKilledValue;
        internal UILabel clblFundsCollectedValue;
        internal UILabel clblMinesExploitedValue;
        internal UILabel clblFireRateUpgradesValue;
        internal UILabel clblLevelTimeValue;
        internal UILabel clblUniversalTimeValue;

        internal UILabel lblBonusValue;
        internal UILabel lblFinalScoreValue;
        internal UIButton btnPauseResume;
        internal UIPanel pnlHUD;
        internal UIPanel pnlSummary;
        internal ToolBar toolBar;
        internal UIPanel pnlToolbar;
        internal UIPanel pnlDebugConsole;
        private UILabel[] newScoreLabels; //TODO:Optimize
        internal Transform newScoreLabelPool;
        public float scoreFlightDuration = 1.0f;
        public float scoreFlightSpeed = 7.0f;
        //private UISprite spritePauseResume;
        private int score = 0;


        protected override bool DoSetMember(Transform go)
        {

          return SetMember(go, "lblEnergy", ref lblEnergy)
              || SetMember(go, "lblScore", ref lblScore)
              || SetMember(go, "lblTime", ref lblTime)
              || SetMember(go, "gaugeCombo", ref gaugeCombo)
              || SetMember(go, "gaugeWeaponLevel", ref gaugeWeaponLevel)              
              || SetMember(go, "lblStatus", ref lblStatus)
              || SetMember(go, "lblShield", ref lblShield)
              || SetMember(go, "btnPauseResume", ref btnPauseResume)
              || SetMember(go, "pnlToolBar", ref pnlToolbar)
              || SetMember(go, "pnlDebugConsole", ref pnlDebugConsole)
              || SetMember(go, "newScoreLabelPool", ref newScoreLabelPool)
              || SetMember(go, "pnlHUD", ref pnlHUD)
              || SetMember(go, "pnlSummary", ref pnlSummary)
              || SetMember(go, "lblBaseScoreValue", ref lblBaseScoreValue)
              || SetMember(go, "lblEnemiesKilledValue", ref lblEnemiesKilledValue)
              || SetMember(go, "lblFundsCollectedValue", ref lblFundsCollectedValue)
              || SetMember(go, "lblMinesExploitedValue", ref lblMinesExploitedValue)
              || SetMember(go, "clblLevelTimeValue", ref clblLevelTimeValue)
              || SetMember(go, "clblUniversalTimeValue", ref clblUniversalTimeValue)
              || SetMember(go, "clblEnemiesKilledValue", ref clblEnemiesKilledValue)
              || SetMember(go, "clblFundsCollectedValue", ref clblFundsCollectedValue)
              || SetMember(go, "clblMinesExploitedValue", ref clblMinesExploitedValue)
              || SetMember(go, "clblFireRateUpgradesValue", ref clblFireRateUpgradesValue)
              || SetMember(go, "lblBonusValue", ref lblBonusValue)
              || SetMember(go, "lblFinalScoreValue", ref lblFinalScoreValue);
        }


        void Awake()
        {
          INSTANCE = this;
          SetMembers();
          toolBar = pnlToolbar.gameObject.GetComponent<ToolBar>();
          lblStatus.text = "";
          int count = newScoreLabelPool.childCount;
          newScoreLabels = new UILabel[count];
          for (int i = 0; i < count; ++i)
          {
            newScoreLabels[i] = newScoreLabelPool.GetChild(i).GetComponent<UILabel>();
          }
          
          if (!Debug.isDebugBuild)
          {
              pnlDebugConsole.gameObject.GetComponent<UIPanel>().enabled = false;
          }
        }

        void Update()
        {
          if (Player.IsAlive())
          {
            UpdateWhenPlayerAlive();
          }
          
          if (Debug.isDebugBuild)
          {
            clblEnemiesKilledValue.text = "" + GameController.INSTANCE.summaryData.EnemiesInfo;
            clblFundsCollectedValue.text = "" + GameController.INSTANCE.summaryData.FundsInfo;
            clblMinesExploitedValue.text = "" + GameController.INSTANCE.summaryData.ExtractablesInfo;
            clblFireRateUpgradesValue.text = "" + GameController.INSTANCE.summaryData.FireRateUpgradeInfo;

            clblLevelTimeValue.text = "" + GameController.INSTANCE.mainGamePlay.LevelTime;
            clblUniversalTimeValue.text = "" + GameController.INSTANCE.mainGamePlay.UniversalTime;
          }
        }
        
        void UpdateWhenPlayerAlive()
        {
            if (gaugeCombo.label != null)
            {
              gaugeCombo.label.text = GameController.INSTANCE.ComboLevelName;
            }
            if (gaugeWeaponLevel.label != null)
            {
              gaugeWeaponLevel.label.text = "" + Player.INSTANCE.FireRateIndex;
            }
            lblEnergy.text = ((int)(Player.LID.energy.amount * 100 / Player.LID.energy.maxAmount)) + "%";

            if (score < GameController.INSTANCE.summaryData.score)
            {
              if (Time.frameCount % 5 == 0)
              {
                ++score;
                lblScore.text = String.Format("{0:000 000}", score);
              }
            }

            lblTime.text = ((int)(Time.time - GameController.INSTANCE.mainGamePlay._doRunStarted)).ToString("D3");
        }



        internal IEnumerator DisableLater(UILabel label, float duration)
        {
          yield return new WaitForSeconds(duration);
          label.enabled = false;
        }

        internal void ReleaseNewScoreLabel(Vector3 position, int score)
        {
          foreach (UILabel label in newScoreLabels)
          {
            if (label.enabled)
            {
              continue;
            }

            label.enabled = true;
            label.text = "+" + score;
            label.gameObject.transform.localPosition = Camera.main.WorldToScreenPoint(position) - uiCameraOffset;
            label.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.onUnitSphere * scoreFlightSpeed);
            StartCoroutine(DisableLater(label, scoreFlightDuration));
            break;
          }
        }

        public string testTimeMark
        {
          get { return PlayerPrefs.HasKey("App.TestTimeMark") ? PlayerPrefs.GetString("App.TestTimeMark") : ""; }
          set { PlayerPrefs.SetString("App.TestTimeMark", value); }
        }

        public void OnClickMarkLevelMoment()
        {
          testTimeMark += lblTime.text + ",";
          //Debug.Log(testTimeMark);
          App.INSTANCE.ppd.Save();
        }

        public void OnClickPauseResume()
        {
          Time.timeScale = 1.0f - Time.timeScale;

          if (Time.timeScale == 0.0f)
          {
            btnPauseResume.normalSprite = "Play";
          }
          else
          {
            btnPauseResume.normalSprite = "Pause";
          }
        }

        public void OnClickNext()
        {
          GameController.INSTANCE.GetComponent<Summary>().OnClickNext();
        }

        public void DrawHUDLines()
        {
          
        }
      }
    }
  }
}