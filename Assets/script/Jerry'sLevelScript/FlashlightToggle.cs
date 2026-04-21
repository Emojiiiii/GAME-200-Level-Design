using UnityEngine;// 灯开关脚本，放在玩家上，按 E 键开灯（第二关偷懒）
public class FlashlightToggle : MonoBehaviour
{
    public GameObject flashlightObject;
    private bool isOn = false;

    void Start()
    {
        if (flashlightObject != null)
        {
            flashlightObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOn = !isOn;

            if (flashlightObject != null)
            {
                flashlightObject.SetActive(isOn);
            }
        }
    }
}