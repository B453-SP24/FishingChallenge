using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Boat Specs")]
    [Tooltip("Power of the boat engine in Newtons.")]
    [SerializeField] float enginePower = 10;
    [Tooltip("Turning torque in Newtons.")]
    [SerializeField] float turningTorque = 10;
    private Rigidbody rb; // Rigidbody reference.

    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI depthText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(rb.position, Vector3.down, out hit, 300f))
        {
            depthText.text = "Depth: " + Mathf.RoundToInt(hit.distance) + " meters";
        }
        else
        {
            depthText.text = "Depth: 300+ meters";
        }

        speedText.text = Mathf.RoundToInt(rb.velocity.magnitude * 1.944f) + " knots";
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.forward * enginePower);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-Vector3.forward * enginePower);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeTorque(-Vector3.up * turningTorque);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeTorque(Vector3.up * turningTorque);
        }

        if (rb.velocity.magnitude > 25.722)
        {
            rb.velocity = rb.velocity.normalized * 25.722f;
        }
    }
}