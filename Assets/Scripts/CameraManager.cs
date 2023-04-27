using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject cameraResetPos;

    public Transform target; // The point or object to rotate around
    public float rotationSpeed = 5f; // The speed of the camera rotation
    public float distance = 10f; // The distance from the target
    public float zoomSpeed = 10f; // The distance from the target
    public float minDistance = 2; // The distance from the target
    public float maxDistance = 10; // The distance from the target

    private Vector2 mouseMovement;

    void Update()
    {
        if (target != null)
        {
            // Handle camera rotation
            if (Input.GetMouseButton(0))
            {
                mouseMovement.x = Input.GetAxis("Mouse X") * rotationSpeed;
                mouseMovement.y = Input.GetAxis("Mouse Y") * rotationSpeed;

                Vector3 eulerAngles = transform.eulerAngles;
                eulerAngles.z = 0f;
                eulerAngles += new Vector3(-mouseMovement.y, mouseMovement.x, 0);
                Quaternion camRotation = Quaternion.Euler(eulerAngles);
                transform.rotation = camRotation;
            }

            // Handle camera zoom
            float zoomAmount = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance - zoomAmount, minDistance, maxDistance);

            // Update camera position
            transform.position = target.position - (transform.rotation * Vector3.forward * distance);
        }
    }

    public void ResetCamera()
    {
        target = null;
        transform.position = cameraResetPos.transform.position;
        transform.rotation = cameraResetPos.transform.rotation;
    }
}
