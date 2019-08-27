using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
   public static ParticleManager _Instance;

   public ParticleSystem[] ShotEffects;
   public ParticleSystem[] ExplosionEffects;

   public List<ParticleSystem> ShotList;
   public List<ParticleSystem> ExplosionList;
//    public ParticleSystem [] ShotEffects;
//    public ParticleSystem EnemyParticles;
//    public ParticleSystem tempParticle;
//    public ParticleSystem tempParticle2;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            ParticleSystem temp;
            temp = Instantiate(ShotEffects[PlayerPrefs.GetInt("GunIndex")]);
            ShotList.Add(temp);
        }

        for (int i = 0; i < 1; i++)
        {
            ParticleSystem temp;
            temp = Instantiate(ExplosionEffects[PlayerPrefs.GetInt("GunIndex")]);
           ExplosionList.Add(temp);
        }
       // Instantiate(ShotEffects[0]);
      //  ShotList.Add(ShotEffects[0]);
        //  tempParticle = Instantiate(ShotEffects[PlayerPrefs.GetInt("GunIndex")]);
        //    tempParticle2 = Instantiate(EnemyParticles);
        //    tempParticle.transform.position=new Vector3(10,10,0);
        //  tempParticle2.transform.position=new Vector3(10,10,0);
    }

    void Awake()
    {
        _Instance = this;
    }

    public ParticleSystem GetShotParticle()
    {
        ParticleSystem temp;
        for (int i = 0; i < ShotList.Count; i++)
        {
            if (!ShotList[i].isPlaying)
            {
                return ShotList[i];
            }
        }
        temp = Instantiate(ShotEffects[PlayerPrefs.GetInt("GunIndex")]);
        ShotList.Add(temp);
        return temp;
    }

    public ParticleSystem GetExplosionParticle()
    {
        ParticleSystem temp;
        for (int i = 0; i < ExplosionList.Count; i++)
        {
            if (!ExplosionList[i].isPlaying)
            {
                return ExplosionList[i];
            }
        }
        temp = Instantiate(ExplosionEffects[PlayerPrefs.GetInt("GunIndex")]);
        ExplosionList.Add(temp);
        return temp; 
    }
}
