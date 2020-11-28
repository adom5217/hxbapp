using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//杀死玩家事件
public class OnKillOneEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(OnKillOneEventArgs).GetHashCode();
    public override int Id => EventId;

    public override void Clear() 
    {
        KillNum = 0;
    }
    public string AttackerName
    {
        get;
        private set;
    }
    public string BeAttackName
    {
        get;
        private set;
    }
    public int KillNum
    {
        get;
        private set;
    }
    public bool MySelf
    {
        get;
        private set;
    }
    public static OnKillOneEventArgs Create(string attacker,string beAttack,int num,bool isDead)
    {
        // 使用引用池技术，避免频繁内存分配
        OnKillOneEventArgs e = ReferencePool.Acquire<OnKillOneEventArgs>();
        e.AttackerName = attacker;
        e.BeAttackName = beAttack;
        e.KillNum = num;
        e.MySelf = isDead;
        return e;
    }

}

//获得技能事件
public class OnSkillEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(OnSkillEventArgs).GetHashCode();
    public override int Id => EventId;

    public override void Clear()
    {
        SkillID = -1;
    }
    public string ownerName
    {
        get;
        private set;
    }
    public int SkillID
    {
        get;
        private set;
    }
    public bool MySelf
    {
        get;
        private set;
    }
    public static OnSkillEventArgs Create(string owner, int id, bool isMy)
    {
        // 使用引用池技术，避免频繁内存分配
        OnSkillEventArgs e = ReferencePool.Acquire<OnSkillEventArgs>();
        e.ownerName = owner;
        e.SkillID = id;
        e.MySelf = isMy;
        return e;
    }

}

