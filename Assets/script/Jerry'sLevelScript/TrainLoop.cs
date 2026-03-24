using UnityEngine;// 过场火车脚本，放在火车上，设置速度和重生位置，火车会不断从左向右移动，离开右侧后重生到左侧

public class TrainLoop : MonoBehaviour
{
    public float speed = 10f;

    [Header("World X positions")]
    public float spawnX = -12f;     // 放到门后 / 镜头外
    public float despawnX = 12f;    // 放到镜头外更右侧一点
    public float extraPadding = 0.2f;

    private SpriteRenderer sr;
    private float halfWidth;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) halfWidth = sr.bounds.extents.x;
        else halfWidth = 0.5f; // 没有SpriteRenderer就给个兜底
    }

    void Update()
    {
        if (TimePauseManager.IsPaused) return;

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // 当“车的左边缘”都超过 despawnX，说明整辆车完全离开右侧
        if (transform.position.x - halfWidth > despawnX)
        {
            // 让“车的右边缘”在 spawnX 的左侧 => 重生时完全在门后/镜头外
            float newX = spawnX - halfWidth - extraPadding;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}