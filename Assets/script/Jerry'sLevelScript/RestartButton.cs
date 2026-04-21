using UnityEngine;// 这个脚本放在游戏结束场景的重启按钮上，当玩家点击按钮时，重新加载玩家死前所在的关卡

public class RestartButton : MonoBehaviour
{
    public void RestartLevel()
    {
        LoadGameOverOnTrigger.RestartLastLevel();
    }
}