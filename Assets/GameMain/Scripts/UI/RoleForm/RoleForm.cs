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

            GameData.instance.SetModel(roleIndex);

            Log.Debug("设置模型:"+roleIndex);
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
                }
                else
                {
                    selectButtons[i].SetLocalScaleX(1);
                    selectButtons[i].SetLocalScaleY(1);
                }
            }
            // 重新加载皮肤列表
            if (groupIndex == 1)
            {
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
                Transform selectToggle = null;
                var Content = selectGroups[1].transform.Find("Scroll View/Viewport/Content"); //不建议写这种很长，直接绑定在UI上
                foreach (Sprite dressItem in dressSpriteList)
                {
                    GameObject go = Instantiate(dressGroupItemPrefab, Content);
                    Transform image = go.transform.GetChild(0);//图片
                    Transform toggle = go.transform.GetChild(1);//勾选框
                    Transform lockImg = go.transform.GetChild(2);//锁
                    image.GetComponent<Image>().sprite = dressItem;//设置图片
                    toggle.GetComponent<Toggle>().group = selectGroups[1].transform.GetComponent<ToggleGroup>();
                    toggle.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => OnDressToggleOn(isOn, dressSpriteList.IndexOf(dressItem)));
                    if (selectToggle == null)
                    {
                        selectToggle = dressSpriteList.IndexOf(dressItem) == 0 ? toggle : null;
                    }
                    // TODO 判断这个皮肤是否解锁
                    // lockImg.gameObject.SetActive(false);
                }
                selectToggle.GetComponent<Toggle>().isOn = true;//设置勾选
            }
        }
        private void OnDressToggleOn(bool inOn, int selectDressIndex)
        {
            if (inOn)
            {
                this.selectedDressIndex = selectDressIndex;
                // TODO 判断是否解锁，没解锁提示弹广告
                Debug.Log("roleIndex:" + selectedRoleIndex + " selectDressIndex:" + selectDressIndex);

                GameData.instance.SetSkin(selectDressIndex);

                Log.Debug("设置装饰:" + selectDressIndex);
            }
        }
        private void ClearGroupContent(int groupIndex)
        {
            for (int i = 0; i < selectGroups[groupIndex].transform.Find("Scroll View").Find("Viewport").Find("Content").childCount; i++)
            {
                Destroy(selectGroups[groupIndex].transform.Find("Scroll View").Find("Viewport").Find("Content").GetChild(i).gameObject);
            }
        }
        private void InitPropGroupList()
        {
            ClearGroupContent(2);
            Transform selectToggle = null;
            foreach (Sprite propSprite in propSprites)
            {
                GameObject go = Instantiate(propGroupItemPrefab, selectGroups[2].transform.Find("Scroll View").Find("Viewport").Find("Content"));
                Transform image = go.transform.GetChild(0);//图片
                Transform toggle = go.transform.GetChild(1);//勾选框
                Transform lockImg = go.transform.GetChild(2);//锁
                image.GetComponent<Image>().sprite = propSprite;//设置图片
                toggle.GetComponent<Toggle>().group = selectGroups[1].transform.GetComponent<ToggleGroup>();
                toggle.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) => OnPropToggleOn(isOn, propSprites.IndexOf(propSprite)));
                if (selectToggle == null)
                {
                    selectToggle = propSprites.IndexOf(propSprite) == 0 ? toggle : null;
                }
                // TODO 判断这个道具是否解锁
                // lockImg.gameObject.SetActive(false);
            }
            selectToggle.GetComponent<Toggle>().isOn = true;//设置勾选
        }
        private void OnPropToggleOn(bool inOn, int selectPropIndex)
        {
            if (inOn)
            {
                this.selectedPropIndex = selectPropIndex;
                // TODO 判断是否解锁，没解锁提示弹广告
                Debug.Log("roleIndex:" + selectedRoleIndex + " selectPropIndex:" + selectPropIndex);

                GameData.instance.SetItem(selectPropIndex);

                Log.Debug("设置装饰:" + selectPropIndex);

            }
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
            // 设置道具栏
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
}
