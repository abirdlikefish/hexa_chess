using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FairyGUI;

public class BaseArmy : Unit , IArmy
{
    private MyEnum.ArmyType armyType;
    public MyEnum.ArmyType ArmyType => armyType;
    private float movingSpeed;
    public float MovingSpeed{get {return movingSpeed;}}    
    // public override Vector2Int Coord {get {return base.Coord;} set{coordPosition = value; transform.position = MapManager.Coord_To_Pos(value);}}
    public override int CurrentHP {get {return base.CurrentHP;} set{base.CurrentHP = value;RefreshArmyInfoView();}}
    public override Vector2Int Coord {get {return base.Coord;} set{base.Coord = value;RefreshArmyInfoView();}}
   
    protected GTextField armyInfoView_hp = null;
    protected GTextField armyInfoView_atk = null;
    public void Move(List<Vector2Int> path,float cost)
    {
        if (unitState == MyEnum.UnitStates.Able)
        {
            Sequence sequence = DOTween.Sequence();
            Debug.Log($"Path Count:{path?.Count}");
            for(int i = path.Count - 2; i >= 0; i--)
            {
                sequence.Append(transform.DOMove((Vector3)MapManager.Coord_To_Pos(path[i]),1.0f/MovingSpeed).SetEase(Ease.Linear));
            }
            unitState = MyEnum.UnitStates.Disable;
            sequence.Play();
            sequence.OnComplete(() => {
                unitState = MyEnum.UnitStates.Able;
                // Debug.Log("移动动画完成");
                UnitManager.Instance.ExitGrid(this);
                this.Coord = path[0];
                UnitManager.Instance.EnterGrid(this);
                MyEvent.AnimaEnd?.Invoke();
            });
            MoveForce -= cost;
            
        }
        else
        {
            Debug.Log("单位不可操作！");
        }
    }

    protected override void InitConfig(MyStruct.UnitConfig iniConfig)
    {
        unitType = MyEnum.UnitType.Army;
        armyType = iniConfig.armyType;
        movingSpeed = iniConfig.movingSpeed;
        maxHp = iniConfig.MaxHp;
        CurrentHP = maxHp;
        maxMoveForce = iniConfig.Action;
        atk = iniConfig.Atk;
        def = iniConfig.Def;
        attackRadius = iniConfig.AttackRadius;
        viewRange = iniConfig.viewRange;
        coin = iniConfig.Coin;
        occupation = iniConfig.Occupation;
        haveZOC = iniConfig.HaveZOC;
    }

    protected void RefreshArmyInfoView()
    {
        if(armyInfoView_hp == null)
        {
            UIPanel uiPanel = transform.Find("ArmyInfoView").GetComponent<UIPanel>();
            GComponent armyInfoView = uiPanel.ui;
            if(armyInfoView == null)
                Debug.LogError("ArmyInfoView is null");
            armyInfoView_hp = armyInfoView.GetChild("HpTxt").asTextField;
            armyInfoView_atk = armyInfoView.GetChild("AtkTxt").asTextField;
        }
        armyInfoView_hp.text = CurrentHP.ToString();
        armyInfoView_atk.text = Atk.ToString();
    }
    
}
