using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalSettingSO", menuName = "SO/GlobalSettingSO")]
public class GlobalSettingSO : ScriptableObject
{
    public Vector3 cameraRange;
    public int mapSize;
    public float showGridInfoWin_preTime;
    public float showGridInfoWin_minMoveDis;
}
