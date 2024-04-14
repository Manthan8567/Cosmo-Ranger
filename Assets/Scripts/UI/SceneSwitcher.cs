using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public InventoryObject inventoryObject;

    void Start()
    {
        // Load inventory data when the scene switches
        inventoryObject.Load();
    }

    public void SwitchScene(int sceneIndex)
    {
        // Save inventory data before switching scenes
        inventoryObject.Save();

        // Resume the game before switching scenes
        GameManager.Singleton.ResumeGame();

        // Switch to the specified scene
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        inventoryObject.Clear();
        // Quit the application
        Application.Quit();
    }
}
