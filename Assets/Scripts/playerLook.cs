using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class playerLook : MonoBehaviour
{

    public Camera playerCamera;
    public float mouseSensitivity = 3f;
    Transform tr;
    float xRotation = 0f;
    Inventory inventory;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        tr = gameObject.GetComponent<Transform>();
        inventory = gameObject.GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.GetComponent<Transform>().localRotation = Quaternion.Euler(xRotation, 0, 0);
        tr.Rotate(0, mouseX, 0);
        if (Input.GetKeyDown("e"))
        {
            GameEvents.instance.ToolPickUp();
        }
    }
}
