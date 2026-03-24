using System.Collections;// 近距离按 F 键显示 UI 脚本，放在场景中一个空物体上，设置目标 UI、玩家标签和显示时间，玩家进入触发范围后按 F 键会短暂显示 UI
using UnityEngine;

public class ShowUIOnF_NearObject : MonoBehaviour
{
    public GameObject targetUI;
    public string playerTag = "Player";
    public float showTime = 1f;

    private bool playerInRange = false;
    private Coroutine currentCoroutine;

    void Start()
    {
        if (targetUI != null)
            targetUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (targetUI != null)
            {
                if (currentCoroutine != null)
                    StopCoroutine(currentCoroutine);

                currentCoroutine = StartCoroutine(ShowUIBriefly());
            }
        }
    }

    IEnumerator ShowUIBriefly()
    {
        targetUI.SetActive(true);
        yield return new WaitForSeconds(showTime);
        targetUI.SetActive(false);
        currentCoroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
        }
    }
}