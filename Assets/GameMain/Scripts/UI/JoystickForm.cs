//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class JoystickForm : UGuiForm
    {
        public static JoystickForm Ins;

        /* component refs */
        public JoystickComponentCtrl NavigatorJoystickComponent;
        public JoystickComponentCtrl AttackerJoystickComponent;

        private void Awake()
        {
            Ins = this;
        }
        
#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

           
           
        }
       private void Update()
        {
            if (SceneController.instance.player != null)
            {
                if (NavigatorJoystickComponent.Horizontal != 0 || NavigatorJoystickComponent.Vertical != 0)
                {
                    Vector3 moveVector = (Vector3.right * NavigatorJoystickComponent.Horizontal + Vector3.forward * NavigatorJoystickComponent.Vertical);
                    moveVector.Normalize();
                    SceneController.instance.player.Move(moveVector);
                }
                else
                    SceneController.instance.player.StopMove();

                if (AttackerJoystickComponent.Horizontal != 0 || AttackerJoystickComponent.Vertical != 0)
                {
                    Vector3 directionVector = (Vector3.right * AttackerJoystickComponent.Horizontal + Vector3.forward * AttackerJoystickComponent.Vertical);
                    directionVector.Normalize();
                    SceneController.instance.player.Aim(directionVector);
                }
                else
                    SceneController.instance.player.StopAim();
            }
        }

        public void OnTapAttackerJoystickComponent()
        {
            if (SceneController.instance.player != null)
                SceneController.instance.player.Attack();
        }
        
        public void OnShowSetting()
        {
            GameEntry.UI.OpenUIForm(UIFormId.SettingForm);
        }

        public void OnSkillUp()
        {
            Log.Debug("释放下技能上1");
        }
        public void OnSkillDown()
        {
            Log.Debug("释放下技能下2");
        }
    }
    
}
