using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI控制模型变化
public class AnimUICtrl : MonoBehaviour
{
    public static AnimUICtrl instance;
    public List<GameObject> AnimList;
    public Camera mCamera;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        HideAll();
        
    }
    void HideAll()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        mCamera.gameObject.SetActive(true);
    }
    int curIndex = 0;
    public void ShowModel(int index)
    {
        HideAll();
        curIndex = index;
        AnimList[index].SetActive(true);
        AnimList[index].transform.localPosition = Vector3.zero;
        SetDress();
    }
    //皮肤 0-14
    public void SetSkin(int index)
    {
        var swap = AnimList[curIndex].GetComponent<UnitItemBaseCtrl>().Model.GetComponent<SwapMaterials>();
        swap.Swap(index);
    }
    //武器 0-9
    public void SetWeapon(int index)
    {
        var swap = AnimList[curIndex].GetComponent<UnitItemBaseCtrl>().Model.GetComponent<SwapWeapon>();
        swap.Swap(index);
    }

    public void SetDress()
    {
        var swap = AnimList[curIndex].GetComponent<UnitItemBaseCtrl>().Model.GetComponent<DressUp>();
        swap.Swap(); //随机装饰
    }
}
