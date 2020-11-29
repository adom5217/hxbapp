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
    /// 结束界面
    /// </summary>
    public class OverForm : UGuiForm
    {
        public Image winnerImage;// 第一名图片
        public Text winnerName;// 第一名名字
        public Text winnerKillCount;// 第一名击杀
        public List<RankListItem> rankListItems;// 2-6名

        public List<Sprite> roleSprites;
        protected override void OnOpen(object userData)
        {
            List<PlayerData> playerDatas = new List<PlayerData>();
            for (int i = 0; i < GameData.MaxPlayer; i++)
            {
                playerDatas.Add(GameData.instance.GetPlayer(i));
            }
            playerDatas.Sort((a, b) => a.killNum == b.killNum ? 0 : a.killNum > b.killNum ? -1 : 1);

            winnerImage.sprite = roleSprites[playerDatas[0].model];
            winnerName.text = playerDatas[0].nickName;
            winnerKillCount.text = string.Format("击杀：{0}人", playerDatas[0].killNum);

            for (int i = 1; i < GameData.MaxPlayer; i++)
            {
                rankListItems[i - 1].rank.text = i.ToString();
                rankListItems[i - 1].image.sprite = roleSprites[playerDatas[i].model];
                rankListItems[i - 1].playerName.text = playerDatas[i].nickName;
                rankListItems[i - 1].killCount.text = string.Format("击杀：{0}人", playerDatas[i].killNum);
            }
        }

        public void ContinueButtionClick()
        {
            SceneController.instance.Stage.ResetGame();
            GameEntry.UI.OpenUIForm(UIFormId.JoystickForm);
            this.Close();
        }
        public void ReturnButtionClick()
        {
            ((ProcedureMain)GameEntry.Procedure.CurrentProcedure).GotoMenu();
            this.Close();
        }
    }
}
