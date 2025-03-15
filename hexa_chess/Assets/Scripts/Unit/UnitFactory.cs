using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitFactory : MonoBehaviour
{
    private static UnitFactory _instance;

    private UnitFactory()
    {

    }
    //单例访问模式
    public static UnitFactory Instacne
    {
        get
        {
            if(_instance == null)
            {
                _instance = new UnitFactory();
            }
            return _instance;
        }
    }

    public static IUnit LoadUnit(Transform transform,UnitType unitType)
    {
        //生成实例对象
        GameObject gameObject = Instantiate((GameObject)Resources.Load("Unit"),transform);
        //地图坐标转换
        Vector2 vector2 = new Vector2(transform.position.x,transform.position.y);
        gameObject.GetComponent<IUnit>().ReWritePosition(vector2);
        //存入地图
        MapManager.Instance.AddUnit(MapManager.Pos_To_Coord(vector2),gameObject);
        return gameObject.GetComponent<IUnit>();
    }
}
