using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class Key : MonoBehaviour
{

    AudioSource audioSource;

    Rigidbody rb;

    [SerializeField] GameObject attachedTo;
    bool isDoor = false;
    [SerializeField] float impactForce = 500.0f;
    [SerializeField] float followSpeed;
    [SerializeField] Transform respawnTransform;

    public AudioClip grab;
    public AudioClip insert;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        attachedTo = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedTo == null) return;

        transform.position = Vector3.Lerp(transform.position, attachedTo.transform.position, followSpeed * Time.deltaTime);

        if (isDoor && Vector3.Distance(transform.position, attachedTo.transform.position) < 0.05f)
        {
            attachedTo.transform.parent.GetComponent<KeyDoor>().KeyOn();
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (attachedTo != null) return;

        if (collision.gameObject.CompareTag("EchoProjectile"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 direction = (transform.position - contact.point).normalized;
            rb.AddForce(direction * impactForce);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().keyAttached = this;
            attachedTo = collision.transform.Find("KeyPoint").gameObject;
            GetComponent<Collider>().enabled = false;
            rb.useGravity = false;

            audioSource.clip = grab;
            audioSource.Play();
        }
    }

    public void UseKey(GameObject go)
    {
        // sound
        attachedTo = go.transform.Find("KeyPoint").gameObject;
        isDoor = true;
        followSpeed = followSpeed * 5.0f;

        audioSource.clip = insert;
        audioSource.Play();
    }

    public void RespawnKey()
    {
        attachedTo = null;
        GetComponent<Collider>().enabled = true;
        rb.useGravity = true;
        transform.position = respawnTransform.position;
        rb.velocity = Vector3.zero;
    }
}
