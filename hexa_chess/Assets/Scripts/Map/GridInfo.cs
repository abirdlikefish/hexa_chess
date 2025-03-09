using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInfo : MonoBehaviour
{
    private static GameObject parentGo;
    private static Sprite fogSprite;
    private static Sprite uiSprite;
    private GameObject unit;
    private Dictionary<Enum.GridSpriteLayer,SpriteRenderer> spriteLayer;
    private Enum.GridType gridType;
    private Vector2Int gridPosition;
    private BaseGrid baseGrid;
    private Dictionary<Enum.TheOperator, GridState> gridState;
    public Enum.GridUIState currentUIState;
    public class GridState
    {
        private GridState() { }
        private Enum.TheOperator theOperator;
        private GridInfo gridInfo;
        public GridState(GridInfo gridInfo)
        {
            this.gridInfo = gridInfo;
            currentState = Enum.GridState.UnInit;
            atkOffset = 0;
            defOffset = 0;
            moveOffset = 0;
            controlledCnt = 0;
            watchedCnt = 0;
        }
        public Enum.GridState currentState;
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
                    ChangeState(Enum.GridState.Show);
                }
                else
                {
                    ChangeState(Enum.GridState.Hide);
                }
            }
        }
        public void ChangeState(Enum.GridState state)
        {
            if(currentState == state) return;
            currentState = state;
            if(theOperator == Enum.TheOperator.Player)
            {
                gridInfo.ChangeSpriteLayer(state);
            }
        }
        public GridState Init(Enum.TheOperator theOperator)
        {
            this.theOperator = theOperator;
            return this;
        }
    }
    private void ChangeSpriteLayer(Enum.GridState state)
    {
        spriteLayer[Enum.GridSpriteLayer.Base].enabled = false;
        spriteLayer[Enum.GridSpriteLayer.Fog].enabled = false;
        spriteLayer[Enum.GridSpriteLayer.UI].enabled = false;
        switch (state)
        {
            case Enum.GridState.Fog:
                spriteLayer[Enum.GridSpriteLayer.Fog].enabled = true;
                break;
            case Enum.GridState.Show:
                spriteLayer[Enum.GridSpriteLayer.Base].enabled = true;
                break;
            case Enum.GridState.Hide:
                spriteLayer[Enum.GridSpriteLayer.UI].enabled = true;
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

        gridInfo.spriteLayer = new Dictionary<Enum.GridSpriteLayer, SpriteRenderer>();
        foreach (Enum.GridSpriteLayer layer in Enum.GridSpriteLayer.GetValues(typeof(Enum.GridSpriteLayer)))
        {
            GameObject midGo = new GameObject(layer.ToString());
            midGo.transform.SetParent(go.transform);
            midGo.transform.localPosition = Vector3.zero;
            gridInfo.spriteLayer[layer] = midGo.AddComponent<SpriteRenderer>();
            gridInfo.spriteLayer[layer].enabled = false;
            gridInfo.spriteLayer[layer].sortingLayerName = Const.GridSpriteSortingLayer[layer];
        }
        gridInfo.spriteLayer[Enum.GridSpriteLayer.Base].sprite = baseGrid.GridPicture;
        gridInfo.spriteLayer[Enum.GridSpriteLayer.Fog].sprite = fogSprite;
        gridInfo.spriteLayer[Enum.GridSpriteLayer.UI].sprite = uiSprite;
        // gridInfo.spriteLayer[Enum.GridSpriteLayer.UI].enabled = true;
        gridInfo.currentUIState = Enum.GridUIState.Hide;
        gridInfo.ChangeUIState(Enum.GridUIState.Hide);


        gridInfo.gridState = new Dictionary<Enum.TheOperator, GridState>();
        foreach (Enum.TheOperator theOperator in Enum.TheOperator.GetValues(typeof(Enum.TheOperator)))
        {
            gridInfo.gridState[theOperator] = new GridState(gridInfo).Init(theOperator);
            gridInfo.gridState[theOperator].ChangeState(Enum.GridState.Fog);
        }
        return gridInfo;
    }
    
    public void ChangeState(Enum.TheOperator theOperator, Enum.GridState state)
    {
        gridState[theOperator].ChangeState(state);
    }

    public void ChangeUIState(Enum.GridUIState state)
    {
        if(currentUIState == state) return;
        currentUIState = state;
        spriteLayer[Enum.GridSpriteLayer.UI].enabled = (state != Enum.GridUIState.Hide);
        spriteLayer[Enum.GridSpriteLayer.UI].color = Const.GridUIColor[state];
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
    public bool AddUnit(GameObject unit , Enum.TheOperator theOperator)
    {
        if(this.unit != null)
        {
            return false;
        }
        this.unit = unit;
        return true;
    }
    public bool ChangeControlArea(Enum.TheOperator theOperator , bool isAdd)
    {
        foreach(Enum.TheOperator midOperator in Enum.TheOperator.GetValues(typeof(Enum.TheOperator)))
        {
            if(midOperator == theOperator) continue;
            gridState[midOperator].controlledCnt += isAdd ? 1 : -1;
        }
        return true;
    }
    public bool ChangeVirtualField(Enum.TheOperator theOperator , bool isAdd)
    {
        gridState[theOperator].WatchedCnt += isAdd ? 1 : -1;
        return true;
    }
    public float GetMoveCost(Enum.TheOperator theOperator)
    {
        if(gridState[theOperator].currentState != Enum.GridState.Show || baseGrid.moveCost < 0 || (unit != null /* && unit.theOperator != theOperator*/))
        {
            return -1;
        }
        return (baseGrid.moveCost + gridState[theOperator].moveOffset) * (gridState[theOperator].controlledCnt > 0 ? 2 : 1);
    }
    public float GetAtkOffset(Enum.TheOperator theOperator)
    {
        return baseGrid.atkOffset + gridState[theOperator].atkOffset;
    }
    public float GetDefOffset(Enum.TheOperator theOperator)
    {
        return baseGrid.defOffset + gridState[theOperator].defOffset;
    }
}
