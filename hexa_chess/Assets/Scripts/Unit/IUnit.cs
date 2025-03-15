using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit  
{
    void Move(int cost);//参数待定

    void Attack(IUnit other);//传入攻击对象的脚本接口

    void Station();//驻扎

    void Rest();//休息

    void Dismiss();//解散

    void Skip();//跳过

    void GetDamage(int damage);

    UnitStates GetStates();

    bool isFriendUnit();

    void ReWritePosition(Vector2 vector2);

    Vector2 GetUnitPos();
}
