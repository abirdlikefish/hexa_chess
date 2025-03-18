using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridConfigSO", menuName = "SO/GridConfigSO", order = 0)]
public class GridConfigSO : ScriptableObject , ISerializationCallbackReceiver
{
    public List<MyStruct.GridConfig> gridConfigs = new List<MyStruct.GridConfig>();
    public Dictionary<MyEnum.GridType, MyStruct.GridConfig> gridConfigDict = new Dictionary<MyEnum.GridType, MyStruct.GridConfig>();
    public void OnAfterDeserialize()
    {
        gridConfigDict = new Dictionary<MyEnum.GridType, MyStruct.GridConfig>();
        foreach (var gridConfig in gridConfigs)
        {
            gridConfigDict[gridConfig.gridType] = gridConfig;
        }
    }
    public void OnBeforeSerialize()
    {}
}
