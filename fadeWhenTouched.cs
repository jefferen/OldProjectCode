using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeWhenTouched : MonoBehaviour
{
    [Range(1f, 5f)]
    public float lifespan;

    private ParticleSystem.Particle[] particles;
    private ParticleSystem system;
    private float time;

    public bool trigger;

    void Awake()
    {
        system = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(trigger == true)
        {
            time += Time.deltaTime;
            //time += 0.05f;
            if (!system.isPaused)
            {
               // system.Pause();

                Invoke("callme", 0.2f);

                particles = new ParticleSystem.Particle[system.particleCount];
                system.GetParticles(particles);
            }

            if (particles != null)
            {
                for (int p = 0; p < particles.Length; p++)
                {
                    Color color = particles[p].startColor;
                    color.a = ((lifespan - time));

                    particles[p].startColor = color;
                }

                system.SetParticles(particles, particles.Length);
                if(particles.Length <= 240)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void callme()
    {
        var em = system.emission;
        em.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        trigger = true;
    }
}