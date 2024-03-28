using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfxManager : MonoBehaviour
{
    [SerializeField] ExperienceManager experienceManager;
    private Health health;

    // You can find audio lists in the inspector of AudioManager
    private string takeDamageSfx = "PlayerTakeDamage";
    private string deathSfx = "PlayerDeath";

    private string levelUpSfx = "LevelUp";

    private void OnEnable()
    {
        health = GetComponent<Health>();

        health.OnTakeDamage += PlayTakeDamageSound;
        health.OnDie += PlayDeathSound;

        experienceManager.OnLevelUp += PlayLevelUpSound;   
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= PlayTakeDamageSound;
        health.OnDie -= PlayDeathSound;

        experienceManager.OnLevelUp -= PlayLevelUpSound;
    }

    private void PlayTakeDamageSound(int damage)
    {
        AudioManager.Singleton.PlaySoundEffect(takeDamageSfx);
    }

    private void PlayDeathSound()
    {
        AudioManager.Singleton.PlaySoundEffect(deathSfx);
    }

    private void PlayLevelUpSound()
    {
        AudioManager.Singleton.PlaySoundEffect(levelUpSfx);
    }
}