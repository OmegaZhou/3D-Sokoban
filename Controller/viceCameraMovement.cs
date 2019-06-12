using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viceCameraMovement : MonoBehaviour
{
    public float sensitivity = 5.0f;
    private Vector3 centerPosition;

    // Start is called before the first frame update
    void Start()
    {
        centerPosition = new Vector3(5.0f, 5.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float rotationX = Input.GetAxis("Mouse X") * sensitivity;
            print("X move is " + Input.GetAxis("Mouse X"));
            /*transform.eulerAngles += Vector3.up * rotationX;
            transform.position = new Vector3(centerPosition.x + Mathf.Sin(transform.eulerAngles.y), centerPosition.y, centerPosition.z + Mathf.Cos(transform.eulerAngles.y));*/
            transform.RotateAround(centerPosition, Vector3.up, rotationX);
        }
    }
}