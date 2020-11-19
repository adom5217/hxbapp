using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    private int uuid = 100;

    public int mSkillId = 0; //技能id
    public static int MaxPlayer = 6;
    private List<PlayerData> mPlayers = new List<PlayerData>();
    public static int MaxModel= 6;
    public static int MaxItem = 9;
    public static int MaxDress = 12;
    public static int MaxSkin = 14;

    //设置开放时数据添加入数组
    public List<int> openModels = new List<int>(); //当前开放的模型 0-6 
    public List<int> openItems = new List<int>(); //当前开放的道具0-9
    public List<int> openSkins = new List<int>(); // 当前开发的皮肤装扮0-14
    void Awake()
    {
        instance = this;
        //默认开发第一个
        openModels.Add(0);
        openItems.Add(0);
        openSkins.Add(0);
    }
    //本地保存读取
    public void Read()
    {
    
    }
    public void Save()
    {
        
    }
    private PlayerData GetPlayer(int i)
    {
        var pp = new PlayerData();
        pp.uid = uuid++;
        if (i == 0)
        {
            pp.nickName = "萌宠者";
        }
        else
        {
            pp.nickName = "Bot-" + i;
            pp.model = Random.Range(0, MaxModel);
            pp.skinId = Random.Range(0, MaxSkin);
            pp.itemId = Random.Range(0, MaxItem);
            pp.dressId = Random.Range(0, MaxDress);
        }

        return pp;
    }
    public PlayerData GetPlayerSelf()
    {
        return mPlayers[0];
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
    //设置道具
    public void SetItem(int id,int index =0)
    {
        mPlayers[index].itemId = id;

    }
    //设置皮肤
    public void SetSkin(int id,int index = 0)
    {
        mPlayers[index].skinId = id;

    }
    //设置装饰
    public void SetDress(int id, int index = 0)
    {
        mPlayers[index].dressId = id;

    }
    //设置模型
    public void SetModel(int id, int index = 0)
    {
        mPlayers[index].dressId = id;

    }
}

//玩家数据 道具 皮肤 装扮 都设计 0开始下标 0-6 关联到模型上 对应关系
public class PlayerData
{
    public int uid = 0; // 唯一id
    public string nickName = ""; //昵称
    public int model = 0; //模型id
    public int itemId = 0; //道具id-武器
    public int dressId = 0; //装扮id  样式
    public int skinId = 0;//皮肤id  动物形象
    public int skillId = 0; // 技能 0 无
}