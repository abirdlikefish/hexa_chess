using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    private static GameObject parentGo;
    private static Sprite fogSprite;
    private static Sprite uiSprite;
    private GameObject unit;
    private Dictionary<MyEnum.GridSpriteLayer,SpriteRenderer> spriteLayer;
    private MyEnum.GridType gridType;
    private Vector2Int gridPosition;
    private BaseGrid baseGrid;
    private Dictionary<MyEnum.TheOperator, GridState> gridState;
    public MyEnum.GridUIState currentUIState;
    public class GridState
    {
        private GridState() { }
        private MyEnum.TheOperator theOperator;
        private GridInfo gridInfo;
        public GridState(GridInfo gridInfo)
        {
            this.gridInfo = gridInfo;
            currentState = MyEnum.GridState.UnInit;
            atkOffset = 0;
            defOffset = 0;
            moveOffset = 0;
            controlledCnt = 0;
            watchedCnt = 0;
        }
        public MyEnum.GridState currentState;
        public float atkOffset;
        public float defOffset;
        public float moveOffset;
        public int controlledCnt;
        private int watchedCnt;
        public int WatchedCnt
        {
            get => watchedCnt;
            set
            {
                watchedCnt = value;
                if(watchedCnt > 0)
                {
                    ChangeState(MyEnum.GridState.Show);
                }
                else
                {
                    ChangeState(MyEnum.GridState.Hide);
                }
            }
        }
        public void ChangeState(MyEnum.GridState state)
        {
            if(currentState == state) return;
            currentState = state;
            if(theOperator == MyEnum.TheOperator.Player)
            {
                gridInfo.ChangeSpriteLayer(state);
            }
        }
        public GridState Init(MyEnum.TheOperator theOperator)
        {
            this.theOperator = theOperator;
            return this;
        }
    }
    private void ChangeSpriteLayer(MyEnum.GridState state)
    {
        spriteLayer[MyEnum.GridSpriteLayer.Base].enabled = false;
        spriteLayer[MyEnum.GridSpriteLayer.Fog].enabled = false;
        spriteLayer[MyEnum.GridSpriteLayer.UI].enabled = false;
        switch (state)
        {
            case MyEnum.GridState.Fog:
                spriteLayer[MyEnum.GridSpriteLayer.Fog].enabled = true;
                break;
            case MyEnum.GridState.Show:
                spriteLayer[MyEnum.GridSpriteLayer.Base].enabled = true;
                break;
            case MyEnum.GridState.Hide:
                spriteLayer[MyEnum.GridSpriteLayer.UI].enabled = true;
                break;
        }
    }
    public static void Init()
    {
        parentGo = new GameObject("Grids");
        fogSprite = Resources.Load<Sprite>("Sprite/Map/Fog");
        uiSprite = Resources.Load<Sprite>("Sprite/Map/UI");
    }
    public static GridInfo CreateGrid(BaseGrid baseGrid, Vector2Int gridPosition)
    {
        if(parentGo == null) parentGo = new GameObject("Grids");
        GameObject go = new GameObject("Grid_" + gridPosition.ToString());
        go.transform.SetParent(parentGo.transform);
        go.transform.position = MapManager.Coord_To_Pos(gridPosition);
        GridInfo gridInfo = go.AddComponent<GridInfo>();
        
        gridInfo.baseGrid = baseGrid;
        gridInfo.gridPosition = gridPosition;
        gridInfo.gridType = baseGrid.gridType;

        gridInfo.spriteLayer = new Dictionary<MyEnum.GridSpriteLayer, SpriteRenderer>();
        foreach (MyEnum.GridSpriteLayer layer in MyEnum.GridSpriteLayer.GetValues(typeof(MyEnum.GridSpriteLayer)))
        {
            GameObject midGo = new GameObject(layer.ToString());
            midGo.transform.SetParent(go.transform);
            midGo.transform.localPosition = Vector3.zero;
            gridInfo.spriteLayer[layer] = midGo.AddComponent<SpriteRenderer>();
            gridInfo.spriteLayer[layer].enabled = false;
            gridInfo.spriteLayer[layer].sortingLayerName = MyConst.GridSpriteSortingLayer[layer];
        }
        gridInfo.spriteLayer[MyEnum.GridSpriteLayer.Base].sprite = baseGrid.GridPicture;
        gridInfo.spriteLayer[MyEnum.GridSpriteLayer.Fog].sprite = fogSprite;
        gridInfo.spriteLayer[MyEnum.GridSpriteLayer.UI].sprite = uiSprite;
        // gridInfo.spriteLayer[Enum.GridSpriteLayer.UI].enabled = true;
        gridInfo.currentUIState = MyEnum.GridUIState.Hide;
        gridInfo.ChangeUIState(MyEnum.GridUIState.Hide);


        gridInfo.gridState = new Dictionary<MyEnum.TheOperator, GridState>();
        foreach (MyEnum.TheOperator theOperator in MyEnum.TheOperator.GetValues(typeof(MyEnum.TheOperator)))
        {
            gridInfo.gridState[theOperator] = new GridState(gridInfo).Init(theOperator);
            gridInfo.gridState[theOperator].ChangeState(MyEnum.GridState.Fog);
        }
        return gridInfo;
    }
    
    public void ChangeState(MyEnum.TheOperator theOperator, MyEnum.GridState state)
    {
        gridState[theOperator].ChangeState(state);
    }

    public void ChangeUIState(MyEnum.GridUIState state)
    {
        if(currentUIState == state) return;
        currentUIState = state;
        spriteLayer[MyEnum.GridSpriteLayer.UI].enabled = (state != MyEnum.GridUIState.Hide);
        spriteLayer[MyEnum.GridSpriteLayer.UI].color = MyConst.GridUIColor[state];
    }
    public bool RemoveUnit()
    {
        if(unit == null)
        {
            return false;
        }
        unit = null;
        return true;
    }
    public bool AddUnit(GameObject unit , MyEnum.TheOperator theOperator)
    {
        if(this.unit != null)
        {
            return false;
        }
        this.unit = unit;
        return true;
    }
    public bool ChangeControlArea(MyEnum.TheOperator theOperator , bool isAdd)
    {
        foreach(MyEnum.TheOperator midOperator in MyEnum.TheOperator.GetValues(typeof(MyEnum.TheOperator)))
        {
            if(midOperator == theOperator) continue;
            gridState[midOperator].controlledCnt += isAdd ? 1 : -1;
        }
        return true;
    }
    public bool ChangeVirtualField(MyEnum.TheOperator theOperator , bool isAdd)
    {
        gridState[theOperator].WatchedCnt += isAdd ? 1 : -1;
        return true;
    }
    public float GetMoveCost(MyEnum.TheOperator theOperator)
    {
        if(gridState[theOperator].currentState != MyEnum.GridState.Show || baseGrid.moveCost < 0 || (unit != null /* && unit.theOperator != theOperator*/))
        {
            return -1;
        }
        return (baseGrid.moveCost + gridState[theOperator].moveOffset) * (gridState[theOperator].controlledCnt > 0 ? 2 : 1);
    }
    public float GetAtkOffset(MyEnum.TheOperator theOperator)
    {
        return baseGrid.atkOffset + gridState[theOperator].atkOffset;
    }
    public float GetDefOffset(MyEnum.TheOperator theOperator)
    {
        return baseGrid.defOffset + gridState[theOperator].defOffset;
    }
}
