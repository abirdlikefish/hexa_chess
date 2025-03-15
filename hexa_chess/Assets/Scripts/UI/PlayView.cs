using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class PlayView : IFguiView
{
    GComponent playView ;
    // GButton screenInputBtn;
    GButton testBtn;

    GButton nextBtn;

    GButton attackBtn;
    GButton restBtn;
    GButton stationBtn;
    GButton dismissBtn;
    GButton skipBtn;
    GTextField atkTxt;
    GTextField hpTxt;
    GTextField moveForceTxt;

    GTextField moneyTxt;
    GTextField unitCntTxt;
    GTextField roundTxt;

    Controller unitUIController;

    List<IFguiCom> fguiOtherComs;
    public IFguiView Init()
    {
        playView = UIPackage.CreateObject("Hexa_chess", "PlayView").asCom;
        InitComponent();
        InitEvent();
        return this;
    }
    public void Show()
    {
        GRoot.inst.AddChild(playView);
        MyEvent.OpenUnitUI += OpenUnitUI;
        MyEvent.SetGlobalInfo += SetGlobalInfo;
    }
    public void Hide()
    {
        GRoot.inst.RemoveChild(playView);
    }
    private void InitComponent()
    {
        testBtn = playView.GetChild("TestBtn").asButton;
        nextBtn = playView.GetChild("NextBtn").asButton;
        attackBtn = playView.GetChild("AttackBtn").asButton;
        restBtn = playView.GetChild("RestBtn").asButton;
        stationBtn = playView.GetChild("StationBtn").asButton;
        dismissBtn = playView.GetChild("DismissBtn").asButton;
        skipBtn = playView.GetChild("SkipBtn").asButton;
        atkTxt = playView.GetChild("AtkTxt").asTextField;
        hpTxt = playView.GetChild("HpTxt").asTextField;
        moveForceTxt = playView.GetChild("MoveForceTxt").asTextField;
        moneyTxt = playView.GetChild("MoneyTxt").asTextField;
        unitCntTxt = playView.GetChild("UnitCntTxt").asTextField;
        roundTxt = playView.GetChild("RoundTxt").asTextField;
        unitUIController = playView.GetController("UnitUIController");

        fguiOtherComs = new List<IFguiCom>
        {
            new ScreenInputBtn().Create(playView)
        };

    }
    private void InitEvent()
    {
        testBtn.onClick.Add(() => MyEvent.OnClick_testBtn?.Invoke());
        nextBtn.onClick.Add(() => MyEvent.OnClick_nextBtn?.Invoke());
        attackBtn.onClick.Add(() => MyEvent.OnClick_attackBtn?.Invoke());
        restBtn.onClick.Add(() => MyEvent.OnClick_restBtn?.Invoke());
        stationBtn.onClick.Add(() => MyEvent.OnClick_stationBtn?.Invoke());
        dismissBtn.onClick.Add(() => MyEvent.OnClick_dismissBtn?.Invoke());
        skipBtn.onClick.Add(() => MyEvent.OnClick_skipBtn?.Invoke());
    }

    private void OpenUnitUI(IUnit unit)
    {
        Debug.Log("OpenUnitUI");
        if(unit == null)
        {
            unitUIController.selectedPage = "Hide";
            return;
        }
        unitUIController.selectedPage = "Show";
        atkTxt.text = "1";
        hpTxt.text = "10";
        moveForceTxt.text = "3";
    }
    private void SetGlobalInfo()
    {
        Debug.Log("SetGlobalInfo");
        moneyTxt.text = "1000";
        unitCntTxt.text = "10";
        roundTxt.text = "1";
    }

}
