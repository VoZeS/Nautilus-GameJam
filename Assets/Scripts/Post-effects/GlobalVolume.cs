using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlobalVolume : MonoBehaviour
{
    public static GlobalVolume instance;
    [SerializeField] Volume currentVolume;
    [SerializeField] Volume nextVolume;
    [SerializeField] float blendSpeed;
    bool blending;
    float blendProccess;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentVolume.weight = 1.0f;
        nextVolume.weight = 0.0f;

        blending = false;
    }

    public void ChangeProfile(VolumeProfile profile)
    {
        if (!blending)
        {
            nextVolume.profile = profile;
            blendProccess = 0.0f;
            StartCoroutine("BlendProfiles");
        }
        else
        {
            VolumeProfile aux = currentVolume.profile;
            currentVolume.profile = nextVolume.profile;
            nextVolume.profile = aux;
            currentVolume.weight = blendProccess;
            nextVolume.weight = 1.0f - blendProccess;
            blendProccess = 1.0f - blendProccess;
            StopAllCoroutines();
            StartCoroutine("BlendProfiles");
        }
        Debug.Log("Current volume:"+ currentVolume);
    }


    IEnumerator BlendProfiles()
    {
        blending = true;
        while (blendProccess < 1.0f)
        {
            currentVolume.weight -= blendSpeed * Time.deltaTime;
            nextVolume.weight += blendSpeed * Time.deltaTime;
            blendProccess += blendSpeed * Time.deltaTime;
            yield return null;
        }
        currentVolume.profile = nextVolume.profile;
        currentVolume.weight = 1.0f;
        nextVolume.weight = 0.0f;
        blending = false;
    }
}
