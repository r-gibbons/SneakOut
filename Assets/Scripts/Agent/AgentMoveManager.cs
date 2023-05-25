using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AgentMoveManager : MonoBehaviour
{
   // public static AgentMoveManager instance;
    NavMeshAgent agent;
    Coroutine routineLaround = null;
    Transform playerPos;
    bool moving = false;
    void Awake()
    {
/*        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }*/
        agent = GetComponent<NavMeshAgent>();
    }
    public void moveAgent(Transform playerPos)
    {
        if (routineLaround != null) { StopCoroutine(routineLaround); }
        agent.SetDestination(playerPos.position);
        moving = true;
    }
    void Update()
    {
        if (moving)
        {
            if (agent.remainingDistance < .1f )
            {
                moving = false;
                routineLaround = StartCoroutine(nameof(lookAround));
            }
        }
    }
    IEnumerator lookAround()
    {
        LeanTween.rotateAround(gameObject,transform.up,transform.rotation.y +90f, 1.5f);
        yield return new WaitForSeconds(2f);
        LeanTween.rotateAround(gameObject, transform.up, transform.rotation.y - 180f, 1.5f);
        yield return new WaitForSeconds(2f);
    }
}
