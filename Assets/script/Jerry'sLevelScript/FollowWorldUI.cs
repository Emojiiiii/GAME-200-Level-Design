using UnityEngine;// 跟随世界坐标的 UI 脚本，放在 UI 元素上，设置目标物体和偏移，UI 会跟随目标物体在屏幕上显示（一般不用这么麻烦的脚本）

public class FollowWorldUI : MonoBehaviour
{
    public Transform target;
    public Vector3 worldOffset = Vector3.zero;
    public Camera targetCamera;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (targetCamera == null)
            targetCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (target == null || targetCamera == null) return;

        Vector3 screenPos = targetCamera.WorldToScreenPoint(target.position + worldOffset);

        if (screenPos.z > 0)
        {
            rectTransform.position = screenPos;
        }
    }
}