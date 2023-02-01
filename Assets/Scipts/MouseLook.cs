using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class MouseLook : MonoBehaviour
{
    [SerializeField] float sensitivity = StaticVariables.mouseSensitivity;
    [SerializeField] Transform PlayerBody;

    private float xRotation = 0f;
    

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
        


    }
}
