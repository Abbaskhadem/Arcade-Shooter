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
   ParticleSystem temp2;
//    public ParticleSystem [] ShotEffects;
//    public ParticleSystem EnemyParticles;
//    public ParticleSystem tempParticle;
//    public ParticleSystem tempParticle2;
    void Start()
    {
        for (int j = 0; j < ExplosionEffects.Length; j++)
        {
            for (int k = 0; k < 1; k++)
            {
                ParticleSystem temp;
                temp = Instantiate(ExplosionEffects[j]);
                ExplosionList.Add(temp);
            }  
        }
        for (int i = 0; i < 3; i++)
        {
            ParticleSystem temp;
            temp = Instantiate(ShotEffects[PlayerPrefs.GetInt("GunIndex")]);
            ShotList.Add(temp);
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

    public ParticleSystem GetExplosionParticle(string ExName)
    {

        for (int i = 0; i < ExplosionList.Count; i++)
        {
            if (!ExplosionList[i].isPlaying)
            {
                if(ExplosionList[i].CompareTag(ExName))
                return ExplosionList[i];
            }
        }

        for (int i = 0; i < ExplosionEffects.Length; i++)
        {
            Debug.Log("NEWITEM!");
            if (ExplosionEffects[i].CompareTag(ExName))
            {
                temp2 =  Instantiate(ExplosionEffects[i]);
            }
        }
        Instantiate(temp2);
        ExplosionList.Add(temp2);
        return temp2; 
    }
}
