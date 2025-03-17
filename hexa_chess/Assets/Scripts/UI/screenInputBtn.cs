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
        // Debug.LogWarning("ScreenInputBtn Create");
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
        screenInputBtn.onTouchMove.Add(DragScreen);
        screenInputBtn.onTouchBegin.Add(TouchBegin);
        // screenInputBtn.onTouchEnd.Add(OnTouchEnd);
    }

    private void TrySelectGrid(EventContext context)
    {
        InputEvent inputEvent = context.inputEvent;
        Vector2 screenPosition = new Vector2(inputEvent.x, Screen.height - inputEvent.y);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
        Vector2Int coord = MapManager.Pos_To_Coord(worldPosition);
        Debug.Log("TrySelectGrid" + coord);
        if(MapManager.Instance.IsInMap(coord) == false)
        {
            MyEvent.OnGridClick_left?.Invoke(null);
        }
        else
        {
            MyEvent.OnGridClick_left?.Invoke(coord);
        }
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
    private Vector2 lastScreenPos;
    private void DragScreen(EventContext context)
    {
        InputEvent inputEvent = context.inputEvent;
        Vector2 screenPosition = new Vector2(inputEvent.x, Screen.height - inputEvent.y);
        Vector3 lastDragPos = Camera.main.ScreenToWorldPoint(new Vector3(lastScreenPos.x, lastScreenPos.y, -Camera.main.transform.position.z));
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
        MyEvent.DragScreen?.Invoke(worldPosition - lastDragPos);
        lastScreenPos = screenPosition;
    }
    private void TouchBegin(EventContext context)
    {
        InputEvent inputEvent = context.inputEvent;
        Vector2 screenPosition = new Vector2(inputEvent.x, Screen.height - inputEvent.y);
        lastScreenPos = screenPosition;
    }
}
