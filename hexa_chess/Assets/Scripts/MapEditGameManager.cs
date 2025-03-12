using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // List<int> test1 = new List<int>(10);
        // Debug.Log(test1[3]);
        // return;
        Debug.Log("MapEditGameManager Start");
        MapManager.Init();
        UIManager.Init();
        MapManager.Instance_edit.EditMap(10);
        UIManager.Instance.ShowView(MyEnum.UIView.MapEditView);
    }

}
