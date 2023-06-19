using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "DieCollider")
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
        else if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.GetComponent<Key>().RespawnKey();
        }
    }
}
