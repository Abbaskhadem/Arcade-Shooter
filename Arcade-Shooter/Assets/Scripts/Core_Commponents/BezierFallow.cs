using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFallow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;
    private int RouteToGo;
    private float tparam;
    private Vector2 Enemyposition;
    private float SpeedModifier;
    private bool coroutineAllowed;
    void Start()
    {
        RouteToGo = 0;
        tparam = 0;
        SpeedModifier = 0.5f;
        coroutineAllowed = true;
        
    }
    void Update()
    {
        //if Allowed It Goes On The Routes
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(RouteToGo));
    }
    private IEnumerator GoByTheRoute(int RouteNumber)
    {
        coroutineAllowed = false;
        Vector2 p0 = routes[RouteNumber].GetChild(0).position;
        Vector2 p1 = routes[RouteNumber].GetChild(1).position;
        Vector2 p2 = routes[RouteNumber].GetChild(2).position;
        Vector2 p3 = routes[RouteNumber].GetChild(3).position;
        while (tparam < 1)
        {
            tparam += Time.deltaTime * SpeedModifier;
            Enemyposition = Mathf.Pow(1 - tparam, 3) * p0 +
                3 * Mathf.Pow(1 - tparam, 2) * tparam * p1 +
                3 * (1 - tparam) * Mathf.Pow(tparam, 2) * p2 +
                Mathf.Pow(tparam, 3) * p3;
            transform.position = Enemyposition;
            yield return new WaitForEndOfFrame();

        }
        tparam = 0f;
        RouteToGo += 1;
        if (RouteToGo > routes.Length - 1)
            RouteToGo = 0;
        coroutineAllowed = true;
    }
}
