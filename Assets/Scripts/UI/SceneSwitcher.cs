using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public InventoryObject inventoryObject;
    public DisplayInventory displayInventory;


    public void SwitchScene(int sceneIndex)
    {
        // Save inventory data before switching scenes
        inventoryObject.Save();

        // Resume the game before switching scenes
        GameManager.Singleton.ResumeGame();

        inventoryObject.Load();
        
        // Switch to the specified scene
        SceneManager.LoadScene(sceneIndex);

        displayInventory.UpdateDisplay();
    }

    public void QuitGame()
    {
        inventoryObject.Clear();
        // Quit the application
        //Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
