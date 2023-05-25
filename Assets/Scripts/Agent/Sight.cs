using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Sight : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] float speed = 1f;

    [SerializeField] AgentMoveManager moveManager;
    Transform currentlySeen;
    Vector3 dir;
    float distance;
    bool insideVision = false;
    public bool seen = false;

   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(playerTag))
        {
            insideVision = true;
            currentlySeen = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(playerTag))
        {
            currentlySeen = null;
            insideVision = false;
            seen = false;
        }
    }
    void FixedUpdate()
    {
        if (insideVision)
        {
            dir = (currentlySeen.position - transform.parent.position).normalized;
            distance = Vector3.Distance(transform.parent.position, currentlySeen.position);
            Debug.DrawRay(transform.parent.position, dir * distance, Color.black);
            if (Physics.Raycast(transform.parent.position, dir*distance, out RaycastHit hit))
            {
                if (hit.transform.tag.Equals(playerTag))
                {
                    seen = true;
                    moveManager.moveAgent(hit.transform);
                }
                else
                {
                    seen = false;
                }
            }
            else
            {
                seen = false;
            }
        }
    }
}
