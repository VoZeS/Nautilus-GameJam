using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorEchoes : MonoBehaviour
{
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Invisible"))
        {
            other.GetComponent<InvisibleObject>().FadeIn();
        }
    }
}
