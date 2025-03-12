using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public interface IFguiCom
{
    public IFguiCom Create(GComponent parent);
    public void Remove();
}
