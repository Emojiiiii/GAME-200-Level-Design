using UnityEngine;// NPC 对话脚本，放在 NPC 上，设置两个对话 UI，根据玩家是否拿到手电筒显示不同的对话内容

public class NPCFlashlightDialogue : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject normalDialogueUI;       // 没拿手电筒时显示
    public GameObject flashlightDialogueUI;   // 拿到手电筒后显示

    private void Start()
    {
        if (normalDialogueUI != null)
            normalDialogueUI.SetActive(false);

        if (flashlightDialogueUI != null)
            flashlightDialogueUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        FlashlightController playerFlashlight = other.GetComponent<FlashlightController>();

        if (playerFlashlight != null && playerFlashlight.hasFlashlight)
        {
            if (normalDialogueUI != null)
                normalDialogueUI.SetActive(false);

            if (flashlightDialogueUI != null)
                flashlightDialogueUI.SetActive(true);
        }
        else
        {
            if (flashlightDialogueUI != null)
                flashlightDialogueUI.SetActive(false);

            if (normalDialogueUI != null)
                normalDialogueUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (normalDialogueUI != null)
            normalDialogueUI.SetActive(false);

        if (flashlightDialogueUI != null)
            flashlightDialogueUI.SetActive(false);
    }
}