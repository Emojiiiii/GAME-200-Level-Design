using UnityEngine;// 手电筒（物品）拾取脚本，放在物体上，玩家进入触发范围后按 F 键（可自定义）拾取物体并销毁物体

public class FlashlightPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public string playerTag = "Player";
    public KeyCode pickupKey = KeyCode.F;

    private bool playerInRange = false;
    private FlashlightController playerFlashlight;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(pickupKey) && playerFlashlight != null)
        {
            playerFlashlight.PickupFlashlight();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        FlashlightController controller = other.GetComponent<FlashlightController>();
        if (controller != null)
        {
            playerInRange = true;
            playerFlashlight = controller;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (other.GetComponent<FlashlightController>() == playerFlashlight)
        {
            playerInRange = false;
            playerFlashlight = null;
        }
    }
}