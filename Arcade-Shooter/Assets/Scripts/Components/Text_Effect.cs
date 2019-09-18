using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text_Effect : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DelayForDeactivate(gameObject));
    }

    private void Update()
    {
        var a=  GetComponent<RectTransform>();
      a.transform.Translate(0,0.06f,0);
    }

    private IEnumerator DelayForDeactivate(GameObject gameObject)
    {
        yield return new WaitForSeconds(4);
        gameObject.SetActive(false);
    }
}
