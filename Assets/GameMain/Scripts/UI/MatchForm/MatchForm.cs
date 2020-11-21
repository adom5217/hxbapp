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
    public class MatchForm : UGuiForm
    {
        private ProcedureMenu m_ProcedureMenu = null;
        // 匹配的6个玩家
        [SerializeField]
        public List<MatchFormPlayer> matchFormPlayers;
        [SerializeField]
        public List<Sprite> sprites;
        public Text title;
        public GameObject joinButton;
        private List<PlayerData> playerDatas;
        /// <summary>
        /// 加入按钮点击
        /// </summary>
        public void OnJoinButtonClick()
        {
            if (bMatching)
            {
                Log.Debug("匹配中...");
                //DialogParams dialogParams = new DialogParams();
                //dialogParams.Mode = 1;
                //dialogParams.Message = GameEntry.Localization.GetString("Match.Matching");
                //GameEntry.UI.OpenUIForm(UIFormId.DialogForm, dialogParams);
                return;
            }

            m_ProcedureMenu.StartGame();
            
        }

        bool bMatching = false;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }
            title.text = GameEntry.Localization.GetString("Match.Matching");
            StartCoroutine(PlayersJoin());
        }
        /// <summary>
        /// 模拟玩家加入
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayersJoin()
        {
            bMatching = true;
            playerDatas = GameData.instance.CreatPlayers();
            // TODO
            // 先清掉名字和头像图片，在模拟玩家加入
            matchFormPlayers.ForEach(e =>
            {
                e.Clear();
            });
            matchFormPlayers[0].SetMatchFormPlayerInfo(playerDatas[0].nickName, null);
            for (int i = 1; i < 6; i++)
            {
                yield return new WaitForSecondsRealtime(Random.Range(0.0f, 2f));
                matchFormPlayers[i].SetMatchFormPlayerInfo(playerDatas[i].nickName, sprites[playerDatas[i].model]);
            }
            title.text = GameEntry.Localization.GetString("Match.Success");
            bMatching = false;
        }

        protected override void OnResume()
        {
        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            m_ProcedureMenu = null;
        }
    }
}
