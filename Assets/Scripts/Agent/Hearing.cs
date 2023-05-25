using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hearing : MonoBehaviour
{
    [SerializeField] string soundTag = "PlayerSounds";
    [SerializeField] Sight sightScript;
    [SerializeField] AgentMoveManager moveManager;
    NavMeshAgent agent;
    Transform currentlyHeard;
    bool heard = false;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(soundTag))
        {
            heard = true;
            currentlyHeard = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(soundTag))
        {
            heard = false;
            currentlyHeard = null;
        }
    }
    void Update()
    {
        if (heard && !sightScript.seen)
            {
                moveManager.moveAgent(currentlyHeard);                
            }
    }
    void hasHeard()
    {
        
    }
}
