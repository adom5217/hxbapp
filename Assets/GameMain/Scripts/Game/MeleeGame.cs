//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using System.Threading.Tasks;
using UnityEngine;

namespace StarForce
{
    public class MeleeGame : GameBase
    {
        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode
        {
            get
            {
                return GameMode.Survival;
            }
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);

            m_ElapseSeconds += elapseSeconds;
            if (m_ElapseSeconds >= 3f)
            {
                m_ElapseSeconds = 0f;

                Debug.Log("每3秒刷一个怪？？");
                //IDataTable<DRAsteroid> dtAsteroid = GameEntry.DataTable.GetDataTable<DRAsteroid>();
                //float randomPositionX = SceneBackground.EnemySpawnBoundary.bounds.min.x + SceneBackground.EnemySpawnBoundary.bounds.size.x * (float)Utility.Random.GetRandomDouble();
                //float randomPositionZ = SceneBackground.EnemySpawnBoundary.bounds.min.z + SceneBackground.EnemySpawnBoundary.bounds.size.z * (float)Utility.Random.GetRandomDouble();
                //GameEntry.Entity.ShowAsteroid(new AsteroidData(GameEntry.Entity.GenerateSerialId(), 60000 + Utility.Random.GetRandom(dtAsteroid.Count))
                //{
                //    Position = new Vector3(randomPositionX, 0f, randomPositionZ),
                //});
            }
        }
         public override void Initialize()
         { 
             base.Initialize();
            
             GameEntry.UI.OpenUIForm(UIFormId.RoleForm, this);
             
        }
        
        public void StartGame()
        {
            SceneController.instance.InitStage();
            GameEntry.UI.OpenUIForm(UIFormId.JoystickForm);

            //是否需要引导
            GameEntry.UI.OpenUIForm(UIFormId.OprationGuideForm);

        }

    }
}
