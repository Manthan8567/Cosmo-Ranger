using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public ParticleSystem swordSlash3; // Assign particle system here
    public ParticleSystem swordSlash2;
    public ParticleSystem swordSlash1;

    public void PlaySwordSlash3()
    {
        swordSlash3.Play();
    }
    public void PlaySwordSlash2()
    {
        swordSlash2.Play();
    }
    public void PlaySwordSlash1()
    {
        swordSlash1.Play();
    }
}
