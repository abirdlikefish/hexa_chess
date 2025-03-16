using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitFactory : MonoBehaviour
{

    private UnitFactory()
    {

    }

    public static IUnit LoadUnit(Vector2Int crood,MyEnum.UnitType unitType)
    {
        //生成实例对象
        GameObject gameObject = Instantiate((GameObject)Resources.Load("Unit"));//占位语句，表示加载预制体
        UnitConfig config = (UnitConfig)Resources.Load("unitConfig");//占位语句，表示资源加载过程
        gameObject.GetComponent<Unit>().UnitInitialize(config);
        //地图坐标转换
        Vector2 vector2 = MapManager.Coord_To_Pos(crood);
        gameObject.transform.position = vector2;
        gameObject.GetComponent<Unit>().ReWriteCrood(crood);
        //存入地图
        MapManager.Instance.AddUnit(crood,gameObject.GetComponent<IUnit>());
        return gameObject.GetComponent<IUnit>();
    }
}
