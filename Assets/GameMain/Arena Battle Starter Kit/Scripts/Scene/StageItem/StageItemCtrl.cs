using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StageItemCtrl : MonoBehaviour {

    /* component refs */
    public GameObject[] OurTeamPositions;
    public GameObject[] EnemyTeamPositions;
    public GameObject[] GrassPositions;

    /* public vars */
    public UnitItemBaseCtrl[] ourTeam;
    public UnitItemBaseCtrl[] enemyTeam;

    public GameObject SkillBook;

    void Start () {
        gameObject.SetActive(false);
    }
    //GetPlayer 012 自己方 345 敌方
    public void StartInit()
    {
        gameObject.SetActive(true);
        this.SpawnOurTeam();
        this.SpawnEnemyTeam();
        SpawnSkill();
    }

    public void SpawnOurTeam()
    {
        this.ourTeam = new UnitItemBaseCtrl[3];
        
        for (int i = 0; i < OurTeamPositions.Length; i++)
        {
            var dd = GameData.instance.GetPlayer(i);
            UnitItemBaseCtrl unit = SceneController.instance.AddUnitItem(dd.model, dd.uid, this.OurTeamPositions[i].transform.position.x, this.OurTeamPositions[i].transform.position.z, true);
            if (i == 0) //自己
            {
                unit.isPlayerSelf = true;
                SceneController.instance.SetPlayerUnit(unit);
            }
            unit.SetData(dd);
            this.ourTeam[i] = unit;
        }
    }

    public void SpawnEnemyTeam()
    {
        this.enemyTeam = new UnitItemBaseCtrl[3];

        for (int i = 0; i < EnemyTeamPositions.Length; i++)
        {
            var dd = GameData.instance.GetPlayer(i+3);
            UnitItemBaseCtrl unit = SceneController.instance.AddUnitItem(dd.model, dd.uid, this.EnemyTeamPositions[i].transform.position.x, this.EnemyTeamPositions[i].transform.position.z, false);

            unit.SetData(dd);
            this.enemyTeam[i] = unit;
        }
    }
    //开始后随机3-5秒出现一个 技能
    public async void SpawnSkill()
    {
        int num = Random.Range(0, 2);
        await Task.Delay(3000+ num*1000);
        var obj = Utils.CreateInstance(SkillBook, gameObject, true);
        int i = Random.Range(0, GrassPositions.Length);
        obj.transform.position = GrassPositions[i].transform.position + new Vector3(0, 1f, 0);
    }
    private void StopAI()
    {
        foreach (var u in this.enemyTeam)
        {
            u.aiHelper.enabled = false;
        }

        foreach (var u in this.ourTeam)
        {
            u.aiHelper.enabled = false;
        }
    }

    //重新开始
    public void ResetGame()
    {
        foreach (var u in this.enemyTeam)
        {
            u.aiHelper.enabled = true;
            u.Respawn();
            u.ResetData();
        }
        foreach (var u in this.ourTeam)
        {
            if(!u.isPlayerSelf)
                u.aiHelper.enabled = true;
            u.Respawn();
            u.ResetData();
        }
        //重新生成技能
        SpawnSkill();

    }

    public void GameOver()
    {
        StopAI();
    }
}
