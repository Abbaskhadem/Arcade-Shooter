using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHole : MonoBehaviour
{
    private ParticleSystem Particle;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Particle.emission.rateOverTime.constant <= 150 && Timer>1.5f)
        {
            var emission = Particle.emission;
            emission.rateOverTime = 40f;
        }
    }
}
