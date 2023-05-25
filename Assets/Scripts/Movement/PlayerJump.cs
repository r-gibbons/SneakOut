using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public static PlayerJump Instance;
    [SerializeField] float jumpHeight = 5;
    [SerializeField] float distance = 1.2f;
    [SerializeField] string groundTag = "Ground";
    Rigidbody rig;
    public bool grounded = true;
    void Awake()
    {
        if (!Instance)
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
        InputManager.jumpAction += Jump;
    }

    void Jump()
    {
        if (grounded)
        {
            rig.AddForce(transform.up * jumpHeight, ForceMode.Force);
        }
    }

    void Update()
    {
        if(Physics.Raycast(transform.position,-transform.up,out RaycastHit hit, distance))
        {
            if (hit.collider.gameObject.tag.Equals(groundTag))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
        else
        {
            grounded = false;
        }
    }
}
