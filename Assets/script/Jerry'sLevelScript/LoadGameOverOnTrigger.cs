using UnityEngine;// 这个脚本放在一个触发器上，当玩家进入触发器时，加载游戏结束场景
using UnityEngine.SceneManagement;

public class LoadGameOverOnTrigger : MonoBehaviour
{
    public string playerTag = "Player";
    public string gameOverSceneName = "gameover";

    // 记录玩家死前所在关卡
    public static string LastLevelSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        LastLevelSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Saved level: " + LastLevelSceneName);

        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }

    public static void RestartLastLevel()
    {
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(LastLevelSceneName))
        {
            SceneManager.LoadScene(LastLevelSceneName);
        }
        else
        {
            Debug.LogWarning("No saved level to restart.");
        }
    }
}