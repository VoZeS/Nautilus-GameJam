using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FadeManager.instance.FadeIn();
        }
    }
}
