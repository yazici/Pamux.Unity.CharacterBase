using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Pamux.Zodiac
{
    public class Summary : Abstracts.GamePlayState
    {
        protected override void DoBeforeStart()
        {
            UI.GamePlay.INSTANCE.pnlSummary.enabled = true;
            UI.GamePlay.INSTANCE.pnlHUD.enabled = false;
            UI.GamePlay.INSTANCE.pnlToolbar.enabled = false;
            if (Player.IsAlive())
            {
                Player.INSTANCE.gameObject.SetActive(false);
            }

            UI.GamePlay.INSTANCE.lblBaseScoreValue.text = GameController.INSTANCE.summaryData.ScoreInfo;
            UI.GamePlay.INSTANCE.lblEnemiesKilledValue.text = GameController.INSTANCE.summaryData.EnemiesInfo;
            UI.GamePlay.INSTANCE.lblFundsCollectedValue.text = GameController.INSTANCE.summaryData.FundsInfo;
            UI.GamePlay.INSTANCE.lblMinesExploitedValue.text = GameController.INSTANCE.summaryData.ExtractablesInfo;
            UI.GamePlay.INSTANCE.lblBonusValue.text = GameController.INSTANCE.summaryData.BonusInfo;
            UI.GamePlay.INSTANCE.lblFinalScoreValue.text = GameController.INSTANCE.summaryData.FinalScoreInfo;

            App.INSTANCE.ppd.Merge(GameController.INSTANCE.summaryData);

        }

        internal override IEnumerator DoRun()
        {
            yield return new WaitForSeconds(3);
        }

        public void OnClickNext()
        {
            _doRunCompleted = Time.time;
            _isComplete = true;
            if (Player.IsAlive())
            {
                SceneManager.LoadScene("Intermission");
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}