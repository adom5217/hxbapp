﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class SurvivalGame : GameBase
    {
        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode
        {
            get
            {
                return GameMode.Survival;
            }
        }
         protected ScrollableBackground SceneBackground
        {
            get;
            private set;
        }
        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);

            m_ElapseSeconds += elapseSeconds;
            if (m_ElapseSeconds >= 1f)
            {
                m_ElapseSeconds = 0f;
                IDataTable<DRAsteroid> dtAsteroid = GameEntry.DataTable.GetDataTable<DRAsteroid>();
                float randomPositionX = SceneBackground.EnemySpawnBoundary.bounds.min.x + SceneBackground.EnemySpawnBoundary.bounds.size.x * (float)Utility.Random.GetRandomDouble();
                float randomPositionZ = SceneBackground.EnemySpawnBoundary.bounds.min.z + SceneBackground.EnemySpawnBoundary.bounds.size.z * (float)Utility.Random.GetRandomDouble();
                GameEntry.Entity.ShowAsteroid(new AsteroidData(GameEntry.Entity.GenerateSerialId(), 60000 + Utility.Random.GetRandom(dtAsteroid.Count))
                {
                    Position = new Vector3(randomPositionX, 0f, randomPositionZ),
                });
            }
        }

        public override void Initialize()
        { 
            base.Initialize();
             SceneBackground = Object.FindObjectOfType<ScrollableBackground>();
            if (SceneBackground == null)
            {
                Log.Warning("Can not find scene background.");
                return;
            }

            SceneBackground.VisibleBoundary.gameObject.GetOrAddComponent<HideByBoundary>();
            GameEntry.Entity.ShowMyAircraft(new MyAircraftData(GameEntry.Entity.GenerateSerialId(), 10000)
            {
                Name = "My Aircraft",
                Position = Vector3.zero,
            });
        }
    }
}
