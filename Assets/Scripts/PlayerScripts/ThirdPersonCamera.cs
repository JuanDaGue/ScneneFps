using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Camera offset from the player
    public float sensitivity = 5f; // Mouse sensitivity
    public float distance = 10f; // Distance between the camera and the player
    public float minYAngle = -20f; // Minimum vertical angle
    public float maxYAngle = 60f; // Maximum vertical angle

    private float currentX = 0f; // Current horizontal rotation
    private float currentY = 0f; // Current vertical rotation

    private void Start()
    {
        // Lock cursor to the game window and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get mouse input for rotation
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Clamp the vertical angle to prevent flipping
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
    }

    private void LateUpdate()
    {
        // Calculate camera position and rotation
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = player.position + rotation * direction + offset;

        // Always look at the player
        transform.LookAt(player.position + Vector3.up * offset.y);
    }
}
