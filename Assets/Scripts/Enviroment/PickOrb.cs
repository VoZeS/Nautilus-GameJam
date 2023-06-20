using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOrb : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            player1.SendMessage("Upgrade");
            player2.SendMessage("Upgrade");
            gameObject.SetActive(false);
            // audio
        }
    }
}
