using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class GridInfoWin : Window , IFguiWin
{
    private GTextField gridInfoTxt_name;
    private GTextField gridInfoTxt_atk;
    private GTextField gridInfoTxt_def;
    private GTextField gridInfoTxt_moveCost;
    protected override void OnInit()
    {
        base.OnInit();
        GComponent midCom = UIPackage.CreateObject("Hexa_chess", "GridInfoWin").asCom;
        this.contentPane = midCom;
        gridInfoTxt_name = midCom.GetChild("GridNameTxt").asTextField;
        gridInfoTxt_atk = midCom.GetChild("AtkOffsetTxt").asTextField;
        gridInfoTxt_def = midCom.GetChild("DefOffsetTxt").asTextField;
        gridInfoTxt_moveCost = midCom.GetChild("MoveCostTxt").asTextField;
        // Debug.LogWarning("GridInfoWin OnInit");
        // this.Center();
        // this.modal = true;
    }

    protected override void OnShown()
    {
        base.OnShown();
        Vector2 midPos = Input.mousePosition;
        midPos.y = Screen.height - midPos.y;
        midPos = GRoot.inst.GlobalToLocal(midPos);
        this.SetXY(midPos.x, midPos.y);
    }

    protected override void OnHide()
    {
        base.OnHide();
    }

    private void ShowGridInfo(string gridName ,int atkOffset, int defOffset, float moveCost)
    {
        // Debug.LogWarning("ShowGridInfo");
        Show();
        gridInfoTxt_name.text = gridName;
        gridInfoTxt_atk.text = atkOffset.ToString();
        gridInfoTxt_def.text = defOffset.ToString();
        gridInfoTxt_moveCost.text = moveCost.ToString();
    }

    public void ShowWin()
    {
        // Debug.LogWarning("ShowWin");
        MyEvent.ShowGridInfoWin += ShowGridInfo;
        MyEvent.HideGridInfoWin += Hide;
        // MyEvent.ShowGridInfoWin += (a,b,c) => { Debug.LogWarning("ShowGridInfoWin"); };
    }

    public void HideWin()
    {
        MyEvent.ShowGridInfoWin -= ShowGridInfo;
        MyEvent.HideGridInfoWin -= Hide;
    }
}
