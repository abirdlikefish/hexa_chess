using System;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Unit
{
    private void Start()
    {
        InitInformation(MyEnum.UnitType.Infantry,5,3,2,1,5,1,false);
    }

    private void Update()
    {
        if (this.currentState.state == MyEnum.UnitStates.Action)
        {
            
        }
    }
}