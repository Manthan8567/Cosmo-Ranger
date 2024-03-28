using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySfxManager : MonoBehaviour
{
    private Health health;

    // You can find audio lists in the inspector of AudioManager
    private string takeDamageSfx = "EnemyTakeDamage";
    private string deathSfx = "EnemyDeath";

    private void OnEnable()
    {
        health = GetComponent<Health>();

        health.OnTakeDamage += PlayTakeDamageSound;
        health.OnDie += PlayDeathSound;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= PlayTakeDamageSound;
        health.OnDie -= PlayDeathSound;
    }

    private void PlayTakeDamageSound(int damage)
    {
        AudioManager.Singleton.PlaySoundEffect(takeDamageSfx);
    }

    private void PlayDeathSound()
    {
        AudioManager.Singleton.PlaySoundEffect(deathSfx);
    }
}
