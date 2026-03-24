using UnityEngine;//敌人躲光脚本，放在光源上

public class FlashlightBeam : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.SetFlashlightHit(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.SetFlashlightHit(false);
        }
    }
}