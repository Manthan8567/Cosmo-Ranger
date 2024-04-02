using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSfxManager : MonoBehaviour
{
    [SerializeField] ExperienceManager experienceManager;
    private Health health;

    // You can find audio lists in the inspector of AudioManager
    private string attack1Sfx = "PlayerAttack1";
    private string attack2Sfx = "PlayerAttack2";
    private string attack3Sfx = "PlayerAttack3";

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

    #region AnimationEvents
    public void PlaySwordSlash1()
    {
        AudioManager.Singleton.PlaySoundEffect(attack1Sfx);
    }
    public void PlaySwordSlash2()
    {
        AudioManager.Singleton.PlaySoundEffect(attack2Sfx);
    }

    public void PlaySwordSlash3()
    {
        AudioManager.Singleton.PlaySoundEffect(attack3Sfx);
    }
    #endregion

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