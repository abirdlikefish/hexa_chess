using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public interface ICameraManager
{
    void Move(Vector3 direction);
    void LookAttGrid(Vector2Int grid);
}
public class CameraManager : MonoBehaviour , ICameraManager
{
    static CameraManager instance;
    public static ICameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("CameraManager is null");
            }
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
        anchor = new GameObject("Anchor").transform;
        CinemachineVirtualCamera virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = anchor.transform;
        MyEvent.CameraMove += Move;
        MyEvent.DragScreen += (delta) => Move(new Vector3(-delta.x,-delta.y,0));
    }
    private Transform anchor;
    public void Move(Vector3 direction)
    {
        // anchor.position += new Vector3(direction.x, 0, direction.y);
        anchor.position += direction;
        Debug.Log("Camera Move: " + direction);
    }
    public void LookAttGrid(Vector2Int grid)
    {
        anchor.position = new Vector3(grid.x, grid.y, 0);
    }
}
