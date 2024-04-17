using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public InventoryObject inventoryObject;
    public DisplayInventory displayInventory;
    public Item item;


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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
