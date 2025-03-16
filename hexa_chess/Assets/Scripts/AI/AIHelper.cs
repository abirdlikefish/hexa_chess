using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHelper
{

    private static class AIConst
    {
        public static Dictionary<int, int> AttackEnemyValues = new Dictionary<int, int>
        {
            
        };

        public static Dictionary<int, int> AttacBuildingValues = new Dictionary<int, int>
        {

        };


    }


    private static AIHelper instance;
    public static AIHelper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AIHelper();
            }
            return instance;
        }
    }

    private AIHelper()
    {
    }

    public float GetMaxAttackDelta(Vector2Int beforePos, Vector2Int afterPos, int AttackRange)
    {
        float delta = 0;



        return delta;
    }

    public float GetHomeDelta()
    {
        float delta = 0;

        return delta;
    }

    public float SafetyDelta()
    {
        float delta = 0;

        return delta;
    }

    public float GetDamage()
    {
        float delta = 0;

        return delta;
    }

    public float GetHPPercentage()
    {
        float delta = 0;

        return delta;
    }
}
