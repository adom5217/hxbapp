using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBookCtrl : MonoBehaviour
{
    public GameObject[] Skills;
    public int skillid = 0; //0 飞镖 1 爆炸 2 瞬移
    void Start()
    {
        int index = Random.Range(0, Skills.Length);
        for (int i = 0; i < Skills.Length; i++)
        {
            if (i == index)
            {
                Skills[i].SetActive(true);
            }
            else
            {
                Skills[i].SetActive(false);
            }
        }
        skillid = index;
    }

   
    private void OnTriggerEnter(Collider other)
    {
        UnitItemBaseCtrl unit = other.GetComponent<UnitItemBaseCtrl>();
        if (unit != null && unit.isPlayerSelf)
        {//只有自己可以获取技能

            unit.SetSkill(skillid);
            Destroy(this.gameObject);
        }
    }
}
