using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticElevatorLogic : MonoBehaviour
{
    public float velocity;
    public Transform initialPosition;
    public Transform finalPosition;

    public GameObject realElevator;

    private bool going = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ElevatorCollider")
            going = !going;

    }
}