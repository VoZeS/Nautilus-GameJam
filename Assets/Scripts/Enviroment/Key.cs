using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class Key : MonoBehaviour
{
    Rigidbody rb;

    GameObject attachedTo;
    [SerializeField] float impactForce = 500.0f;
    [SerializeField] float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attachedTo = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedTo == null) return;

        transform.position = Vector3.Lerp(transform.position, attachedTo.transform.position, followSpeed * Time.deltaTime);
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
        }
    }

    public void UseKey(GameObject go)
    {
        // sound
        attachedTo = go;
        followSpeed = followSpeed * 5.0f;
    }
}
