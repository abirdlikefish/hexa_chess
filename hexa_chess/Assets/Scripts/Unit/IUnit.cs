using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit  
{

    MyEnum.UnitType UnitType{get;}
    string UnitName{get;}
    void Attack(IUnit other);//传入攻击对象的脚本接口

    void GetDamage(int damage);

    void RecoverHp(int hp);



    
    //单位归属查询
    // MyEnum.TheOperator GetOperator();
    //获取单位位置
    // Vector2Int GetUnitCoord();
    //获取单位行动力
    // float GetActionForce();
    Vector2Int Coord{get;}
    MyEnum.TheOperator TheOperator{get;}
    int MaxHp { get; }
    int Atk { get; }
    int CurrentHP{get;}
    float MoveForce{get;}

    // int GetUnitHp();

    //MyStruct.UnitConfig GetUnitConfig();

    bool HaveZOC{get;}
    int ViewRange{get;}
    int AttackRadius { get; }
    List<Vector2Int> virtualArea{get;set;}

    /// <summary>
    /// 管理器操作接口
    /// </summary>
    void RoundBeginCheck();
    MyEnum.UnitStates GetStates();

    
}
