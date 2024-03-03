using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Pamux.Zodiac.UI
{
    using Pamux;

    public sealed class MainMenu : Abstracts.UI
    {

        // https://www.iconfinder.com/iconsets/glyph
        private UILabel lblTitle;
        private UILabel lblLives;
        internal UILabel lblLevel;
        private UILabel lblFunds;
        private UILabel lblTimeToNewShip;
        private UILabel lblFundsToNewShip;

        private UISprite sprTimer;

        private UIButton btnPlay;
        private UIButton btnBuyNow;
        private UIButton btnOptions;
        private UIButton btnStore;
        private UIButton btnHighScores;

        public DodecahedronMenu levelSelector;

        public Transform lblSelectDifficulty;
        public Transform grpInfo;
        public Transform grpLauncher;
        public Transform ui;
        protected override bool DoSetMember(Transform go)
        {
            return SetMember(go, "grpInfo", ref grpInfo)
                || SetMember(go, "grpLauncher", ref grpLauncher)
                || SetMember(go, "lblLives", ref lblLives)
                || SetMember(go, "lblTitle", ref lblTitle)
                || SetMember(go, "lblFunds", ref lblFunds)
                || SetMember(go, "lblLevel", ref lblLevel)
                || SetMember(go, "lblTimeToNewShip", ref lblTimeToNewShip)
                || SetMember(go, "lblFundsToNewShip", ref lblFundsToNewShip)
                || SetMember(go, "sprTimer", ref sprTimer)
                || SetMember(go, "lblSelectDifficulty", ref lblSelectDifficulty)
                || SetMember(go, "btnPlay", ref btnPlay)
                || SetMember(go, "btnStore", ref btnStore)
                || SetMember(go, "btnOptions", ref btnOptions)
                || SetMember(go, "btnHighScores", ref btnHighScores)
                || SetMember(go, "btnBuyNow", ref btnBuyNow)
                || SetMember(go, "UI", ref ui);
        }
        internal void FadeOut(float duration)
        {
            btnPlay.isEnabled = false;
            btnPlay.enabled = false;
            ui.gameObject.ShakePosition(Vector3.one / 10f, 3.0f, 0.0f);
            lblTitle.gameObject.MoveBy(Vector3.down, 30.0f, 0.0f);
            TweenAlpha.Begin(grpInfo.gameObject, duration, 0.0f);
            TweenAlpha.Begin(grpLauncher.gameObject, duration, 0.0f);
            TweenAlpha.Begin(lblLives.gameObject, duration, 0.0f);
            TweenAlpha.Begin(lblLevel.gameObject, duration, 0.0f);
            TweenAlpha.Begin(lblFunds.gameObject, duration, 0.0f);
            TweenAlpha.Begin(lblTimeToNewShip.gameObject, duration, 0.0f);
            TweenAlpha.Begin(lblFundsToNewShip.gameObject, duration, 0.0f);
            TweenAlpha.Begin(sprTimer.gameObject, duration, 0.0f);
            TweenAlpha.Begin(btnPlay.gameObject, duration, 0.0f);
            TweenAlpha.Begin(btnBuyNow.gameObject, duration, 0.0f);
            TweenAlpha.Begin(btnStore.gameObject, duration, 0.0f);
            TweenAlpha.Begin(btnOptions.gameObject, duration, 0.0f);
            TweenAlpha.Begin(btnHighScores.gameObject, duration, 0.0f);
            TweenAlpha.Begin(lblSelectDifficulty.gameObject, duration, 0.0f);
        }
        void Awake()
        {
            SetMembers();
        }

        void Start()
        {

            //string[] s_Galaxies = new string[] {
            //                                "Milky Way",
            //                                "Andromeda",
            //                                "Black Eye",
            //                                "Cartwheel",
            //                                "Pinwheel",
            //                                "Sombrero",
            //                                "Tadpole",
            //                                "Sunflower",
            //                                "Whirlpool",
            //                                "Omega Cantauri",
            //                                "Triangulum",
            //                                "3C 273",
            //                                "Cygnus A",
            //                                "Canis Major Dwarf",
            //                                "Circinus",
            //                                "IC 1127",
            //                                "Malin 1",
            //                            };


            //for (int i = 0; i < s_Galaxies.Length; ++i)
            //{
            //    Vector3 position = new Vector3(512.0f * i, 0.0f, 0.0f);
            //    GameObject go = (GameObject) Instantiate(levelSelectItemPrefab, position, Quaternion.identity);
            //    go.transform.parent = gridLevelSelectScroll.transform;
            //    gridLevelSelectScroll.AddChild(go.transform);
            //    go.transform.localScale = Vector3.one;

            //    LevelSelectItem lsi = go.GetComponent<LevelSelectItem>();
            //    lsi.Id = i+1;

            //    UILabel label = go.GetComponentInChildren<UILabel>();
            //    label.text = String.Format("{0:00}-{1}", i+1, s_Galaxies[i]);


            //    foreach (Transform child in go.transform)
            //    {
            //        if (child.name == "spriteLevelImage")
            //        {
            //            UISprite sprite = child.GetComponent<UISprite>();
            //            sprite.spriteName = String.Format("{0:00}", i+1);
            //        }
            //        else if (child.name == "spriteLevelMedals")
            //        {
            //            UISprite sprite = child.GetComponent<UISprite>();
            //            sprite.spriteName = String.Format("Medal", i+1);
            //        }
            //        else if (child.name == "spriteLevelLock")
            //        {
            //            child.gameObject.SetActive(!lsi.isPlayable);
            //        }
            //    }

            //}
        }

        private bool IsPlayableLevelSelected()
        {
            return App.INSTANCE.ppd.achievements.IsLevelUnlocked(App.INSTANCE.selectedLevel, App.INSTANCE.selectedDifficulty);
        }

        void Update()
        {
            lblLives.text = App.INSTANCE.ppd.lives + "/" + PersistentPlayerData.MAX_LIVES;
            lblFunds.text = String.Format("${0:0,0}", App.INSTANCE.ppd.funds);

            bool isMaxLives = PersistentPlayerData.MAX_LIVES == App.INSTANCE.ppd.lives;
            bool isZeroLives = 0 == App.INSTANCE.ppd.lives;

            if (isZeroLives)
            {
                btnPlay.isEnabled = false;
            }
            else
            {
                foreach (var difficultySelect in lblSelectDifficulty.GetComponentsInChildren<DifficultySelect>())
                {
                    if (difficultySelect.isSelected)
                    {
                        App.INSTANCE.selectedDifficulty = difficultySelect.difficulty;
                        break;
                    }
                }


                //App.INSTANCE.selectedLevel = (int) ZodiacSigns.Aries;


                btnPlay.isEnabled = true; // IsPlayableLevelSelected();
            }

            if (isMaxLives)
            {
                lblTimeToNewShip.text = "";
                lblFundsToNewShip.text = "MAX";
                btnBuyNow.isEnabled = false;
                sprTimer.enabled = false;
            }
            else
            {
                float timeLeft = App.INSTANCE.ppd.timeToNewShip;

                float hours = timeLeft / 3600;
                float mins = timeLeft / 60;
                float secs = timeLeft % 60;
                lblTimeToNewShip.text = String.Format("{0:00}:{1:00}:{2:00}", hours, mins, secs);

                float neededFunds = App.INSTANCE.ppd.fundsToNewShip;
                lblFundsToNewShip.text = String.Format("${0:0,0}", neededFunds);

                if (App.INSTANCE.ppd.funds >= neededFunds)
                {
                    btnBuyNow.isEnabled = true;
                    lblFundsToNewShip.color = Color.white;
                }
                else
                {
                    btnBuyNow.isEnabled = false;
                    lblFundsToNewShip.color = Color.blue;
                }
            }
        }


        public void OnClickPlay()
        {
            StartCoroutine(levelSelector.StartLevel());
        }
        public void OnClickBuyNow()
        {
            App.INSTANCE.ppd.BuyShip();
        }
        public void OnClickOptions()
        {
            SceneManager.LoadScene("Options");
        }
        public void OnClickStore()
        {
            SceneManager.LoadScene("Store");
        }
        public void OnClickHighScores()
        {
            SceneManager.LoadScene("HighScores");
        }

        public void OnClickEasy()
        {

        }
        public void OnClickMedium()
        {

        }
        public void OnClickHard()
        {

        }
    }
}
