using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ExpBarManager : MonoBehaviour
{
    [SerializeField] private Image expBar;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI levelText;

    private ExperienceManager experienceManager;


    private void Start()
    {
        experienceManager = GameObject.FindWithTag("ExperienceManager").GetComponent<ExperienceManager>();

        // Subscribe events
        experienceManager.OnGainExperience += UpdateExpBar;

        experienceManager.OnLevelUp += UpdateExpBar;
        experienceManager.OnLevelUp += UpdateLevelText;

        UpdateLevelText();
        UpdateExpBar();
    }

    public void UpdateExpBar()
    {
        expBar.fillAmount = experienceManager.currentExperience / experienceManager.expForNextLevel;

        UpdateExpText();
    }

    private void UpdateExpText()
    {
        string currExp = experienceManager.currentExperience.ToString();
        string maxExp = experienceManager.expForNextLevel.ToString();
        expText.text = currExp + "/" + maxExp;
    }

    public void UpdateLevelText()
    {
        levelText.text = "Lv. " + experienceManager.currentLevel.ToString();
    }
}
