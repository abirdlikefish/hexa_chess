using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class GridInfoWin : Window , IFguiWin
{
    private GTextField gridInfoTxt_atk;
    private GTextField gridInfoTxt_def;
    private GTextField gridInfoTxt_moveCost;
    protected override void OnInit()
    {
        base.OnInit();
        GComponent midCom = UIPackage.CreateObject("Hexa_chess", "GridInfoWin").asCom;
        this.contentPane = midCom;
        gridInfoTxt_atk = midCom.GetChild("AtkOffsetTxt").asTextField;
        gridInfoTxt_def = midCom.GetChild("DefOffsetTxt").asTextField;
        gridInfoTxt_moveCost = midCom.GetChild("MoveCostTxt").asTextField;
        // this.Center();
        // this.modal = true;
    }

    protected override void OnShown()
    {
        base.OnShown();
        Vector2 midPos = Input.mousePosition;
        midPos.y = Screen.height - midPos.y;
        this.SetXY(midPos.x, midPos.y);
    }

    protected override void OnHide()
    {
        base.OnHide();
    }

    private void ShowGridInfo(int atkOffset, int defOffset, float moveCost)
    {
        gridInfoTxt_atk.text = atkOffset.ToString();
        gridInfoTxt_def.text = defOffset.ToString();
        gridInfoTxt_moveCost.text = moveCost.ToString();
        Show();
    }
    // private void ShowGridInfo(MyEnum.TheOperator theOperator , Vector2Int coord)
    // {
    //     gridInfoTxt_atk.text = MapManager.Instance.GetAtkOffset(theOperator, coord).ToString();
    //     gridInfoTxt_def.text = MapManager.Instance.GetDefOffset(theOperator, coord).ToString();
    //     gridInfoTxt_moveCost.text = MapManager.Instance. GetMoveCost(theOperator, coord).ToString();
    //     Show();
    // }

    public void ShowWin()
    {
        MyEvent.ShowGridInfoWin += ShowGridInfo;
        MyEvent.HideGridInfoWin += Hide;
    }

    public void HideWin()
    {
        MyEvent.ShowGridInfoWin -= ShowGridInfo;
        MyEvent.HideGridInfoWin -= Hide;
    }
}
