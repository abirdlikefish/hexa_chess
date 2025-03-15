using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit  
{
    /// <summary>
    /// 单位操作接口
    /// </summary>
    /// <param name="cost"></param>
    void Move(int cost);//参数待定

    void Attack(IUnit other);//传入攻击对象的脚本接口

    void Station();//驻扎

    void Rest();//休息

    void Dismiss();//解散

    void Skip();//跳过

    void GetDamage(int damage);


    /// <summary>
    /// 信息获取接口
    /// </summary>
    /// <returns></returns>

    //当前单位操作状态
    UnitStates GetStates();
    //单位归属查询
    bool isFriendUnit();
    //覆写单位位置
    void ReWritePosition(Vector2 vector2);
    //获取单位位置
    Vector2 GetUnitPos();
    //获取单位行动力
    int GetActionForce();
}
