using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class CityPanelCom : IFguiCom
{
    private GComponent parent;
    private GComponent cityPanelCom;
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
    // GLoader CityIconLoader;
    
    public IFguiCom Create(GComponent parent)
    {
        // Debug.LogWarning("ScreenInputBtn Create");
        this.parent = parent;
        cityPanelCom =parent.GetChild("CityPanelCom").asCom;

        // attackBtn = cityPanelCom.GetChild("AttackBtn").asButton;
        // restBtn = cityPanelCom.GetChild("RestBtn").asButton;
        // stationBtn = cityPanelCom.GetChild("StationBtn").asButton;
        // dismissBtn = cityPanelCom.GetChild("DismissBtn").asButton;
        // skipBtn = cityPanelCom.GetChild("SkipBtn").asButton;
        // atkTxt = cityPanelCom.GetChild("AtkTxt").asTextField;
        hpTxt = cityPanelCom.GetChild("HpTxt").asTextField;
        // moveForceTxt = cityPanelCom.GetChild("MoveForceTxt").asTextField;
        // atkRangeTxt = cityPanelCom.GetChild("AtkRangeTxt").asTextField;
        CityNameTxt = cityPanelCom.GetChild("CityNameTxt").asTextField;
        // CityIconLoader = cityPanelCom.GetChild("CityIcon").asLoader;

        DescribeTxt = cityPanelCom.GetChild("DescribeTxt").asTextField;

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
            // atkTxt.text = (unit as ICity).Atk.ToString();
            hpTxt.text = (unit as ICity).CurrentHP.ToString();
            // moveForceTxt.text = (unit as ICity).MoveForce.ToString();
            // atkRangeTxt.text = (unit as ICity).AttackRadius.ToString();
            CityNameTxt.text = (unit as ICity).UnitName;
            // CityNameTxt.text = "TestName";
            // CityIconLoader.url = MyConst.CityUIIconPath_small[(unit as ICity).CityType];
            DescribeTxt.text = "大本营被摧毁时，游戏失败。你的单位只能生成在大本营周围。";
        }
    }
}
