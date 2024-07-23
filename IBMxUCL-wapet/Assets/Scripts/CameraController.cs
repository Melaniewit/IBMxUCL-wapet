using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // 目标模型
    public Vector3 cameraPosition = new Vector3(0, 0.4f, -1.5f); // 摄像机的位置
    public Vector3 cameraRotation = new Vector3(0, 0, 0); // 摄像机的旋转
    
    void Start()
    {
        // 确保目标模型已正确设置
        if (target == null)
        {
            Debug.LogError("目标模型未设置，请在Inspector中设置目标模型。");
        }
        else
        {
            Debug.Log("目标模型已设置为: " + target.name);
        }
    }

    void Update()
    {
        // 如果目标模型存在，则更新摄像机位置和旋转
        if (target != null)
        {
            transform.position = cameraPosition;
            transform.eulerAngles = cameraRotation;
            transform.LookAt(target);
            Debug.Log("摄像机位置已更新，当前面向目标模型: " + target.name);
        }
        else
        {
            Debug.LogWarning("目标模型未找到，请检查目标模型是否在场景中。");
        }
    }
}
