using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ExpBarManager : MonoBehaviour
{
    [SerializeField] private Image expBar;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI levelText;


    private void Start()
    {
        // Subscribe events
        ExperienceManager.Singleton.OnGainExperience += UpdateExpBar;

        ExperienceManager.Singleton.OnLevelUp += UpdateExpBar;
        ExperienceManager.Singleton.OnLevelUp += UpdateLevelText;

        UpdateLevelText();
        UpdateExpBar();
    }

    private void OnDisable()
    {
        ExperienceManager.Singleton.OnGainExperience -= UpdateExpBar;

        ExperienceManager.Singleton.OnLevelUp -= UpdateExpBar;
        ExperienceManager.Singleton.OnLevelUp -= UpdateLevelText;
    }

    public void UpdateExpBar()
    {
        expBar.fillAmount = ExperienceManager.Singleton.currentExperience / ExperienceManager.Singleton.expForNextLevel;

        UpdateExpText();
    }

    private void UpdateExpText()
    {
        string currExp = ExperienceManager.Singleton.currentExperience.ToString();
        string maxExp = ExperienceManager.Singleton.expForNextLevel.ToString();
        expText.text = currExp + "/" + maxExp;
    }

    public void UpdateLevelText()
    {
        levelText.text = "Lv. " + ExperienceManager.Singleton.currentLevel.ToString();
    }
}
