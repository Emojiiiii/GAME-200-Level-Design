using UnityEngine;// 场景切换触发器，放在场景中一个空物体上，设置玩家标签和下一个场景名称，玩家进入触发范围后自动切换到下一个场景
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string playerTag = "Player";
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}