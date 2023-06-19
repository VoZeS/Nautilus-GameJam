using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOrb : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player1.SendMessage("Upgrade");
            player2.SendMessage("Upgrade");
            gameObject.SetActive(false);
            // audio
        }
    }
}
