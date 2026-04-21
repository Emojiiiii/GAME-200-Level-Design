using UnityEngine;// 火车时间控制脚本，放在场景中一个空物体上，设置火车根物体和 TrainLoop 脚本，监听 TimeStateManager 的时间状态变化事件，在进入过去时显示火车并开始移动，进入现在时停止移动并隐藏火车

public class TrainTimeController : MonoBehaviour
{
    public GameObject trainRoot;
    public TrainLoop mover;

    private void OnEnable()
    {
        TimeStateManager.Instance.OnTimeStateChanged += HandleTimeChanged;
    }

    private void OnDisable()
    {
        TimeStateManager.Instance.OnTimeStateChanged -= HandleTimeChanged;
    }

    void HandleTimeChanged(TimeState state)
    {
        if (state == TimeState.Past)
            EnterPast();
        else
            EnterPresent();
    }

    void EnterPast()
    {
        trainRoot.SetActive(true);
        mover.ResetTrain();
        mover.StartMoving();
    }

    void EnterPresent()
    {
        mover.StopMoving();
        trainRoot.SetActive(false);
    }
}