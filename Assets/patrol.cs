using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class patrol : StateMachineBehaviour
{
    public List<string> posible_patrol_points = new List<string>();
    //UnityEngine.AI.NavMeshAgent agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
    List<GameObject> patrol_point_s = new List<GameObject>();
    bool start = true;
   
    // miau
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrol_point_s = new List<GameObject>();
        GameObject new_go = new GameObject();
        string pre = "PatrolPoint_";

        foreach (string ppp in posible_patrol_points) {
            new_go = GameObject.Find((pre + ppp));
            if (new_go) {
                patrol_point_s.Add(new_go);
            } else {
                Debug.Log( ((pre + ppp) + " not found") );
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UnityEngine.AI.NavMeshAgent agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 target = new Vector3();
        if(start) {
            target = patrol_point_s[2].transform.position;
            agent.SetDestination(target);
            start = false;
        }         
        
        if(agent.remainingDistance < agent.stoppingDistance) {
            target = patrol_point_s[ (Random.Range (0, patrol_point_s.Count))].transform.position;
            agent.SetDestination(target);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
