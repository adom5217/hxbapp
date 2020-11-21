//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class MenuForm : UGuiForm
    {

        public Toggle soundToggle;//音效
        public Toggle musicToggle;//背景音乐

        [SerializeField]
        private GameObject m_QuitButton = null;

        private ProcedureMenu m_ProcedureMenu = null;

        public void OnStartButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.MatchForm, m_ProcedureMenu);
        }

        public void TestButton()
        {
            Log.Debug("这里添加测试打开UI Form");

            //随便测试


        }
        public void OnMusicMuteChanged(bool isOn)
        {
            GameEntry.Sound.Mute("Music", isOn);
            GameEntry.Sound.SetVolume("Music", isOn ? 0 : 50);
        }
        public void OnSoundMuteChanged(bool isOn)
        {
            GameEntry.Sound.Mute("Sound", isOn);
            GameEntry.Sound.Mute("UISound", isOn);
            GameEntry.Sound.SetVolume("Sound", isOn ? 0 : 100);
            GameEntry.Sound.SetVolume("UISound", isOn ? 0 : 100);
        }
        public void OnSettingButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.SettingForm);
        }

        public void OnAboutButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.AboutForm);
        }

        public void OnQuitButtonClick()
        {
            GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 1,
                Title = GameEntry.Localization.GetString("AskQuitGame.Title"),
                Message = GameEntry.Localization.GetString("AskQuitGame.Message"),
                OnClickConfirm = delegate (object userData) { UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit); },
            });
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }

            m_QuitButton.SetActive(Application.platform != RuntimePlatform.IPhonePlayer);

            soundToggle.isOn = GameEntry.Sound.IsMuted("Sound");
            musicToggle.isOn = GameEntry.Sound.IsMuted("Music");
            GameEntry.Sound.SetVolume("Sound", soundToggle.isOn ? 0 : 100);
            GameEntry.Sound.SetVolume("UISound", soundToggle.isOn ? 0 : 100);
            GameEntry.Sound.SetVolume("Music", musicToggle.isOn ? 0 : 50);
        }
        protected override void OnResume()
        {
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            m_ProcedureMenu = null;

            base.OnClose(isShutdown, userData);
        }
    }
}
