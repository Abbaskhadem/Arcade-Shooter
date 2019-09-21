using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePost : MonoBehaviour
{
    public Material RippleMaterial;
    public float MaxAmount = 50f;
 
    [Range(0,1)]
    public float Friction = .9f;

    private float Timer;
    private float Amount = 0f;
 
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 2)
        {
            Timer = 0;
            this.enabled = false;
        }
        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    public void RippleEffect(Transform Object)
    {
        this.Amount = this.MaxAmount;
        this.RippleMaterial.SetFloat("_CenterX", Object.transform.position.x);
        this.RippleMaterial.SetFloat("_CenterY", Object.transform.position.y);  
    }
    void OnRenderImage(RenderTexture a, RenderTexture b)
    {
        
        Graphics.Blit(a, b, this.RippleMaterial);
    }
}
