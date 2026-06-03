using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {
    
    [Header("Camera Tracking")]
    [SerializeField] private float trackingSpeed;
    [SerializeField] private Vector2 offset;
    [Header("Camera Bounds")]
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    [SerializeField] private Transform camTarget;

    void FixedUpdate() {
        if (camTarget != null) {
            Vector2 trackingPosition = camTarget.position + (Vector3)offset;
            var newPos = Vector2.Lerp(transform.position, trackingPosition, Time.deltaTime * trackingSpeed);
            var camPosition = new Vector3(newPos.x, newPos.y, -10f);
            var v3 = camPosition;
            var clampX = Mathf.Clamp(v3.x, minX, maxX);
            var clampY = Mathf.Clamp(v3.y, minY, maxY);
            transform.position = new Vector3(clampX, clampY, -10f);
        }
    }

    public void UpdateCameraBounds(float newMinX, float newMinY, float newMaxX, float newMaxY)
    {
        minX = newMinX;
        minY = newMinY;
        maxX = newMaxX;
        maxY = newMaxY;
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector3 topLeft = new Vector3(minX, maxY, 0f);
        Vector3 topRight = new Vector3(maxX, maxY, 0f);
        Vector3 bottomLeft = new Vector3(minX, minY, 0f);
        Vector3 bottomRight = new Vector3(maxX, minY, 0f);
        
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);

        Camera cameraComponent = GetComponent<Camera>();

        float verticalExtent = cameraComponent.orthographicSize;
        float horizontalExtent = verticalExtent * cameraComponent.aspect;

        Gizmos.color = Color.blue;

        // Expand the camera movement limits by the visible camera extents
        topLeft = new Vector3(
            minX - horizontalExtent,
            maxY + verticalExtent,
            0f
        );

        topRight = new Vector3(
            maxX + horizontalExtent,
            maxY + verticalExtent,
            0f
        );

        bottomLeft = new Vector3(
            minX - horizontalExtent,
            minY - verticalExtent,
            0f
        );

        bottomRight = new Vector3(
            maxX + horizontalExtent,
            minY - verticalExtent,
            0f
        );

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
