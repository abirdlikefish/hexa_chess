using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyEvent
{
    public static Action<Vector2Int?> OnGridClick_left;
    public static Action<Vector2Int?> OnGridClick_right;
}
