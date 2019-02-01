using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBurst : MonoBehaviour
{
    ParticleSystem psystem;

    public int particleCount;

    public GameObject trigger;

    public Gradient fireGradient;

    public Gradient iceGradient;

    // Start is called before the first frame update
    void Start()
    {
        psystem = GetComponent<ParticleSystem>();
    }

    public void Burst(Vector2 position)
    {
        psystem.transform.position = position;
        psystem.Emit(particleCount);
        Instantiate(trigger, position, transform.rotation);
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SetColorOverLifetime(fireGradient);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SetColorOverLifetime(iceGradient);
        }
    }

    void SetColorOverLifetime(Gradient gradient)
    {
        var colorModule = psystem.colorOverLifetime;
        colorModule.color = gradient;
    }
}
