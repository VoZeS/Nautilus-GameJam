using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLogic : MonoBehaviour
{
    public bool isLeverActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        isLeverActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EchoProjectile")
            isLeverActivated = true;
    }
}
