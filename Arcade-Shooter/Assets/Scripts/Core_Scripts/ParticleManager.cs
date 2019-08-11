using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager _Instance;
    public ParticleSystem PlayerParticles;
    public ParticleSystem tempParticle;
    void Start()
    {
        tempParticle = Instantiate(PlayerParticles);
        tempParticle.transform.position=new Vector3(10,10,0);
    }

    void Awake()
    {
        _Instance = this;
    }
}
