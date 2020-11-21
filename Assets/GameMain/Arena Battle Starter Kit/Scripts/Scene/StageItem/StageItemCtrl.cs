using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageItemCtrl : MonoBehaviour {

    /* component refs */
    public GameObject[] OurTeamPositions;
    public GameObject[] EnemyTeamPositions;
    public GameObject[] GrassPositions;

    /* public vars */
    public UnitItemBaseCtrl[] ourTeam;
    public UnitItemBaseCtrl[] enemyTeam;

    void Start () {
        gameObject.SetActive(false);
    }
    //GetPlayer 012 自己方 345 敌方
    public void StartInit()
    {
        gameObject.SetActive(true);
        this.SpawnOurTeam();
        this.SpawnEnemyTeam();
    }

    public void SpawnOurTeam()
    {
        this.ourTeam = new UnitItemBaseCtrl[3];
        
        for (int i = 0; i < OurTeamPositions.Length; i++)
        {
            var dd = GameData.instance.GetPlayer(i);
            UnitItemBaseCtrl unit = SceneController.instance.AddUnitItem(dd.model, -1, this.OurTeamPositions[i].transform.position.x, this.OurTeamPositions[i].transform.position.z, true);
            if (i == 0) //自己
                SceneController.instance.SetPlayerUnit(unit);
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
            UnitItemBaseCtrl unit = SceneController.instance.AddUnitItem(dd.model, -1, this.EnemyTeamPositions[i].transform.position.x, this.EnemyTeamPositions[i].transform.position.z, false);

            unit.SetData(dd);
            this.enemyTeam[i] = unit;
        }
    }

}
