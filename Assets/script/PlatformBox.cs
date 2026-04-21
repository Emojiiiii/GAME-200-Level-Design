using UnityEngine;
using Cinemachine;

public class PlatformBox : MonoBehaviour
{
    private bool isPlayerInside = false;
    private GameObject[] allPlatforms;
    public bool boxType;
    public GameObject plat1;
    GameObject plat2;
    GameObject plat3;
    GameObject plat4;
    GameObject plat5;
    
    void Awake()
    {
        allPlatforms = GameObject.FindGameObjectsWithTag("MovingPlatform");
    }

    // Detect when player enters the box
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    // Detect when player leaves the box
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        // Check for button press (e.g., the 'E' key)
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (CheckInteraction())
            {
            }
        }
    }

    

    public bool CheckInteraction()
    {
        return isPlayerInside;
    }
}
