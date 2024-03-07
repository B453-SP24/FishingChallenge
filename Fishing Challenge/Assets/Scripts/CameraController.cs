using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform boatTransform; // Assign your boat's transform here in the Inspector
    public float distanceFromBoat = 50.0f; // The fixed distance from the boat
    public float currentAngleX = 0.0f; // Current horizontal angle
    public float currentAngleY = 20.0f; // Initial downward angle from the horizontal plane, adjust if needed
    public float sensitivityX = 4.0f; // Mouse sensitivity
    public float sensitivityY = 2.0f; // Mouse sensitivity

    void Update()
    {
        // Adjust the angle based on the mouse movement
        currentAngleX += Input.GetAxis("Mouse X") * sensitivityX;
        currentAngleY -= Input.GetAxis("Mouse Y") * sensitivityY;
        currentAngleY = Mathf.Clamp(currentAngleY, -89f, 89f); // Prevent flipping over

        // Convert spherical coordinates to Cartesian coordinates for the camera position
        Vector3 direction = new Vector3(
            Mathf.Cos(currentAngleY * Mathf.Deg2Rad) * Mathf.Sin(currentAngleX * Mathf.Deg2Rad),
            Mathf.Sin(currentAngleY * Mathf.Deg2Rad),
            Mathf.Cos(currentAngleY * Mathf.Deg2Rad) * Mathf.Cos(currentAngleX * Mathf.Deg2Rad)
        );

        // Set the camera's position and look at the boat
        transform.position = boatTransform.position + direction * distanceFromBoat;
        transform.LookAt(boatTransform.position);
    }
}