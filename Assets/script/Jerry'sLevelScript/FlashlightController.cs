using System.Collections;
using UnityEngine;

// 手电筒控制脚本，放在玩家上
// 负责：朝向、按 C 开关、拾取后显示提示 UI
public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight")]
    public GameObject flashlightObject;
    public Transform flashlightHolder;
    public bool hasFlashlight = false;

    [Header("Facing")]
    public Vector3 rightLocalPosition = new Vector3(0.3f, 0f, 0f);
    public Vector3 leftLocalPosition = new Vector3(-0.3f, 0f, 0f);

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

        UpdateFacingVisual();
    }

    void Update()
    {
        UpdateFacing();
        HandleFlashlightToggle();
    }

    void UpdateFacing()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0.01f)
            faceRight = true;
        else if (moveInput < -0.01f)
            faceRight = false;

        UpdateFacingVisual();
    }

    void UpdateFacingVisual()
    {
        if (flashlightHolder == null) return;

        if (faceRight)
        {
            flashlightHolder.localPosition = rightLocalPosition;
            flashlightHolder.localEulerAngles = new Vector3(0f, 0f, 270f);
        }
        else
        {
            flashlightHolder.localPosition = leftLocalPosition;
            flashlightHolder.localEulerAngles = new Vector3(0f, 0f, 90f);
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