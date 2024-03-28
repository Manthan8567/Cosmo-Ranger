using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfxManager : MonoBehaviour
{
    [SerializeField] private ExperienceManager experienceManager;

    private Health health;

    private string takeDamageSfx = "PlayerTakeDamage";
    private string deathSfx = "PlayerDeath";
    private string levelUpSfx = "LevelUp";

    private void OnEnable()
    {
        health = GetComponent<Health>();

        health.OnDie += PlayDeathSfx;
        health.OnTakeDamage += PlayDamageSfx;
        experienceManager.OnLevelUp += PlayLevelUpSfx;
    }

    private void OnDisable()
    {
        health.OnDie -= PlayDeathSfx;
        health.OnTakeDamage -= PlayDamageSfx;
        experienceManager.OnLevelUp -= PlayLevelUpSfx;
    }

    private void PlayDamageSfx(int damage)
    {
        AudioManager.Singleton.PlaySoundEffect(takeDamageSfx);
    }

    private void PlayDeathSfx()
    {
        AudioManager.Singleton.PlaySoundEffect(deathSfx);
    }

    private void PlayLevelUpSfx()
    {
        AudioManager.Singleton.PlaySoundEffect(levelUpSfx);
    }
}
