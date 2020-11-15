using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    public GameObject[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject c in weapons)
        {
            c.SetActive(false);
        }
        int index = Random.Range(0, weapons.Length);
        weapons[index].SetActive(true);
    }

    public void Swap(int index)
    {
        if(index<weapons.Length)
            weapons[index].SetActive(true);
    }
}
