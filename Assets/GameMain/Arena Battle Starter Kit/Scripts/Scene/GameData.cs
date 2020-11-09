using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    private int uuid=100;

    public int mSkillId = 0; //技能id
    public static int MaxPlayer = 8;
    private List<PlayerData> mPlayers = new List<PlayerData>();
    void Awake()
    {
        instance = this;
        
    }
    
    private PlayerData GetPlayer(int i)
    {
        var pp = new PlayerData();
        pp.uid = uuid++;
        if(i==0)
        {
            pp.nickName = "萌宠者";
        }
        else
        {
            pp.nickName = "Bot-"+i;
            pp.skinId = Random.Range(0,6);
            pp.itemId = Random.Range(0,6);
            pp.dressId = Random.Range(0,6);
        }
        
        return pp;
    }

    //第一个玩家自己
    public List<PlayerData> CreatPlayers()
    {
        mPlayers.Clear();
        for(int i = 0;i<MaxPlayer;i++)
        {
            mPlayers.Add(GetPlayer(i));
        }
        return mPlayers;
    }


}

//玩家数据 道具 皮肤 装扮 都设计 0开始下标 0-6 关联到模型上 对应关系
public class PlayerData
{
    public int uid = 0; //唯一id
    public string nickName = ""; //昵称
    public int itemId = 0; //道具id-武器
    public int dressId = 0; //装扮id  样式
    public int skinId = 0;//皮肤id  动物形象
    public int skillId = 0; // 技能 0 无
}