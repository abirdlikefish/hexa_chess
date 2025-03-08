using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单位带有的属性
/// </summary>
public class UnitInformation
{
    /// <summary>
    /// 是哪种类型
    /// </summary>
    public Enum.UnitType unitType;
    /// <summary>
    /// 最大生命值
    /// </summary>
    public int MaxHp;
    /// <summary>
    /// 行动力
    /// </summary>
    public int Action; 
    /// <summary>
    /// 攻击力
    /// </summary>
    public int Attak; 
    /// <summary>
    /// 攻击范围
    /// </summary>
    public int AttackRadius; 
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