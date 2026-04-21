using UnityEngine;

public class ShowPromptOnPlayerNearOnlyWhenPaused : MonoBehaviour
{
    public GameObject promptUI;
    public string playerTag = "Player";

    private bool playerInside = false;

    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (promptUI == null) return;

        // 时间没有暂停时，强制隐藏提示
        if (!TimePauseManager.IsPaused)
        {
            promptUI.SetActive(false);
            return;
        }

        // 时间暂停时，如果玩家在范围内，就显示
        promptUI.SetActive(playerInside);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        playerInside = true;

        if (TimePauseManager.IsPaused && promptUI != null)
        {
            promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        playerInside = false;

        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }
}