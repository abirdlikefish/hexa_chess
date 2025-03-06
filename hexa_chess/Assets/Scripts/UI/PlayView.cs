using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class PlayView : IFguiView
{
    GComponent playView ;
    GButton screenInputBtn;
    public bool Init()
    {
        playView = UIPackage.CreateObject("Hexa_chess", "PlayView").asCom;
        GRoot.inst.AddChild(playView);
        screenInputBtn =playView.GetChild("ScreenInputBtn").asButton;
        InitEvent();
        return true;
    }

    private void InitEvent()
    {
        screenInputBtn.onClick.Add(TrySelectGrid);
    }

    private void TrySelectGrid(EventContext context)
    {
        // 获取点击事件的输入事件
        InputEvent inputEvent = context.inputEvent;

        // 获取点击位置的屏幕坐标
        Vector2 screenPosition = new Vector2(inputEvent.x, Screen.height - inputEvent.y);

        // 将屏幕坐标转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));

        Debug.Log("Button clicked at world position: " + worldPosition);
        // MapManager.Pos_To_Coord(worldPosition);
        MapManager.Instance.SetGrid( MapManager.Pos_To_Coord(worldPosition) , Enum.GridType.Empty);
    }
}
