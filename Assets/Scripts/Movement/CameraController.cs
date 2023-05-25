using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2 newDir = Vector2.zero;
    [SerializeField] float cameraSpeed = 5f;
    [SerializeField] Transform FullPivot;
    [SerializeField] Transform yPivot;
    float xValue = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void OnEnable()
    {
        InputManager.lookAction += RotateCamera;
    }
    void RotateCamera(Vector2 dir)
    {
        newDir += dir;
        
        newDir.y = Mathf.Clamp(newDir.y, -39f, 39f);
        FullPivot.rotation = Quaternion.Euler(xValue, newDir.x * cameraSpeed, 0f);
        yPivot.rotation = Quaternion.Euler(0f, newDir.x * cameraSpeed, 0f);

    }
    private void Update()
    {
        xValue = Mathf.Clamp(newDir.y * cameraSpeed, -40f, 40f);
    }

    void OnDisable()
    {
        InputManager.lookAction -= RotateCamera;
    }
}
