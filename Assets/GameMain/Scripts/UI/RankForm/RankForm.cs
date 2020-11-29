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
    /// 排行榜界面
    /// </summary>
    public class RankForm : UGuiForm
    {
        // 排行榜的6行
        public List<Transform> rankListItem;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            // 1.获取排行榜数据
            // 2.修改展示数据

        }
       
        protected override void OnResume()
        {

        }
        protected override void OnClose(bool isShutdown, object userData)
        {
        }


        //继续按钮点击
        public void ContinueButtionClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.JoystickForm);
            Close();
        }
    }
}
