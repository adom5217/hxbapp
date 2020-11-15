using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMaterials : MonoBehaviour
{
    public Material[] materials;
    public Renderer rend;
    void Start()
    {
        //rend = GetComponent<Renderer>();
        //rend.enabled = true;
    }

   
    public void Swap(int index)
    {
        if(index<materials.Length)
            rend.sharedMaterial = materials[index];
    }
}
