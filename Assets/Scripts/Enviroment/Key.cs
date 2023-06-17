using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Rigidbody rb;

    GameObject attachedTo;
    [SerializeField] float respectDistance = 0.5f;
    [SerializeField] float atractDistance = 2.0f;
    [SerializeField] float atractForce = 500.0f;
    [SerializeField] float impactForce = 500.0f;
    [SerializeField] float atractOffset = 0.3f;
    bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attachedTo = null;
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedTo == null) return;

        if (Vector3.Distance(attachedTo.transform.position, transform.position) < respectDistance)
        {
            rb.velocity = Vector3.zero;
        }
        else if (!waiting && Vector3.Distance(attachedTo.transform.position, transform.position) > atractDistance)
        {
            Vector3 direction = (attachedTo.transform.position - transform.position).normalized;
            direction.y += Random.Range(-atractOffset, atractOffset);
            rb.AddForce(direction * atractForce);
            waiting = true;
            Invoke("WaitFinished", 0.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (attachedTo != null) return;

        if (collision.gameObject.CompareTag("EchoProjectile"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 direction = (contact.point - transform.position).normalized;
            rb.AddForce(direction * impactForce);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            attachedTo = collision.gameObject;
        }
    }

    void WaitFinished()
    {
        waiting = false;
    }
}
