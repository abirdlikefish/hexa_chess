using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public interface ICameraManager
{
    void Move(Vector3 direction);
    void LookAttGrid(Vector2Int grid);
    void Init(Vector2 center);
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
    private Vector2 center;
    public void Move(Vector3 direction)
    {
        // anchor.position += new Vector3(direction.x, 0, direction.y);
        anchor.position += direction;
        CorrectCamera();
    }
    public void LookAttGrid(Vector2Int grid)
    {
        anchor.position = new Vector3(grid.x, grid.y, 0);
        CorrectCamera();
    }
    public void Init(Vector2 center)
    {
        this.center = center;
        anchor.position = new Vector3(center.x, center.y, 0);        
    }
    private void CorrectCamera()
    {
        if(anchor.position.x < center.x - GameManager.instance.globalSettingSO.cameraRange.x)
        {
            anchor.position = new Vector3(center.x - GameManager.instance.globalSettingSO.cameraRange.x, anchor.position.y, anchor.position.z);
        }
        if(anchor.position.x > center.x + GameManager.instance.globalSettingSO.cameraRange.x)
        {
            anchor.position = new Vector3(center.x + GameManager.instance.globalSettingSO.cameraRange.x, anchor.position.y, anchor.position.z);
        }
        if(anchor.position.y < center.y - GameManager.instance.globalSettingSO.cameraRange.y)
        {
            anchor.position = new Vector3(anchor.position.x, center.y - GameManager.instance.globalSettingSO.cameraRange.y, anchor.position.z);
        }
        if(anchor.position.y > center.y + GameManager.instance.globalSettingSO.cameraRange.y)
        {
            anchor.position = new Vector3(anchor.position.x, center.y + GameManager.instance.globalSettingSO.cameraRange.y, anchor.position.z);
        }
        if(anchor.position.z > GameManager.instance.globalSettingSO.cameraRange.z)
        {
            anchor.position = new Vector3(anchor.position.x, anchor.position.y, GameManager.instance.globalSettingSO.cameraRange.z);
        }
        if(anchor.position.z < -GameManager.instance.globalSettingSO.cameraRange.z)
        {
            anchor.position = new Vector3(anchor.position.x, anchor.position.y, -GameManager.instance.globalSettingSO.cameraRange.z);
        }
    }
}
