using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEcho : MonoBehaviour
{
    Rigidbody rb;
    int lifes;
    float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.right * 200.0f);
        lifes = 2;
        lifeTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        lifes--;
        if (lifes == 0) Destroy(gameObject);
    }
}
