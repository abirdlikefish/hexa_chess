using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class ArmyPanelCom : IFguiCom
{
    private GComponent parent;
    private GComponent armyPanelCom;
    private bool isShow = false;
    
    GButton attackBtn;
    GButton restBtn;
    GButton stationBtn;
    GButton dismissBtn;
    GButton skipBtn;
    GTextField atkTxt;
    GTextField hpTxt;
    GTextField moveForceTxt;
    
    public IFguiCom Create(GComponent parent)
    {
        // Debug.LogWarning("ScreenInputBtn Create");
        this.parent = parent;
        armyPanelCom =parent.GetChild("ArmyPanelCom").asCom;

        attackBtn = armyPanelCom.GetChild("AttackBtn").asButton;
        restBtn = armyPanelCom.GetChild("RestBtn").asButton;
        stationBtn = armyPanelCom.GetChild("StationBtn").asButton;
        dismissBtn = armyPanelCom.GetChild("DismissBtn").asButton;
        skipBtn = armyPanelCom.GetChild("SkipBtn").asButton;
        atkTxt = armyPanelCom.GetChild("AtkTxt").asTextField;
        hpTxt = armyPanelCom.GetChild("HpTxt").asTextField;
        moveForceTxt = armyPanelCom.GetChild("MoveForceTxt").asTextField;

        InitEvent();
        return this;
    }
    public void Remove()
    {
        if(isShow)
        {
            Hide();
        }
    }

    private void InitEvent()
    {
        attackBtn.onClick.Add(() => MyEvent.OnClick_attackBtn?.Invoke());
        restBtn.onClick.Add(() => MyEvent.OnClick_restBtn?.Invoke());
        stationBtn.onClick.Add(() => MyEvent.OnClick_stationBtn?.Invoke());
        dismissBtn.onClick.Add(() => MyEvent.OnClick_dismissBtn?.Invoke());
        skipBtn.onClick.Add(() => MyEvent.OnClick_skipBtn?.Invoke());
    }
    public void Show()
    {
        isShow = true;
        MyEvent.OpenUnitUI += OpenUnitUI;
    }

    public void Hide()
    {
        if(!isShow) return;
        isShow = false;
        MyEvent.OpenUnitUI -= OpenUnitUI;
    }

    private void OpenUnitUI(IUnit unit)
    {        
        if(unit != null && unit.UnitType == MyEnum.UnitType.Army)
        {
            atkTxt.text = (unit as IArmy).Atk.ToString();
            hpTxt.text = (unit as IArmy).CurrentHP.ToString();
            moveForceTxt.text = (unit as IArmy).MoveForce.ToString();
        }
    }
}
