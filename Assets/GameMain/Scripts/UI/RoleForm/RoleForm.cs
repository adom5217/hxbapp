﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 角色选择界面
    /// </summary>
    public class RoleForm : UGuiForm
    {
        private MeleeGame m_MeleeGame = null;
        // 3个面板的游戏物体
        public List<GameObject> selectGroups;
        // 皮肤的content
        public Transform dressContent;
        // 道具的content
        public Transform propContent;
        // 角色选择的toggle
        public List<Toggle> roleToggle;
        // 3个按钮
        public List<Transform> selectButtons;
        // 左按钮
        public Transform leftButton;
        // 右按钮
        public Transform rightButton;
        public Transform dressScrollBar;
        public Transform propScrollBar;
        // 玩家名字
        public Text playerName;
        // 角色皮肤
        public List<Sprite> dressSprites;
        // 道具
        public List<Sprite> propSprites;
        // 装扮栏预制体
        public GameObject dressGroupItemPrefab;
        // 道具栏预制体
        public GameObject propGroupItemPrefab;

        //------------------------------

        // 玩家数据列表
        private PlayerData playerData;
        // 选择的角色index
        private int selectedRoleIndex;
        // 选择的皮肤
        private int selectedDressIndex;
        // 选择的道具
        private int selectedPropIndex;

        private Coroutine moveCoroutine;
        // 点击确认按钮
        public void ConfirmButtonClick()
        {
            m_MeleeGame.StartGame();

        }

        //  点击重置按钮
        public void ResetButtonClick()
        {
            this.selectedRoleIndex = 0;
            this.selectedDressIndex = 0;
            this.selectedPropIndex = 0;
            // 默认选择第一个
            this.ChangeGroup(0);
            // 初始化角色栏
            this.InitRoleGroupList();
            // 初始化道具栏
            this.InitPropGroupList();
        }

        public void OnLeftButtonClick()
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
            if (dressContent.gameObject.activeInHierarchy)
            {
                moveCoroutine = StartCoroutine(MoveScrollBar(0, dressScrollBar.GetComponent<Scrollbar>()));
            }
            else if (propContent.gameObject.activeInHierarchy)
            {
                moveCoroutine = StartCoroutine(MoveScrollBar(0, propScrollBar.GetComponent<Scrollbar>()));
            }
        }
        public void OnRightButtonClick()
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
            if (dressContent.gameObject.activeInHierarchy)
            {
                moveCoroutine = StartCoroutine(MoveScrollBar(1, dressScrollBar.GetComponent<Scrollbar>()));
            }
            else if (propContent.gameObject.activeInHierarchy)
            {
                moveCoroutine = StartCoroutine(MoveScrollBar(1, propScrollBar.GetComponent<Scrollbar>()));
            }
        }

        IEnumerator MoveScrollBar(float targetValue, Scrollbar scrollbar)
        {
            float i = 0;
            while (Mathf.Abs(scrollbar.value - targetValue) >= 0.01)
            {
                i += 1f / 30f;
                scrollbar.value = Mathf.Lerp(scrollbar.value, targetValue, i);
                yield return new WaitForFixedUpdate();
            }
            scrollbar.value = targetValue;
        }

        // 切换选择面板
        public void ChangeGroup(int groupIndex)
        {
            if (selectGroups == null || selectGroups.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < selectGroups.Count; i++)
            {
                if (i == groupIndex)
                {
                    selectGroups[i].SetActive(true);
                }
                else
                {
                    selectGroups[i].SetActive(false);
                }
            }
            // 按钮放大缩小
            for (int i = 0; i < selectButtons.Count; i++)
            {
                if (i == groupIndex)
                {
                    selectButtons[i].SetLocalScaleX(1.2f);
                    selectButtons[i].SetLocalScaleY(1.2f);
                    selectButtons[i].GetChild(0).gameObject.SetActive(false);// 隐藏白色字
                    selectButtons[i].GetChild(1).gameObject.SetActive(true);//  显示金色字
                }
                else
                {
                    selectButtons[i].SetLocalScaleX(1);
                    selectButtons[i].SetLocalScaleY(1);
                    selectButtons[i].GetChild(0).gameObject.SetActive(true);
                    selectButtons[i].GetChild(1).gameObject.SetActive(false);
                }
            }
            if (groupIndex == 0)
            {
                rightButton.gameObject.SetActive(false);
                leftButton.gameObject.SetActive(false);
            }
            else
            {
                rightButton.gameObject.SetActive(true);
                leftButton.gameObject.SetActive(true);
            }

        }
        // 清除Scroll View的content
        private void ClearGroupContent(int groupIndex)
        {
            var content = selectGroups[groupIndex].transform.Find("Scroll View/Viewport/Content");
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
        }

        private void InitDressGroupList()
        {
            // 重新加载皮肤列表
            // 清除
            ClearGroupContent(1);
            string dressPrefix = "";
            if (selectedRoleIndex == 0)
            {
                dressPrefix = "M";
            }
            else if (selectedRoleIndex == 1)
            {
                dressPrefix = "W";
            }
            else if (selectedRoleIndex == 2)
            {
                dressPrefix = "X";
            }
            else if (selectedRoleIndex == 3)
            {
                dressPrefix = "H";
            }
            else if (selectedRoleIndex == 4)
            {
                dressPrefix = "G";
            }
            else if (selectedRoleIndex == 5)
            {
                dressPrefix = "T";
            }
            else
            {
                throw new UnityException("不支持的角色ID");
            }
            List<Sprite> dressSpriteList = dressSprites.FindAll(e => e.name.StartsWith(dressPrefix));
            Toggle selectedToggle = null;
            foreach (Sprite dressItem in dressSpriteList)
            {
                if (dressSpriteList.IndexOf(dressItem) >= GameData.MaxSkin)
                {
                    break;
                }
                GameObject go = Instantiate(dressGroupItemPrefab, dressContent);

                Transform image = go.transform.GetChild(0);//图片
                Transform toggle = go.transform.GetChild(1);//勾选框
                Transform lockImg = go.transform.GetChild(2);//锁
                // 设置按钮事件
                CommonButton commonButton = go.GetComponent<CommonButton>();
                commonButton.m_OnClick = new UnityEvent();
                commonButton.m_OnClick.AddListener(() =>
                {
                    OnDressToggleOn(dressSpriteList.IndexOf(dressItem), toggle.GetComponent<Toggle>());
                    PlayUISound(10001);
                });
                image.GetComponent<Image>().sprite = dressItem;//设置图片
                toggle.GetComponent<Toggle>().group = selectGroups[1].transform.GetComponent<ToggleGroup>();//设置组
                toggle.GetComponent<Toggle>().isOn = false;
                toggle.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) =>
                {
                    if (isOn)
                    {
                        commonButton.m_OnClick.Invoke();
                    }
                });
                if (dressSpriteList.IndexOf(dressItem) == this.selectedDressIndex)// 默认选择皮肤
                {
                    selectedToggle = toggle.GetComponent<Toggle>();
                }
                // 判断是否解锁
                if (GameData.instance.openSkins.Contains(dressSpriteList.IndexOf(dressItem)))
                {
                    lockImg.gameObject.SetActive(false);
                }

            }
            OnDressToggleOn(this.selectedDressIndex, selectedToggle);// 设置勾选装扮
        }
        // 选择皮肤
        private void OnDressToggleOn(int selectDressIndex, Toggle toggle)
        {
            //  判断是否解锁，没解锁提示弹广告
            if (!GameData.instance.openSkins.Contains(selectDressIndex))
            {
                Debug.Log("没解锁 roleIndex:" + selectedRoleIndex + " selectDressIndex:" + selectDressIndex);
                return;
            }
            this.selectedDressIndex = selectDressIndex;
            if (!toggle.isOn)
            {
                toggle.isOn = true;
            }
            Log.Debug("设置装饰:" + selectDressIndex);
            GameData.instance.SetSkin(selectDressIndex);
            AnimUICtrl.instance.SetSkin(selectDressIndex);
        }
        private void InitPropGroupList()
        {
            ClearGroupContent(2);
            Toggle selectedToggle = null;
            foreach (Sprite propSprite in propSprites)
            {
                if (propSprites.IndexOf(propSprite) >= GameData.MaxItem)
                {
                    break;
                }
                GameObject go = Instantiate(propGroupItemPrefab, propContent);
                Transform image = go.transform.GetChild(0);//图片
                Transform toggle = go.transform.GetChild(1);//勾选框
                Transform lockImg = go.transform.GetChild(2);//锁
                                                             // 设置按钮事件
                CommonButton commonButton = go.GetComponent<CommonButton>();
                commonButton.m_OnClick = new UnityEvent();
                commonButton.m_OnClick.AddListener(() =>
                {
                    OnPropToggleOn(propSprites.IndexOf(propSprite), toggle.GetComponent<Toggle>());
                    PlayUISound(10001);
                });
                image.GetComponent<Image>().sprite = propSprite;//设置图片
                toggle.GetComponent<Toggle>().group = selectGroups[2].transform.GetComponent<ToggleGroup>();
                toggle.GetComponent<Toggle>().isOn = false;
                toggle.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) =>
                {
                    if (isOn)
                    {
                        commonButton.m_OnClick.Invoke();
                    }
                });
                if (propSprites.IndexOf(propSprite) == this.selectedPropIndex)
                {
                    selectedToggle = toggle.GetComponent<Toggle>();
                }
                // 判断是否解锁
                if (GameData.instance.openItems.Contains(propSprites.IndexOf(propSprite)))
                {
                    lockImg.gameObject.SetActive(false);
                }
            }
            OnPropToggleOn(this.selectedPropIndex, selectedToggle);//设置勾选
        }
        // 选择道具
        private void OnPropToggleOn(int selectPropIndex, Toggle toggle)
        {
            //  判断是否解锁，没解锁提示弹广告
            if (!GameData.instance.openItems.Contains(selectPropIndex))
            {
                Debug.Log("没解锁 roleIndex:" + selectedRoleIndex + " selectPropIndex:" + selectPropIndex);
                return;
            }
            this.selectedPropIndex = selectPropIndex;
            if (!toggle.isOn)
            {
                toggle.isOn = true;
            }
            Log.Debug("设置武器:" + selectPropIndex);
            GameData.instance.SetItem(selectPropIndex);
            AnimUICtrl.instance.SetWeapon(selectPropIndex);
        }
        private void InitRoleGroupList()
        {
            for (int i = 0; i < selectGroups[0].transform.childCount; i++)
            {
                if (GameData.instance.openModels.Contains(i))
                {
                    selectGroups[0].transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                }
            }
            this.OnRoleSelected(0);
        }
        //  选择角色
        public void OnRoleSelected(int roleIndex)
        {
            PlayUISound(10001);
            if (!GameData.instance.openModels.Contains(roleIndex))
            {
                Debug.Log("没解锁 roleIndex:" + roleIndex);
                return;
            }
            this.selectedRoleIndex = roleIndex;
            if (!roleToggle[roleIndex].isOn)
            {
                roleToggle[roleIndex].isOn = true;
            }
            Log.Debug("设置模型:" + roleIndex);
            GameData.instance.SetModel(roleIndex);
            AnimUICtrl.instance.ShowModel(roleIndex);

            InitDressGroupList();

            Log.Debug("设置道具:" + roleIndex);
            GameData.instance.SetItem(this.selectedPropIndex);
            AnimUICtrl.instance.SetWeapon(this.selectedPropIndex);
        }

        public void OnRoleToggleChanged(bool isOn)
        {
            if (isOn)
            {
                for (int i = 0; i < roleToggle.Count; i++)
                {
                    if (roleToggle[i].isOn)
                    {
                        this.OnRoleSelected(i);
                    }
                }
            }
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_MeleeGame = (MeleeGame)userData;
            if (m_MeleeGame == null)
            {
                Log.Warning("m_MeleeGame is invalid when open RoleForm.");
                return;
            }
            // 玩家数据赋值
            playerData = GameData.instance.GetPlayerSelf();
            // 设置上玩家名字
            this.playerName.text = playerData.nickName;
            // 默认选择第一个
            this.ChangeGroup(0);
            // 初始化角色栏
            this.InitRoleGroupList();
            // 初始化道具栏
            this.InitPropGroupList();
        }
        protected override void OnResume()
        {

        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            m_MeleeGame = null;
        }

        // 分页
        private List<T> GetDataWithPage<T>(List<T> datalist, int page, int length)
        {
            List<T> data = new List<T>();
            if (length == 0) return data;
            for (int i = page * length; i < (page + 1) * length; i++)
            {
                if (i >= datalist.Count) return data;
                data.Add(datalist[i]);
            }
            return data;
        }
    }

    [System.Serializable]
    public class RoleFromEvent : UnityEvent<int>
    {
    }

}
