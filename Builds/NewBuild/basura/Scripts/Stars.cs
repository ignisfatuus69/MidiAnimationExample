using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem stars;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleStars()
    {
        if (stars.isPlaying)
        {
            stars.Stop();
        }
        else
        {
            stars.Play();
        }
    }
}
