//------------------------------------------------------------
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
        private ProcedureMenu m_ProcedureMenu = null;
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
        // 点击确认按钮
        public void ConfirmButtonClick()
        {
            m_ProcedureMenu.StartGame();
        }

        //  点击重置按钮
        public void ResetButtonClick()
        {


        }
        //  选择角色
        public void OnRoleSelected(int roleIndex)
        {
            this.selectedRoleIndex = roleIndex;
            if (!roleToggle[roleIndex].isOn)
            {
                roleToggle[roleIndex].isOn = true;
            }
            GameData.instance.SetModel(roleIndex);

            Log.Debug("设置模型:" + roleIndex);

            InitDressGroupList();
        }

        /// <summary>
        /// 切换选择面板
        /// </summary>
        /// <param name="groupIndex"></param>
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

        }
        private void OnDressToggleOn(int selectDressIndex, Toggle toggle)
        {
            //  判断是否解锁，没解锁提示弹广告
            if (!GameData.instance.openSkins.Contains(selectDressIndex))
            {
                Debug.Log("没解锁 roleIndex:" + selectedRoleIndex + " selectDressIndex:" + selectDressIndex);
                return;
            }
            this.selectedDressIndex = selectDressIndex;
            toggle.isOn = true;

            Debug.Log("roleIndex:" + selectedRoleIndex + " selectDressIndex:" + selectDressIndex);

            GameData.instance.SetSkin(selectDressIndex);

            Log.Debug("设置装饰:" + selectDressIndex);

        }
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
                dressPrefix = "H";
            }
            else if (selectedRoleIndex == 1)
            {
                dressPrefix = "W";
            }
            else if (selectedRoleIndex == 2)
            {
                dressPrefix = "G";
            }
            else if (selectedRoleIndex == 3)
            {
                dressPrefix = "H";
            }
            else if (selectedRoleIndex == 4)
            {
                dressPrefix = "G";
            }
            else
            {
                throw new UnityException("不支持的角色ID");
            }
            List<Sprite> dressSpriteList = dressSprites.FindAll(e => e.name.StartsWith(dressPrefix));
            Toggle firstToggle = null;
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
                });
                image.GetComponent<Image>().sprite = dressItem;//设置图片
                toggle.GetComponent<Toggle>().group = selectGroups[1].transform.GetComponent<ToggleGroup>();//设置组
                toggle.GetComponent<Toggle>().isOn = false;
                if (dressSpriteList.IndexOf(dressItem) == 0)// 默认选择第一个皮肤
                {
                    firstToggle = toggle.GetComponent<Toggle>();
                }
                // 判断是否解锁
                if (GameData.instance.openSkins.Contains(dressSpriteList.IndexOf(dressItem)))
                {
                    lockImg.gameObject.SetActive(false);
                }

            }
            OnDressToggleOn(0, firstToggle);//设置勾选第一个
        }
        private void InitPropGroupList()
        {
            ClearGroupContent(2);
            Toggle firstToggle = null;
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
                });
                image.GetComponent<Image>().sprite = propSprite;//设置图片
                toggle.GetComponent<Toggle>().group = selectGroups[2].transform.GetComponent<ToggleGroup>();
                toggle.GetComponent<Toggle>().isOn = false;
                if (propSprites.IndexOf(propSprite) == 0)
                {
                    firstToggle = toggle.GetComponent<Toggle>();
                }
                // 判断是否解锁
                if (GameData.instance.openItems.Contains(propSprites.IndexOf(propSprite)))
                {
                    lockImg.gameObject.SetActive(false);
                }
            }
            OnPropToggleOn(0, firstToggle);//设置勾选第一个
        }
        private void OnPropToggleOn(int selectPropIndex, Toggle toggle)
        {
            //  判断是否解锁，没解锁提示弹广告
            if (!GameData.instance.openItems.Contains(selectPropIndex))
            {
                Debug.Log("没解锁 roleIndex:" + selectedRoleIndex + " selectPropIndex:" + selectPropIndex);
                return;
            }
            this.selectedPropIndex = selectPropIndex;
            toggle.isOn = true;

            Debug.Log("roleIndex:" + selectedRoleIndex + " selectPropIndex:" + selectPropIndex);
            GameData.instance.SetItem(selectPropIndex);
            Log.Debug("设置装饰:" + selectPropIndex);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }
            // 玩家数据赋值
            playerData = GameData.instance.GetPlayerSelf();
            // 设置上玩家名字
            this.playerName.text = playerData.nickName;
            // 默认选择第一个
            this.ChangeGroup(0);
            this.OnRoleSelected(0);
            // 初始化道具栏
            this.InitPropGroupList();
        }
        protected override void OnResume()
        {

        }
        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            m_ProcedureMenu = null;
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
