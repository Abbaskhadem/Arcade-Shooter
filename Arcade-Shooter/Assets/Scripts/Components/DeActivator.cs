using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActivator : MonoBehaviour
{
    [SerializeField] private GameObject ActiveText;
    public void DeActivate()
    {
        gameObject.SetActive(false);
    }

    public void WaveText()
    {
        ActiveText.SetActive(true);
    }
}
