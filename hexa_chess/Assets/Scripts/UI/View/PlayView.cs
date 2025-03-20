using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class PlayView : IFguiView
{
    private bool isShow;
    GComponent playView ;
    // GButton screenInputBtn;
    GButton testBtn;

    GButton nextBtn;


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
    public void ShowView()
    {
        isShow = true;
        GRoot.inst.AddChild(playView);
        MyEvent.OpenUnitUI += OpenUnitUI;
        MyEvent.SetGlobalInfo += SetGlobalInfo;
        Stage.inst.onMouseWheel.Add(OnMouseWheel);
        foreach (var item in fguiOtherComs)
        {
            item.Show();
        }
    }
    public void HideView()
    {
        if(!isShow) return;
        isShow = false;
        GRoot.inst.RemoveChild(playView);
        MyEvent.OpenUnitUI -= OpenUnitUI;
        MyEvent.SetGlobalInfo -= SetGlobalInfo;
        Stage.inst.onMouseWheel.Remove(OnMouseWheel);
        foreach (var item in fguiOtherComs)
        {
            item.Hide();
        }
    }
    private void InitComponent()
    {
        testBtn = playView.GetChild("TestBtn").asButton;
        nextBtn = playView.GetChild("NextBtn").asButton;
        moneyTxt = playView.GetChild("MoneyTxt").asTextField;
        unitCntTxt = playView.GetChild("UnitCntTxt").asTextField;
        roundTxt = playView.GetChild("RoundTxt").asTextField;
        unitUIController = playView.GetController("UnitUIController");

        fguiOtherComs = new List<IFguiCom>
        {
            new ScreenInputBtn().Create(playView),
            new ArmyPanelCom().Create(playView),
            new CreateArmyCom().Create(playView),
        };

    }
    private void InitEvent()
    {
        testBtn.onClick.Add(() => MyEvent.OnClick_testBtn?.Invoke());
        nextBtn.onClick.Add(() => MyEvent.OnClick_nextBtn?.Invoke());
    }

    private void OpenUnitUI(IUnit unit )
    {
        // Debug.Log("OpenUnitUI");
        if(unit == null)
        {
            unitUIController.selectedPage = "Hide";
            return;
        }
        else if(unit.UnitType == MyEnum.UnitType.Army)
        {
            unitUIController.selectedPage = "ShowArmy";
        }
        else if(unit.UnitType == MyEnum.UnitType.City)
        {
            unitUIController.selectedPage = "ShowCity";
        }
        else
        {
            Debug.LogError("OpenUnitUI Error");
        }
    }
    private void SetGlobalInfo(Vector2 money , Vector2 unitCnt , Vector2 round)
    {
        Debug.Log("SetGlobalInfo");
        moneyTxt.text = money.x.ToString() + "/+" + money.y.ToString();
        unitCntTxt.text = unitCnt.x.ToString() + "/" + unitCnt.y.ToString();
        roundTxt.text = round.x.ToString() + "/" + round.y.ToString();
    }
    private void OnMouseWheel(EventContext context)
    {
        InputEvent inputEvent = (InputEvent)context.data;
        float delta = inputEvent.mouseWheelDelta;
        // Debug.Log("Mouse Wheel Delta: " + delta);
        MyEvent.CameraMove?.Invoke(new Vector3(0,0,delta));
    }

}
