using UnityEngine;
using TMPro;

public class LevelUpEffectManager : MonoBehaviour
{
    public TextMeshProUGUI levelUpText; // Reference to your Text Mesh Pro UI element

    void Start()
    {
        ExperienceManager.Singleton.OnLevelUp += PlayLevelUpEffect;
    }

    private void OnDisable()
    {
        ExperienceManager.Singleton.OnLevelUp -= PlayLevelUpEffect;
    }

    public void PlayLevelUpEffect()
    {
        GetComponent<ParticleSystem>().Play();
        levelUpText.gameObject.SetActive(true); // Activate the Text Mesh Pro UI element
        Invoke("DeactivateText", 3.3f); // Deactivate the text after 3 seconds
    }

    void DeactivateText()
    {
        levelUpText.gameObject.SetActive(false); // Deactivate the Text Mesh Pro UI element
    }
}