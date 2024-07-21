using UnityEngine;

public class AdjustCameraAndModel : MonoBehaviour
{
    public Transform modelTransform;

    void Start()
    {
        // 设置摄像机位置
        Camera.main.transform.position = new Vector3(0, 0.4f, -1.5f); 

        // 调整摄像机方向使其面向3D模型
        Camera.main.transform.LookAt(modelTransform);

        // 调整摄像机FOV，如果需要的话
        Camera.main.fieldOfView = 60; 

        // 调整模型缩放，如果需要的话
        modelTransform.localScale = new Vector3(1f, 1f, 1f); 
    }
}
