using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class MapEditView : IFguiView
{
    GComponent mapEditView;
    GList gridList;
    Vector2Int? selectedCoord;
    List<IFguiCom> fguiComs;
    public IFguiView Init()
    {
        mapEditView = UIPackage.CreateObject("Hexa_chess", "MapEditView").asCom;
        // GRoot.inst.AddChild(mapEditView);
        gridList = mapEditView.GetChild("GridList").asList;
        gridList.itemRenderer = RenderListItem;
        gridList.numItems = MyEnum.GridType.GetValues(typeof(MyEnum.GridType)).Length;
        fguiComs = new List<IFguiCom>
        {
            new ScreenInputBtn().Create(mapEditView)
        };
        InitEvent();
        return this;
    }
    public void Show()
    {
        MyEvent.OnGridClick_left += SetSelectedCoord;
        GRoot.inst.AddChild(mapEditView);
    }
    public void Hide()
    {
        MyEvent.OnGridClick_left -= SetSelectedCoord;
        GRoot.inst.RemoveChild(mapEditView);
    }
    private void InitEvent()
    {
    }
    private void SetSelectedCoord(Vector2Int? coord)
    {
        if(selectedCoord != null)
        {
            MapManager.Instance_edit.HighLightGrid(selectedCoord.Value, false);
        }
        selectedCoord = coord;
        if (selectedCoord != null)
        {
            MapManager.Instance_edit.HighLightGrid(selectedCoord.Value, true);
        }
    }

    void RenderListItem(int index, GObject obj)
    {
        GButton item = obj.asButton;
        MyEnum.GridType gridType = (MyEnum.GridType)index;
        item.onClick.Add(() => SelectGrid(gridType));
        item.icon = MyConst.GridPicturePath[gridType];
    }
    void SelectGrid(MyEnum.GridType gridType)
    {
        Debug.Log("click grid " + gridType);
        if (selectedCoord == null) return ;
        MapManager.Instance_edit.ChangeGrid_edit(selectedCoord.Value, gridType);
        
    }
}
