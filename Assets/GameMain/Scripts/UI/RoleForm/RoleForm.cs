//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 角色选择界面
    /// </summary>
    public class RoleForm : UGuiForm
    {
        // 3个面板的游戏物体
        public List<GameObject> selectGroups;
        // 3个按钮
        public List<Transform> selectButtons;
        // 玩家名字
        public Text playerName;

        private List<PlayerData> playerDatas;
        /// <summary>
        /// 点击确认按钮
        /// </summary>
        public void ConfirmButtonClick()
        {
            
          
        }

        /// <summary>
        /// 点击重置按钮
        /// </summary>
        public void ResetButtonClick()
        {
           
        }
        /// <summary>
        /// 选择角色
        /// </summary>
        /// <param name="value"></param>
        public void OnRoleSelected(int value)
        {

        }

        /// <summary>
        /// 切换选择面板
        /// </summary>
        /// <param name="groupIndex"></param>
        public void ChangeGroup(int groupIndex)
        {
            if (selectGroups == null || selectGroups.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < selectGroups.Count; i++)
            {
                if (i == groupIndex)
                {
                    selectGroups[i].SetActive(true);
                }
                else
                {
                    selectGroups[i].SetActive(false);
                }
            }
            // 按钮放大缩小
            for (int i = 0; i < selectButtons.Count; i++)
            {
                if (i == groupIndex)
                {
                    selectButtons[i].SetLocalScaleX(1.2f);
                    selectButtons[i].SetLocalScaleY(1.2f);
                }
                else
                {
                    selectButtons[i].SetLocalScaleX(1);
                    selectButtons[i].SetLocalScaleY(1);
                }
            }
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            //玩家数据赋值
            playerDatas = (List<PlayerData>)userData;
            // 设置上玩家名字
            this.playerName.text = playerDatas[0].nickName;
            // 默认选择第一个
            this.ChangeGroup(0);
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {

        }
        protected override void OnResume()
        {

        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            GameEntry.UI.OpenUIForm(UIFormId.MatchForm);
        }
    }
}
