using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitConfig", menuName = "ScriptableObject/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    public MyEnum.UnitType unitType;//单位类型
    public int MaxHp;//生命值
    public float Action; //行动点数
    public int Attak; //攻击力
    public int AttackRadius; //攻击范围

    public float movingSpeed;
    /// <summary>
    /// 生成所需的金币数目
    /// </summary>
    public int Coin;
    /// <summary>
    /// 占用的单位数
    /// </summary>
    public int Occupation;
    /// <summary>
    /// 能否生成控制区(ZOC)
    /// </summary>
    public bool HaveZOC;
}

