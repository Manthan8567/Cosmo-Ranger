using UnityEngine;


public class LevelUpEffectManager : MonoBehaviour
{
    private ExperienceManager experienceManager;

    private string levelUpSound = "LevelUp";

    void Start()
    {
        experienceManager = GameObject.FindWithTag("ExperienceManager").GetComponent<ExperienceManager>();

        experienceManager.OnLevelUp += PlayLevelUpEffect;
    }

    public void PlayLevelUpEffect()
    {
        this.GetComponent<ParticleSystem>().Play();

        AudioManager.Singleton.PlaySoundEffect(levelUpSound);
    }
}
