//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class MatchForm : UGuiForm
    {

        // 菜单流程对象
        private ProcedureMenu m_ProcedureMenu = null;
        // 匹配的6个玩家
        [SerializeField]
        public List<MatchFormPlayer> matchFormPlayers;

        private bool excuteMatch = false;

        /// <summary>
        /// 加入按钮点击
        /// </summary>
        public void OnJoinButtonClick()
        {
            //GameEntry.UI.OpenUIForm(UIFormId.SettingForm);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MatchForm.");
                return;
            }
        }
        /// <summary>
        /// 模拟玩家加入
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayersJoin()
        {

            for (int i = 1; i < 6; i++)
            {
                yield return new WaitForSecondsRealtime(Random.Range(0.0f, 2f));
                matchFormPlayers[i].SetMatchFormPlayerInfo("BOT-" + i.ToString(), null);
            }
            yield return 0;
        }
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (!excuteMatch)
            {
                // 先清掉名字和头像图片，在模拟玩家加入
                matchFormPlayers.ForEach(e =>
                {
                    e.Clear();
                });
                matchFormPlayers[0].SetMatchFormPlayerInfo("萌忍者", null);
                StartCoroutine(PlayersJoin());
                excuteMatch = !excuteMatch;
            }
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            m_ProcedureMenu = null;
            base.OnClose(isShutdown, userData);
        }
    }
}
