using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPlayersOnCollider : MonoBehaviour
{
    [NonEditable]public int playersOnCollider;

    // Start is called before the first frame update
    void Start()
    {
        playersOnCollider = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playersOnCollider++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playersOnCollider--;
    }
}
