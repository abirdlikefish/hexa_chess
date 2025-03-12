using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class midGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("midGameManager Start");
        MapManager.Init();
        UIManager.Init();
        MapManager.Instance.CreateMap(10);
        UIManager.Instance.ShowView(MyEnum.UIView.PlayView);
        MyEvent.OnGridClick_left += (coord) => 
        {
            if (coord.HasValue)
            {
                MapManager.Instance.ChangeVirtualField(MyEnum.TheOperator.Player, coord.Value, true);
            }
            else
            {
                Debug.Log("coord is null");
            }
        };
        MyEvent.OnGridClick_right += (coord) =>
        {
            if (coord.HasValue)
            {
                MapManager.Instance.SearchMovableArea(MyEnum.TheOperator.Player , coord.Value, 3);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
