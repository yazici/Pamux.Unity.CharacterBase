
namespace Pamux
{
    using UnityEngine;
    using System.Collections;

    public class App : UnityEngine.Object
    {
        public static App INSTANCE = null;
        public static Abstracts.UI UI = null;

        internal int selectedLevel = 1;
        public PersistentPlayerData ppd = new PersistentPlayerData();
        internal Difficulties selectedDifficulty = Difficulties.Easy;

        static App()
        {
            Random.seed = 42;

            INSTANCE = new App();
            DontDestroyOnLoad(INSTANCE);
        }

        public float musicVolume
        {
            get { return PlayerPrefs.HasKey("App.MusicVolume") ? PlayerPrefs.GetFloat("App.MusicVolume") : 0.0f; }
            set { PlayerPrefs.SetFloat("App.MusicVolume", value); }
        }

        public float sfxVolume
        {
            get { return PlayerPrefs.HasKey("App.SFXVolume") ? PlayerPrefs.GetFloat("App.SFXVolume") : 1.0f; }
            set { PlayerPrefs.SetFloat("App.SFXVolume", value); }
        }
        public float playerWeaponSfxVolume
        {
            get { return weaponFiringSoundIsAnnoyingMe ? 0.0f : sfxVolume / 20.0f; }
        }
        public float enemyWeaponSfxVolume
        {
            get { return soundCanTravelInSpace ? sfxVolume / 20.0f : 0.0f; }
        }

        public bool soundCanTravelInSpace
        {
            get { return PlayerPrefs.HasKey("App.SoundCanTravelInSpace") ? PlayerPrefs.GetInt("App.SoundCanTravelInSpace") != 0 : true; }
            set { PlayerPrefs.SetInt("App.SoundCanTravelInSpace", value ? 1 : 0); }
        }

        public bool weaponFiringSoundIsAnnoyingMe
        {
            get { return PlayerPrefs.HasKey("App.WeaponFiringSoundIsAnnoyingMe") ? PlayerPrefs.GetInt("App.WeaponFiringSoundIsAnnoyingMe") != 0 : true; }
            set { PlayerPrefs.SetInt("App.WeaponFiringSoundIsAnnoyingMe", value ? 1 : 0); }
        }
    }
}
