using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesUpLogic : MonoBehaviour
{
    public AudioSource audioSource;
    public float velocity;
    public Transform initialPosition;
    public Transform finalPosition;
    public bool end;

    public GameObject elevatorColl;
    public GameObject elevatorRB;
    public GameObject realElevator;
    public Transform elevatorInitialPosition;
    public Transform elevatorRBInitialPosition;
    public Transform elevatorRealInitialPosition;

    public StartSpikesLogic startLogic;
    public LeverLogic leverLogic;
    public ElevatorLogic elevatorLogic;

    // Start is called before the first frame update
    void Start()
    {
        
        end = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(!end && startLogic.start)
        {
            audioSource.Play();
            Vector3 direction = finalPosition.transform.position - initialPosition.transform.position;
            gameObject.transform.position += direction * velocity / 100;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpikesTrigger")
            end = true;
    }

    public void FadeInFinish()
    {
        end = false;
        startLogic.start = false;
        leverLogic.isLeverActivated = false;
        leverLogic.animatorLever.SetBool("isActivated", false);
        elevatorLogic.going = true;
        startLogic.chispas1.Stop();
        startLogic.chispas2.Stop();
        startLogic.chispas3.Stop();
        startLogic.chispas4.Stop();

        realElevator.transform.position = elevatorRealInitialPosition.position;
        elevatorRB.transform.position = elevatorRBInitialPosition.position;
        elevatorColl.transform.position = elevatorInitialPosition.position;

        gameObject.transform.position = initialPosition.position;
    }
}
