using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpikesLogic : MonoBehaviour
{
    public bool start;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            start = true;
    }
}
