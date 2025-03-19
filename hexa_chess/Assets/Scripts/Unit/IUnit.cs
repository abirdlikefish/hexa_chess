using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit  
{
    /// <summary>
    /// 单位玩家输入操作接口
    /// </summary>
    MyEnum.UnitType UnitType{get;}
    void Attack(IUnit other);//传入攻击对象的脚本接口

    void GetDamage(int damage);

    void RecoverHp(int hp);

    void Station();

    void Rest();

    void Dismiss();

    void Skip();

    /// <summary>
    /// 信息获取接口
    /// </summary>
    /// <returns></returns>

    
    //单位归属查询
    // MyEnum.TheOperator GetOperator();
    //获取单位位置
    // Vector2Int GetUnitCoord();
    //获取单位行动力
    // float GetActionForce();
    Vector2Int Coord{get;}
    MyEnum.TheOperator TheOperator{get;}
    int CurrentHP{get;}
    float MoveForce{get;}

    // int GetUnitHp();

    // MyStruct.UnitConfig GetUnitConfig();

    bool HaveZOC{get;}
    int ViewRange{get;}
    List<Vector2Int> virtualArea{get;set;}

    /// <summary>
    /// 管理器操作接口
    /// </summary>
    void RoundBeginCheck();
    MyEnum.UnitStates GetStates();
    MyEnum.OperationBuff GetOperationBuff();
    // MyEnum.UnitType GetUnitType();

    
}
