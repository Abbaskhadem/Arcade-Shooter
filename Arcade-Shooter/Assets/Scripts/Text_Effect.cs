using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_Effect : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(0, 0.03f, 0);
        Destroy(gameObject,2f);
    }
}
