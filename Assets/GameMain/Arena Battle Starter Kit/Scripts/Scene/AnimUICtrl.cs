using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI控制模型变化
public class AnimUICtrl : MonoBehaviour
{
    
    public List<GameObject> AnimList;
    public Camera mCamera;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        mCamera.gameObject.SetActive(true);
    }

    public void ShowAnim(int index)
    {
        AnimList[index].SetActive(true);
    }
}
