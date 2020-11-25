using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimatorCtrl : MonoBehaviour
{

    public Animator animator;
    void Start()
    {
        
    }
    
    private void SetFloat(string parameter = "key,value")
    {
        char[] separator = { ',', ';' };
        string[] param = parameter.Split(separator);

        string name = param[0];
        float value = (float)Convert.ToDouble(param[1]);

        Debug.Log(name + " " + value);
            
        animator.SetFloat(name, value);
            
    }
    private void SetInt(string parameter = "key,value")
    {
        char[] separator = { ',', ';' };
        string[] param = parameter.Split(separator);

        string name = param[0];
        int value = Convert.ToInt32(param[1]);

        Debug.Log(name + " " + value);
            
        animator.SetInteger(name, value);
           
    }

    private void SetBool(string parameter = "key,value")
    {
        char[] separator = { ',', ';' };
        string[] param = parameter.Split(separator);

        string name = param[0];
        bool value = Convert.ToBoolean(param[1]);

        Debug.Log(name + " " + value);
            
        animator.SetBool(name, value);
            
    }

    private void SetTrigger(string parameter = "key,value")
    {
        char[] separator = { ',', ';' };
        string[] param = parameter.Split(separator);

        string name = param[0];

        Debug.Log(name);
            
        animator.SetTrigger(name);
            
    }
    //设置动作
    public void SetAnimator(Common.State state)
    {
        if (state == Common.State.IDLE)
            SetInt("animation,1");
        else if (state == Common.State.RUN)
            SetInt("animation,18");
        else if (state == Common.State.ATTACK)
            SetInt("animation,11");
        else if (state == Common.State.VICTORY)
            SetInt("animation,3");
        else if (state == Common.State.DAMAGE)
            SetInt("animation,5");
        else if (state == Common.State.DIE)
            SetInt("animation,7");
        else if (state == Common.State.WALK)
            SetInt("animation,21");
        else if(state == Common.State.DASH)
            SetInt("animation,8");
    }
}
