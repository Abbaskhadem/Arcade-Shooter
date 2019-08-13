using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager _Instance;
    public ParticleSystem PlayerParticles;
    public ParticleSystem EnemyParticles;
    public ParticleSystem tempParticle;
    public ParticleSystem tempParticle2;
    void Start()
    {
        tempParticle = Instantiate(PlayerParticles);
        tempParticle2 = Instantiate(EnemyParticles);
        tempParticle.transform.position=new Vector3(10,10,0);
        tempParticle2.transform.position=new Vector3(10,10,0);
    }

    void Awake()
    {
        _Instance = this;
    }
}
