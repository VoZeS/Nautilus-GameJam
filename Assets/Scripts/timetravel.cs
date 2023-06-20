using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timetravel : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource m_AudioSource;
    bool isPlaying = false;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (!m_AudioSource.isPlaying && !isPlaying)
            {
                m_AudioSource.Play();

                isPlaying = true;
            }
        }
    }
}
