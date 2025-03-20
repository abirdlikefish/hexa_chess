using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class CreateArmyCom : IFguiCom
{
    private GComponent parent;
    private GComponent createArmyCom;
    private bool isShow = false;
    GList armyList;
    private GButton selectedArmyBtn;

    public IFguiCom Create(GComponent parent)
    {
        this.parent = parent;
        createArmyCom = parent.GetChild("CreateArmyCom").asCom;
        armyList = createArmyCom.GetChild("ArmyList").asList;
        armyList.itemRenderer = RenderListItem;
        armyList.numItems = MyEnum.ArmyType.GetValues(typeof(MyEnum.ArmyType)).Length - 1;
        InitEvent();
        return this;
    }

    public void Remove()
    {
        if(isShow) Hide();
    }

    private void InitEvent()
    {
        
    }

    public void Show()
    {
        isShow = true;
        MyEvent.OpenUnitUI += OpenUnitUI;
        MyEvent.GetSelectedArmy += GetSelectedArmy;
    }
    public void Hide()
    {
        if(!isShow) return;
        isShow = false;
        MyEvent.OpenUnitUI -= OpenUnitUI;
        MyEvent.GetSelectedArmy -= GetSelectedArmy;
    }

    void RenderListItem(int index, GObject obj)
    {
        GComponent item = obj.asCom;
        MyEnum.ArmyType armyType = (MyEnum.ArmyType)(index + 1);
        item.data = armyType;
        item.onClick.Add(() => SelectArmy(item));
        item.GetChild("up").asLoader.url = MyConst.ArmyUIIconPath[armyType];
        item.GetChild("down").asLoader.url = MyConst.ArmyUIIconPath[MyEnum.ArmyType.None];
        // item.GetChild("over").asLoader.url = MyConst.ArmyUIIconPath[MyEnum.ArmyType.None];;
        item.GetChild("over").asLoader.url = MyConst.ArmyUIIconPath[armyType];
        item.GetChild("selectedOver").asLoader.url = MyConst.ArmyUIIconPath[MyEnum.ArmyType.None];
        // item.GetChild("AtkTxt").text = "1";
        // item.GetChild("HpTxt").text = "1";
        // item.GetChild("MoveForceTxt").text = "1";
        // item.GetChild("AtkRangeTxt").text = "1";
    }
    void SelectArmy(GComponent item)
    {
        if(selectedArmyBtn != null) selectedArmyBtn.selected = false;
        selectedArmyBtn = item == null ? null : item.asButton;
        if(selectedArmyBtn != null) Debug.Log("SelectedArmy: " + ((MyEnum.ArmyType)selectedArmyBtn.data).ToString());
        if(selectedArmyBtn != null) MyEvent.SelectArmyToCreate?.Invoke((MyEnum.ArmyType)selectedArmyBtn.data);
    }
    void OpenUnitUI(IUnit unit)
    {
        if(unit != null && unit.UnitType == MyEnum.UnitType.Army)
        {
            SelectArmy(null);
        }
    }
    MyEnum.ArmyType GetSelectedArmy()
    {
        if(selectedArmyBtn == null) return MyEnum.ArmyType.None;
        return (MyEnum.ArmyType)selectedArmyBtn.data;
    }
    
}
