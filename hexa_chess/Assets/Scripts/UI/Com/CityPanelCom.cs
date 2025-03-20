using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class CityPanelCom : IFguiCom
{
    private GComponent parent;
    private GComponent armyPanelCom;
    private bool isShow = false;
    
    // GButton attackBtn;
    // GButton restBtn;
    // GButton stationBtn;
    // GButton dismissBtn;
    // GButton skipBtn;
    // GTextField atkTxt;
    GTextField hpTxt;
    // GTextField moveForceTxt;
    // GTextField atkRangeTxt;
    GTextField CityNameTxt;
    GTextField DescribeTxt;
    // GLoader ArmyIconLoader;
    
    public IFguiCom Create(GComponent parent)
    {
        // Debug.LogWarning("ScreenInputBtn Create");
        this.parent = parent;
        armyPanelCom =parent.GetChild("CityPanelCom").asCom;

        // attackBtn = armyPanelCom.GetChild("AttackBtn").asButton;
        // restBtn = armyPanelCom.GetChild("RestBtn").asButton;
        // stationBtn = armyPanelCom.GetChild("StationBtn").asButton;
        // dismissBtn = armyPanelCom.GetChild("DismissBtn").asButton;
        // skipBtn = armyPanelCom.GetChild("SkipBtn").asButton;
        // atkTxt = armyPanelCom.GetChild("AtkTxt").asTextField;
        hpTxt = armyPanelCom.GetChild("HpTxt").asTextField;
        // moveForceTxt = armyPanelCom.GetChild("MoveForceTxt").asTextField;
        // atkRangeTxt = armyPanelCom.GetChild("AtkRangeTxt").asTextField;
        CityNameTxt = armyPanelCom.GetChild("CityNameTxt").asTextField;
        // ArmyIconLoader = armyPanelCom.GetChild("ArmyIcon").asLoader;

        DescribeTxt = armyPanelCom.GetChild("DescribeTxt").asTextField;

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
        // attackBtn.onClick.Add(() => MyEvent.OnClick_attackBtn?.Invoke());
        // restBtn.onClick.Add(() => MyEvent.OnClick_restBtn?.Invoke());
        // stationBtn.onClick.Add(() => MyEvent.OnClick_stationBtn?.Invoke());
        // dismissBtn.onClick.Add(() => MyEvent.OnClick_dismissBtn?.Invoke());
        // skipBtn.onClick.Add(() => MyEvent.OnClick_skipBtn?.Invoke());
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
        if(unit != null && unit.UnitType == MyEnum.UnitType.City)
        {
            // atkTxt.text = (unit as IArmy).Atk.ToString();
            hpTxt.text = (unit as IArmy).CurrentHP.ToString();
            // moveForceTxt.text = (unit as IArmy).MoveForce.ToString();
            // atkRangeTxt.text = (unit as IArmy).AttackRadius.ToString();
            CityNameTxt.text = (unit as IArmy).UnitName;
            // ArmyNameTxt.text = "TestName";
            // ArmyIconLoader.url = MyConst.ArmyUIIconPath_small[(unit as IArmy).ArmyType];
            DescribeTxt.text = "大本营被摧毁时，游戏失败。你的单位只能生成在大本营周围。";
        }
    }
}
