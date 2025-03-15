using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class ScreenInputBtn : IFguiCom
{
    private GComponent parent;
    private GButton screenInputBtn;
    public IFguiCom Create(GComponent parent)
    {
        this.parent = parent;
        screenInputBtn =parent.GetChild("ScreenInputBtn").asButton;
        InitEvent();
        return this;
    }
    public void Remove()
    {
        screenInputBtn.onClick.Remove(TrySelectGrid);
        screenInputBtn.onRightClick.Remove(TrySelectGrid_right);
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
        if(MapManager.Instance.IsInMap(coord) == false)
        {
            MyEvent.OnGridClick_left?.Invoke(null);
        }
        else
        {
            MyEvent.OnGridClick_left?.Invoke(coord);
        }
        
        // MapManager.Instance.SearchMovableArea(Enum.TheOperator.Player , coord , 5);
    }
    private void TrySelectGrid_right(EventContext context)
    {
        InputEvent inputEvent = context.inputEvent;
        Vector2 screenPosition = new Vector2(inputEvent.x, Screen.height - inputEvent.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
        Vector2Int coord = MapManager.Pos_To_Coord(worldPosition);
        // MapManager.Instance.ChangeVirtualField(Enum.TheOperator.Player , coord , true);
        if(MapManager.Instance.IsInMap(coord) == false)
        {
            MyEvent.OnGridClick_right?.Invoke(null);
        }
        else
        {
            MyEvent.OnGridClick_right?.Invoke(coord);
        }
    }
}
