using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySfxManager : MonoBehaviour
{
    private Health health;

    private string takeDamageSfx = "EnemyTakeDamage";
    private string deathSfx = "EnemyDeath";

    private void OnEnable()
    {
        health = GetComponent<Health>();

        health.OnTakeDamage += PlayDamageSfx;
        health.OnDie += PlayDeathSfx;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= PlayDamageSfx;
        health.OnDie -= PlayDeathSfx;
    }

    private void PlayDamageSfx(int damage)
    {
        AudioManager.Singleton.PlaySoundEffect(takeDamageSfx);
    }

    private void PlayDeathSfx()
    {
        AudioManager.Singleton.PlaySoundEffect(deathSfx);
    }
}
