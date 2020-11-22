//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.Event;
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

        public GameObject DeadView;
        public GameObject MsgTips;

        public bool EDITOR_PlAY = true;
        private void Awake()
        {
            Ins = this;
            DeadView.SetActive(false);
            MsgTips.SetActive(false);
        }
        
#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);
            GameEntry.Event.Subscribe(OnKillOneEventArgs.EventId, OnKillOneEvent);
        }
        Vector3 moveVector1 = Vector3.zero;
       private void Update()
       {
            if (SceneController.instance.player != null)
            {
                //编辑器操作简单实现一下
                if (Input.GetKeyDown (KeyCode.A)) {
                    moveVector1= new Vector3(-1,0,0);
                }else
                if (Input.GetKeyDown (KeyCode.S)) {
                    moveVector1= new Vector3(0,0,-1);
                }else
                if (Input.GetKeyDown (KeyCode.D)) {
                    moveVector1= new Vector3(1,0,0);
               
                }else
                if (Input.GetKeyDown (KeyCode.W)) {
                    moveVector1= new Vector3(0,0,1);
                }
                //抬起就停止
                if (Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.S)||Input.GetKeyUp(KeyCode.W))
                {
                    moveVector1 = Vector3.zero;
                }
                if(moveVector1!=Vector3.zero)
                {
                    moveVector1.Normalize();
                    SceneController.instance.player.Move(moveVector1);
                }else
                //摇杆操作
                if (NavigatorJoystickComponent.Horizontal != 0 || NavigatorJoystickComponent.Vertical != 0)
                {
                    Vector3 moveVector = (Vector3.right * NavigatorJoystickComponent.Horizontal + Vector3.forward * NavigatorJoystickComponent.Vertical);
                    moveVector.Normalize();
                    SceneController.instance.player.Move(moveVector);
                }
                else
                {
                    SceneController.instance.player.StopMove();
                }
                
                if (AttackerJoystickComponent.Horizontal != 0 || AttackerJoystickComponent.Vertical != 0)
                {
                    Vector3 directionVector = (Vector3.right * AttackerJoystickComponent.Horizontal + Vector3.forward * AttackerJoystickComponent.Vertical);
                    directionVector.Normalize();
                    SceneController.instance.player.Aim(directionVector);
                }
                else
                {
                    SceneController.instance.player.StopAim();
                }
            }
            
        }
        public void OnKillOneEvent(object agrs, GameEventArgs e)
        {
            var events = (OnKillOneEventArgs)e;

            //是否游戏结束
            if (events.KillNum == 10)
            {//结束了
                SceneController.instance.Stage.GameOver();
                GameEntry.UI.OpenUIForm(UIFormId.OverForm);
                return;
            }
            //提示杀死
            MsgTips.SetActive(true);
            if (events.MySelf)
            {
                MsgTips.SetText(events.AttackerName + "<color=red> 杀死了 </color> " + "(我)");
                ShowDead();
            }
            else
            {
                MsgTips.SetText(events.AttackerName + "<color=red> 杀死了 </color>" + events.BeAttackName);
            }
            HideTips();
            
        }
        int count = 3;
        private async void ShowDead()
        {
            count = 3;
            DeadView.SetActive(true);
            while (count > 0)
            {
                DeadView.SetText(count.ToString());
                await Task.Delay(1000);
                count--;
            }
            DeadView.SetActive(false);
        }
        private async void HideTips()
        {
            await Task.Delay(2000);
            MsgTips.SetActive(false);
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
            Log.Debug("释放下技能上1 加速");
        }
        public void OnSkillDown()
        {
            Log.Debug("释放下技能下2 飞镖");
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown,userData);
            GameEntry.Event.Unsubscribe(OnKillOneEventArgs.EventId, OnKillOneEvent);
        }
    }
    
}
