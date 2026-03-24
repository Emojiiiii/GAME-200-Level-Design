using System.Collections;// 过场镜头脚本，放在场景中一个空物体上，设置摄像机、玩家和敌人，按顺序移动摄像机到敌人位置，停留一段时间，再移回玩家位置，最后开启玩家控制和敌人追逐
using UnityEngine;

public class IntroCameraPan : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform player;
    public Transform enemy;

    [Header("Timing")]
    public float moveToEnemyTime = 1.5f;
    public float stayOnEnemyTime = 1f;
    public float moveBackToPlayerTime = 1.5f;
    public float delayBeforeChase = 1f;

    [Header("Offsets")]
    public Vector3 cameraOffset = new Vector3(0f, 0f, -10f);

    [Header("Enemy Chase")]
    public Enemy enemyChaseScript;

    [Header("Player Control")]
    public MonoBehaviour playerMovementScript; 
    [Header("CameraFollow2D")]
    public CameraFollow2D cameraFollow;

    void Start()
    {
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        if (enemyChaseScript != null)
            enemyChaseScript.canChase = false;

            Rigidbody2D rb = enemyChaseScript.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            
        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        if (cameraFollow != null)
            cameraFollow.enabled = false;

        Vector3 playerStartPos = player.position + cameraOffset;
        Vector3 enemyPos = enemy.position + cameraOffset;

        cameraTransform.position = playerStartPos;

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(MoveCamera(playerStartPos, enemyPos, moveToEnemyTime));

        yield return new WaitForSeconds(stayOnEnemyTime);

        Vector3 playerReturnPos = player.position + cameraOffset;

        yield return StartCoroutine(MoveCamera(enemyPos, playerReturnPos, moveBackToPlayerTime));

        cameraTransform.position = playerReturnPos;

        yield return new WaitForSeconds(delayBeforeChase);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        if (enemyChaseScript != null)
            enemyChaseScript.canChase = true;

        if (cameraFollow != null)
            cameraFollow.enabled = true;
    }

    IEnumerator MoveCamera(Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            cameraTransform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        cameraTransform.position = endPos;
    }
}