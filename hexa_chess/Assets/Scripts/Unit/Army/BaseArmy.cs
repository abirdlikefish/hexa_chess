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
    protected Controller armyStateController = null;

    public override void RoundBeginCheck()
    {
        base.RoundBeginCheck();
        operationBuff = MyEnum.OperationBuff.Normal;
        RefreshArmyInfoView();
    }



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
                // unitState = MyEnum.UnitStates.Able;
                // Debug.Log("移动动画完成");
                UnitManager.Instance.ExitGrid(this);
                this.Coord = path[0];
                UnitManager.Instance.EnterGrid(this);
                MyEvent.AnimaEnd?.Invoke();
            });
            MoveForce -= cost;
            if(MoveForce <= 0)
            {
                unitState = MyEnum.UnitStates.Disable;
            }
            
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
        RefreshArmyInfoView();
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
            armyStateController = armyInfoView.GetController("StateController");
        }
        armyInfoView_hp.text = CurrentHP.ToString();
        armyInfoView_atk.text = Atk.ToString();
        armyStateController.selectedPage = operationBuff.ToString();
    }

    public void Station()
    {
        MoveForce = 0;
        operationBuff = MyEnum.OperationBuff.Station;
        unitState = MyEnum.UnitStates.Disable;
        RefreshArmyInfoView();
        StartCoroutine(Tmp_ArmyAnimation());
    }

    public void Rest()
    {
        MoveForce = 0;
        operationBuff = MyEnum.OperationBuff.Rest;
        unitState = MyEnum.UnitStates.Disable;
        RefreshArmyInfoView();
        StartCoroutine(Tmp_ArmyAnimation());
    }

    public void Dismiss()
    {
        StartCoroutine(Tmp_ArmyAnimation_Dismiss());
    }

    public void Skip()
    {
        MoveForce = 0;
        unitState = MyEnum.UnitStates.Disable;
        RefreshArmyInfoView();
        // StartCoroutine(Tmp_ArmyAnimation());
    }

    IEnumerator Tmp_ArmyAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        MyEvent.AnimaEnd?.Invoke();
    }
    IEnumerator Tmp_ArmyAnimation_Dismiss()
    {
        yield return new WaitForSeconds(0.3f);
        MyEvent.AnimaEnd?.Invoke();
        UnitManager.Instance.ReceiveIncome(theOperator,coin);
        UnitManager.Instance.RemoveUnit(this);
    }

    
}
