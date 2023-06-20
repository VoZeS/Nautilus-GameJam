using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorLogic : MonoBehaviour
{
    AudioSource audioSource;
    public float velocity;
    public Transform initialPosition;
    public Transform finalPosition;

    public GameObject realElevator;
    public LeverLogic lever;

    private bool going = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lever.isLeverActivated)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (going)
            {
                Vector3 direction = finalPosition.transform.position - initialPosition.transform.position;
                gameObject.transform.position += direction * velocity / 100;
                realElevator.transform.position += direction * velocity / 100;
            }
            else if (!going)
            {
                Vector3 direction = initialPosition.transform.position - finalPosition.transform.position;
                gameObject.transform.position += direction * velocity / 100;
                realElevator.transform.position += direction * velocity / 100;

            }
        }
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ElevatorCollider")
            going = !going;

    }
}
