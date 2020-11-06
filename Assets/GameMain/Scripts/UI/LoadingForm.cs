﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class LoadingForm : UGuiForm
    {
       
        [SerializeField]
        private Text m_MessageText = null;

        [SerializeField]
        private Slider m_Progress = null;

        private float mCurPro = 0;
        private float mMaxPro = 0; 
#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            LoadingParams loadingParams = (LoadingParams)userData;
            if (loadingParams == null)
            {
                Log.Warning("loadingParams is invalid.");
                return;
            }
            if(loadingParams.msg.Length>0)
            {
                m_MessageText.text = loadingParams.msg;
            }
            mCurPro = 0;
            mMaxPro = loadingParams.max;
            m_Progress.value = 0;
            //先制作一个假的进度条
            StartCoroutine(Updarepropress());
        }
       
        private IEnumerator Updarepropress()
        {
            yield return new WaitForSeconds(0.2f);
            //while(mCurPro<mMaxPro)
            //{
            //    mCurPro++;
            //    m_Progress.value = mCurPro/mMaxPro;
            //    yield return new WaitForSeconds(0.2f);
            //}
            Debug.Log("模拟加载完成");
            Close();
        }
        
    }

     public class LoadingParams
     {
        public string msg = "游戏加载中...";
        public int max = 5;

     }
}
