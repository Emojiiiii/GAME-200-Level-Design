using System.Collections;// 手电筒控制脚本，放在玩家上，按 C 键切换手电筒开关，拿到手电筒后显示提示 UI 3 秒
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight")]
    public GameObject flashlightObject;     
    public Transform flashlightHolder;      
    public bool hasFlashlight = false;

    [Header("UI")]
    public GameObject tipUI;                

    private bool flashlightOn = false;
    private bool faceRight = true;
    private Coroutine tipCoroutine;

    void Start()
    {
        if (flashlightObject != null)
            flashlightObject.SetActive(false);

        if (tipUI != null)
            tipUI.SetActive(false);
    }

    void Update()
    {
        UpdateFacing();
        HandleFlashlightToggle();
    }

    void UpdateFacing()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            faceRight = true;
        else if (moveInput < 0)
            faceRight = false;

        if (flashlightHolder != null)
        {
            Vector3 scale = flashlightHolder.localScale;
            scale.x = Mathf.Abs(scale.x) * (faceRight ? 1f : -1f);
            flashlightHolder.localScale = scale;
        }
    }

    void HandleFlashlightToggle()
    {
        if (!hasFlashlight) return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            flashlightOn = !flashlightOn;

            if (flashlightObject != null)
                flashlightObject.SetActive(flashlightOn);
        }
    }

    public void PickupFlashlight()
    {
        if (hasFlashlight) return;

        hasFlashlight = true;

        if (tipUI != null)
        {
            tipUI.SetActive(true);

            if (tipCoroutine != null)
                StopCoroutine(tipCoroutine);

            tipCoroutine = StartCoroutine(HideTipAfterSeconds(3f));
        }
    }

    IEnumerator HideTipAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (tipUI != null)
            tipUI.SetActive(false);

        tipCoroutine = null;
    }
}