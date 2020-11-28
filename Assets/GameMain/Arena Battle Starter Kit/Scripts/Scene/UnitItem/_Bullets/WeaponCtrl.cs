using StarForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    public WeaponStatue Type = WeaponStatue.None;
    private UnitItemBaseCtrl owner;
    private float _velocity;
    private Vector3 _moveDirection;
  
    void Start()
    {
        
    }
    public void SetData(UnitItemBaseCtrl attacker)
    {
        Type = WeaponStatue.Weapon;
        owner = attacker;
        StartCoroutine(AttackEnd(0.5f));
    }
    public void SetData(Vector3 moveDirection, float velocity, float maxDistance,UnitItemBaseCtrl attacker)
    {
        Type = WeaponStatue.Bullet;
        owner = attacker;
        this._moveDirection = moveDirection;
        this._velocity = velocity;
        
        StartCoroutine(MoveEnd(maxDistance / velocity));
    }
    IEnumerator MoveEnd(float time)
    {
        yield return new WaitForSeconds(time);
        Type = WeaponStatue.Item;
    }
    IEnumerator AttackEnd(float time)
    {
        yield return new WaitForSeconds(time);
        Type = WeaponStatue.None;
    }
    private void Update()
    {
        if (Type!=WeaponStatue.Bullet)
            return;

        this.transform.Translate(this._moveDirection * this._velocity * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (Type == WeaponStatue.None)
            return;
        UnitItemBaseCtrl unit = other.GetComponent<UnitItemBaseCtrl>();
        if (Type == WeaponStatue .Weapon)
        {//近战武器攻击
            
            if (unit != null && unit.isOurTeam != owner.isOurTeam)
            {
                if (unit.OnReceiveHit(owner.hitPoints))
                {//打死了
                    owner.Kill(unit);

                    GameEntry.Event.Fire(OnKillOneEventArgs.EventId, OnKillOneEventArgs.Create(owner.playerName, unit.playerName, owner.KillNum, unit.isPlayerSelf));
                    
                }

                Type = WeaponStatue.None;
            }
        }
        if (Type == WeaponStatue.Item)
        {//掉落拾取武器
            
            if (unit != null && unit.isPlayerSelf)
            {
                unit.SetWeapon(true);

                Destroy(this.gameObject);
                Debug.Log(unit.playerName+ " 拾取武器");
            }
           
        }
        if (Type == WeaponStatue.Bullet)
        {//扔出去武器攻击-技能
            if (unit != null && unit.isOurTeam != owner.isOurTeam)
            {
                if (unit.OnReceiveHit(owner.hitPoints))
                {//打死了
                    owner.Kill(unit);

                    GameEntry.Event.Fire(OnKillOneEventArgs.EventId, OnKillOneEventArgs.Create(owner.playerName, unit.playerName, owner.KillNum, unit.isPlayerSelf));

                    Type = WeaponStatue.Item;
                }
            }
        }
    }
}

public enum WeaponStatue
{
    None,
    Weapon,
    Bullet,
    Item,
}
