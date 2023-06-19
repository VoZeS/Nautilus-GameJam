using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerOne;
    public Transform playerTwo;

    public Vector3 offset;
    public float smoothSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraStartDelay());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerOne != null)
        {
            Vector3 desiredPosition = playerOne.transform.position + offset;
            Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPostion;
        }
    }

    IEnumerator CameraStartDelay()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
