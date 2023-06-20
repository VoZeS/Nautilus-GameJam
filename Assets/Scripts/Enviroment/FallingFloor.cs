using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    [SerializeField] GameObject orb;
    [SerializeField] Transform endTransform;
    [SerializeField] float speed;
    [NonEditable][SerializeField] int playersOnZone;
    bool triggered;
    bool hasSounded;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playersOnZone = 0;
        hasSounded = false;
        triggered = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered || orb.activeSelf) return;
        
        if (Vector3.Distance(transform.position, endTransform.position) > 0.1f)
        {
            transform.position = (endTransform.position - transform.position).normalized * speed;
            if (!hasSounded)
            {
                audioSource.Play();
                hasSounded = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersOnZone++;
            if (playersOnZone == 2) triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersOnZone--;
        }
    }
}
