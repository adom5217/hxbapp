using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchFormPlayer : MonoBehaviour
{
    // 玩家姓名
    public Text playerName;
    // 玩家头像
    public Image playerImage;

    // 设置名字和头像的图片
    public void SetMatchFormPlayerInfo(string name, Sprite image)
    {
        if (this.playerName != null && name != null)
        {
            this.playerName.text = name;
        }
        if (this.playerImage != null && image != null)
        {
            this.playerImage.sprite = image;
            // 透明度设置回来
            Color color = playerImage.color;
            color.a = 255;
            this.playerImage.color = color;
        }
    }
    // 隐藏名字和头像
    public void Clear()
    {
        if (this.playerName != null)
        {
            this.playerName.text = "";
        }
        if (this.playerImage != null)
        {
            Color color = this.playerImage.color;
            color.a = 0;
            this.playerImage.color = color;
        }
    }
}
