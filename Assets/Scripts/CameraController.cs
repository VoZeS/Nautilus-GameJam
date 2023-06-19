using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerOne;
    public Transform playerTwo;

    public Vector3 offset;
    public float smoothSpeed = 0.2f;
    public float magnitude = 2.0f;

    Vector3 gizmosPos;

    private Vector3 distancePlayers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraStartDelay());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerOne != null && playerTwo != null)
        {
            //Vector3 desiredPosition = FindCentroid() + offset;
            //Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            //transform.position = smoothedPostion;
            //gizmosPos = FindCentroid();


            // Zoom Out Camera
            distancePlayers = playerOne.transform.position - playerTwo.transform.position;

            if (transform.position.z <= -8.0f)
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, -8.0f), transform.rotation);
            }

            //if (distancePlayers.magnitude >= 10.0f)
            {
                Vector3 desiredPosition = FindCentroid() + offset + new Vector3(0.0f, 0.0f, -distancePlayers.magnitude / magnitude );
                Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPostion;
                gizmosPos = FindCentroid();
            }

        }
    }

    IEnumerator CameraStartDelay()
    {
        yield return new WaitForSeconds(0.01f);
        transform.position = playerOne.transform.position + offset;
    }

    Vector3 FindCentroid()
    {
        var totalX = 0f;
        var totalY = 0f;
        var totalZ = 0f;

        totalX += playerOne.transform.position.x;
        totalY += playerOne.transform.position.y;
        totalZ += playerOne.transform.position.z;   
        
        totalX += playerTwo.transform.position.x;
        totalY += playerTwo.transform.position.y;
        totalZ += playerTwo.transform.position.z;

        var centerX = totalX / 2;
        var centerY = totalY / 2;
        var centerZ = totalZ / 2;

        return new Vector3(centerX, centerY, centerZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(gizmosPos, new Vector3(1, 1, 1));
    }
}
