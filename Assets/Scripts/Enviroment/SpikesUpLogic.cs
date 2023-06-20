using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesUpLogic : MonoBehaviour
{
    public AudioSource audioSource;
    public float velocity;
    public Transform finalPosition;
    public bool end;

    public StartSpikesLogic startLogic;

    // Start is called before the first frame update
    void Start()
    {
        
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!end && startLogic.start)
        {
            audioSource.Play();
            Vector3 direction = finalPosition.transform.position - gameObject.transform.position;
            gameObject.transform.position += direction * velocity / 100;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpikesTrigger")
            end = true;
    }
}
