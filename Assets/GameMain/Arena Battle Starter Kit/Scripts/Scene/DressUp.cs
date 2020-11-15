using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressUp : MonoBehaviour
{
    public GameObject[] mHeads;
    public GameObject[] mAccessories;
    public Material[] mFaces;
    public Renderer faceRend;
    // Start is called before the first frame update
    void Start()
    {
        Swap();
    }

    //随机一个

    public void Swap()
    {
        //头饰品
        foreach (GameObject c in mHeads)
        {
            c.SetActive(false);
        }
        int index = Random.Range(0, mHeads.Length);
        mHeads[index].SetActive(true);

        //饰品
        foreach (GameObject c in mAccessories)
        {
            c.SetActive(false);
        }
        int index1 = Random.Range(0, mAccessories.Length);
        mAccessories[index1].SetActive(true);

        //眼部
        int index2 = Random.Range(0, mFaces.Length);
        faceRend.material = mFaces[index2];
    }
}
