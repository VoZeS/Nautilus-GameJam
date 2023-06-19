using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelVolume : MonoBehaviour
{
    [SerializeField] VolumeProfile profile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            GlobalVolume.instance.ChangeProfile(profile);
        }
    }
}
