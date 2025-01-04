using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orieantation;

    public float xRotation;
    public float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //getting the mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //this is just how Unity handles rotations I dunno its meaning
        xRotation -= mouseY;
        yRotation += mouseX;

        //make sure player cannot look up and down more than 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotating cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orieantation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
