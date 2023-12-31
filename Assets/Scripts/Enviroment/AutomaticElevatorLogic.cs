using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticElevatorLogic : MonoBehaviour
{
    AudioSource m_AudioSource;
    public float velocity;
    public Transform initialPosition;
    public Transform finalPosition;

    public GameObject realElevator;

    private bool going = true;

    [SerializeField] Transform playerParent;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (going)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
            Vector3 direction = finalPosition.transform.position - initialPosition.transform.position;
            gameObject.transform.position += direction * 60 * Time.deltaTime * velocity / 100;
            realElevator.transform.position += direction * 60 * Time.deltaTime * velocity / 100;
        }
        else if (!going)
        {
            //m_AudioSource.Play();
            Vector3 direction = initialPosition.transform.position - finalPosition.transform.position;
            gameObject.transform.position += direction * 60 * Time.deltaTime * velocity / 100;
            realElevator.transform.position += direction * 60 * Time.deltaTime * velocity / 100;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ElevatorCollider")
            going = !going;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(playerParent);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
