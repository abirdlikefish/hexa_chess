using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : BaseCity
{
    protected override void DestroyCheck()
    {
        if (CurrentHP <= 0)
        {
            GameManager.instance.JudgeShouldEndGame();
            UnitManager.Instance.RemoveUnit(this);
        }
    }
}
