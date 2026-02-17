using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour
{
    public string nextSceneName = "room2";

    public KeyCode interactKey = KeyCode.F;
    public bool requireKeyPress = true;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange)
        {
            // Debug.Log("Player in range of door");

            if (!requireKeyPress || Input.GetKeyDown(interactKey))
            {
                // Debug.Log("Interact key pressed. Loading scene...");
                LoadNext();
            }
        }
    }

    void LoadNext()
    {
        // Debug.Log("Trying to load scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Something entered trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player entered door trigger");
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log("Something exited trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player left door trigger");
            playerInRange = false;
        }
    }
}
