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
        screenInputBtn.onRightClick.Add(TrySelectGrid_right);
    }

    private void TrySelectGrid(EventContext context)
    {
        InputEvent inputEvent = context.inputEvent;
        Vector2 screenPosition = new Vector2(inputEvent.x, Screen.height - inputEvent.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
        Vector2Int coord = MapManager.Pos_To_Coord(worldPosition);
        MapManager.Instance.SearchMovableArea(Enum.TheOperator.Player , coord , 5);
    }
    private void TrySelectGrid_right(EventContext context)
    {
        InputEvent inputEvent = context.inputEvent;
        Vector2 screenPosition = new Vector2(inputEvent.x, Screen.height - inputEvent.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
        Vector2Int coord = MapManager.Pos_To_Coord(worldPosition);
        MapManager.Instance.ChangeVirtualField(Enum.TheOperator.Player , coord , true);
    }
}
