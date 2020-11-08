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
        // 匹配的6个玩家
        [SerializeField]
        public List<MatchFormPlayer> matchFormPlayers;
        [SerializeField]
        public List<Sprite> sprites;
        /// <summary>
        /// 加入按钮点击
        /// </summary>
        public void OnJoinButtonClick()
        {
        }


        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            StartCoroutine(PlayersJoin("萌忍者"));
        }
        /// <summary>
        /// 模拟玩家加入
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayersJoin(string player1Name)
        {
            // 先清掉名字和头像图片，在模拟玩家加入
            matchFormPlayers.ForEach(e =>
            {
                e.Clear();
            });
            matchFormPlayers[0].SetMatchFormPlayerInfo(player1Name, null);
            for (int i = 1; i < 6; i++)
            {
                Log.Info("PlayersJoin");
                yield return new WaitForSecondsRealtime(Random.Range(0.0f, 2f));
                matchFormPlayers[i].SetMatchFormPlayerInfo("BOT-" + i.ToString(), sprites[Random.Range(0,5)]);
            }
            yield return 0;
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
        }
    }
}
