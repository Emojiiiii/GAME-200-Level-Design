using System.Collections;// 慢动作触发脚本，放在场景中一个空物体上，设置玩家标签、慢动作倍数和持续时间，玩家进入触发范围后会慢动作一段时间，可以选择只触发一次
using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    public string playerTag = "Player";
    public float slowScale = 0.5f;   // 慢两倍
    public float slowDuration = 2f;  // 持续2秒
    public bool triggerOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered && triggerOnce) return;
        if (!other.CompareTag(playerTag)) return;

        StartCoroutine(SlowMotionCoroutine());

        if (triggerOnce)
            hasTriggered = true;
    }

    IEnumerator SlowMotionCoroutine()
    {
        Time.timeScale = slowScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(slowDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}