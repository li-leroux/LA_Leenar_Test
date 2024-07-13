using UnityEngine;

public class TestController : MonoBehaviour
{
    private bool isRotating = false;
    private float targetRotation = 0f;
    private float rotationSpeed = 360f; // Rotation speed in degrees per second
    private float moveSpeed = 4f; // Movement speed
    private float liftSpeed = 2f; // Lift speed
    private bool isLifting = false;

    private void Update()
    {
        // Rotation controls
        if (!isRotating)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                targetRotation += 90f;
                isRotating = true;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                targetRotation -= 90f;
                isRotating = true;
            }
        }

        // Movement controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Lift controls
        if (Input.GetKey(KeyCode.Space))
        {
            isLifting = true;
            transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            isLifting = true;
            transform.Translate(Vector3.down * liftSpeed * Time.deltaTime);
        }
        else
        {
            isLifting = false;
        }

        // Rotate the object around the Y-axis towards the target rotation
        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, targetRotation, 0f), step);

            // Check if we reached the target rotation
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, targetRotation, 0f)) < 0.01f)
            {
                transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
                isRotating = false;
            }
        }
        else
        {
            // Move the object based on WASD controls
            Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;
            transform.Translate(moveAmount);
        }
    }
}
