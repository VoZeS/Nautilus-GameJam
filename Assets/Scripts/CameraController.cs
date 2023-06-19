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
    public float limitZ = -8.0f;

    Vector3 gizmosPos;

    private Vector3 distancePlayers;

    //public LevelVolume levelVolume;
    public float levelInd;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraStartDelay());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(levelInd);

        switch (levelInd)
        {
            case 1.0f:
                offset = new Vector3(0.0f, 3.0f, -4.0f);
                limitZ = -8.0f;
                break;
            case 2.0f:
                offset = new Vector3(0.0f, 3.0f, -6.0f);
                limitZ = -8.0f;
                break;
            case 2.5f:
                offset = new Vector3(0.0f, 3.0f, -8.0f);
                limitZ = -8.0f;
                break;
            case 3.0f:
                offset = new Vector3(0.0f, 3.0f, -6.0f);
                limitZ = -8.0f;
                break;
            case 4.0f:
                offset = new Vector3(0.0f, 6.0f, -8.0f);
                limitZ = -8.0f;
                break;
            case 5.0f:
                offset = new Vector3(0.0f, 3.0f, -6.0f);
                limitZ = -8.0f;
                break;
            default:
                offset = new Vector3(0.0f, 3.0f, -4.0f);
                limitZ = -8.0f;
                break;
        }

        if (playerOne != null && playerTwo != null)
        {
            // Zoom Out Camera
            distancePlayers = playerOne.transform.position - playerTwo.transform.position;

            if (transform.position.z <= limitZ)
                transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, limitZ), transform.rotation);

            Vector3 desiredPosition = FindCentroid() + offset + new Vector3(0.0f, 0.0f, -distancePlayers.magnitude / magnitude);
            Vector3 smoothedPostion = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPostion;
            gizmosPos = FindCentroid();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level1")
            levelInd = 1.0f;

        if (other.tag == "Level2")
            levelInd = 2.0f;
        
        if (other.tag == "Level2.5")
            levelInd = 2.5f;

        if (other.tag == "Level3")
            levelInd = 3.0f;

        if (other.tag == "Level4")
            levelInd = 4.0f;

        if (other.tag == "Level5")
            levelInd = 5.0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Level2.5")
            levelInd = 2.0f;
    }
}
