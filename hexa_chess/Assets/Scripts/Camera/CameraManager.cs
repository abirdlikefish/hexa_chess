using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public interface ICameraManager
{
    void Move(Vector2 direction);
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
    }
    private Transform anchor;
    public void Move(Vector2 direction)
    {
        anchor.position += new Vector3(direction.x, 0, direction.y);
    }
    public void LookAttGrid(Vector2Int grid)
    {
        anchor.position = new Vector3(grid.x, grid.y, 0);
    }
}
