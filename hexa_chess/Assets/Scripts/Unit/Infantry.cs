using System;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Unit
{
    private void Start()
    {
        InitInformation(Enum.UnitType.Infantry,5,3,2,1,5,1,false);
        GameManager.instance.AddUnitIntoPlayerUnits(this);
    }

    private void Update()
    {
        if (this.currentState.state == Enum.UnitStates.Action)
        {
            
        }
    }
}