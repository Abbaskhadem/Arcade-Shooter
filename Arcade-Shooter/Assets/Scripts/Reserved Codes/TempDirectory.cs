using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDirectory : MonoBehaviour
{
    [SerializeField]
    public GameObject[] Objects;
    int i;
    float Timer;

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 0.6f && i<Objects.Length)
        {
            Timer = 0;
            Objects[i].SetActive(true);
            i++;
        }
        
    }
}
