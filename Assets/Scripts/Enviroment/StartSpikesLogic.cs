using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpikesLogic : MonoBehaviour
{
    public bool start;

    public ParticleSystem chispas1;
    public ParticleSystem chispas2;
    public ParticleSystem chispas3;
    public ParticleSystem chispas4;

    // Start is called before the first frame update
    void Start()
    {
        start = false;

        chispas1.Pause();
        chispas2.Pause();
        chispas3.Pause();
        chispas4.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            start = true;

            chispas1.Play();
            chispas2.Play();
            chispas3.Play();
            chispas4.Play();
        }
    }
}
