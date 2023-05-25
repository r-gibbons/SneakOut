using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public static MovePlayer Instance;
    [SerializeField] Transform cam;
    [SerializeField] float speedMultipler = 1;

    Rigidbody rig;
    Vector2 dir = Vector2.zero;
    Vector3 cameraForward = Vector3.zero;
    Vector3 cameraRight = Vector3.zero;

    float speed = 1000f;
    public bool isSprinting = false;
    public bool isMoving = false;

    void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rig = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        InputManager.moveAction += Move;
        InputManager.sprintAction += Sprint;
    }

    void Sprint()
    {
        if (isSprinting)
        {
            isSprinting = false;
            speedMultipler = 1f;
            
        }
        else
        {
            isSprinting = true;  
            speedMultipler = 1.5f;
        }
    }

    void Move(Vector2 dir)
    {
        this.dir = dir;

        if(this.dir == Vector2.zero)
        {
            isMoving = false;
        }
        else if(this.dir != Vector2.zero)
        {
            isMoving = true;
        }
    }

    void FixedUpdate()
    {
        cameraForward = new Vector3(cam.forward.x,0,cam.forward.z);
        cameraRight = new Vector3(cam.right.x,0,cam.right.z);
        rig.AddForce((speed * speedMultipler) * Time.fixedDeltaTime * (cameraForward * dir.y + cameraRight * dir.x), ForceMode.Force);
    }

    void OnDisable()
    {
        InputManager.moveAction -= Move;
        InputManager.sprintAction -= Sprint;
    }
}
