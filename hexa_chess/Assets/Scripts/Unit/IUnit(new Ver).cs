using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit  
{
    void Attack(IUnit unit);
    void GetDamage(int danage);
    void Move();
}
