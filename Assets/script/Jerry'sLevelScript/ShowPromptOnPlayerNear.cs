// using UnityEngine;// 提示 UI 脚本，放在提示 UI 上，设置玩家标签，玩家进入触发范围时显示提示 UI，离开时隐藏提示 UI

// public class ShowPromptOnPlayerNear : MonoBehaviour
// {
//     [Header("UI Prompt")]
//     public GameObject promptUI;  

//     [Header("Player Tag")]
//     public string playerTag = "Player";

//     private void Start()
//     {
//         if (promptUI != null)
//         {
//             promptUI.SetActive(false);
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag(playerTag))
//         {
//             if (promptUI != null)
//             {
//                 promptUI.SetActive(true);
//             }
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag(playerTag))
//         {
//             if (promptUI != null)
//             {
//                 promptUI.SetActive(false);
//             }
//         }
//     }
// }
using UnityEngine;

public class ShowPromptOnPlayerNear : MonoBehaviour
{
    public GameObject promptUI;
    public string playerTag = "Player";

    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
            // Debug.Log("Prompt UI assigned: " + promptUI.name);
        }
        else
        {
            // Debug.LogError("Prompt UI is not assigned!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Trigger enter by: " + other.name);

        if (!other.CompareTag(playerTag)) return;

        // Debug.Log("Player entered prompt trigger");

        if (promptUI != null)
            promptUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Trigger exit by: " + other.name);

        if (!other.CompareTag(playerTag)) return;

        // Debug.Log("Player exited prompt trigger");

        if (promptUI != null)
            promptUI.SetActive(false);
    }
}