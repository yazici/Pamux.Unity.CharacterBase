using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace Pamux
{
  namespace Zodiac
  {
    namespace UI
    {
      public sealed class Options : Abstracts.UI
      {
        private UISlider sliderSFXVolume;
        private UISlider sliderMusicVolume;
        private UIToggle chkSoundCanTravelInSpace;
        private UIToggle chkWeaponFiringSoundIsAnnoyingMe;


        protected override bool DoSetMember(Transform go)
        {
          return SetMember(go, "sliderSFXVolume", ref sliderSFXVolume)
              || SetMember(go, "sliderMusicVolume", ref sliderMusicVolume)
              || SetMember(go, "chkSoundCanTravelInSpace", ref chkSoundCanTravelInSpace)
              || SetMember(go, "chkWeaponFiringSoundIsAnnoyingMe", ref chkWeaponFiringSoundIsAnnoyingMe);
        }

        void Awake()
        {
          SetMembers();

          sliderMusicVolume.value = App.INSTANCE.musicVolume;
          sliderSFXVolume.value = App.INSTANCE.sfxVolume;
          chkSoundCanTravelInSpace.value = App.INSTANCE.soundCanTravelInSpace;
          chkWeaponFiringSoundIsAnnoyingMe.value = App.INSTANCE.weaponFiringSoundIsAnnoyingMe;
        }

        public void OnOptionsChanged()
        {
          App.INSTANCE.musicVolume = sliderMusicVolume.value;
          App.INSTANCE.soundCanTravelInSpace = chkSoundCanTravelInSpace.value;
          App.INSTANCE.weaponFiringSoundIsAnnoyingMe = chkWeaponFiringSoundIsAnnoyingMe.value;
          App.INSTANCE.sfxVolume = sliderSFXVolume.value;
          App.INSTANCE.ppd.Save();
        }

        public void OnClickCredits()
        {
          SceneManager.LoadScene("Credits");
        }
        public void OnClickHelp()
        {
          SceneManager.LoadScene("Help");
        }

        public void OnClickRestorePurchases()
        {

        }
      }
    }
  }
}