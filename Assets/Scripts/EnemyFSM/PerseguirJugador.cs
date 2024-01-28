using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerseguirJugador : MonoBehaviour
{
    public Transform jugador; // El objeto que el enemigo persigue (el jugador).
    private Animator animator;
    public float fov_distance = 10f;   
    public float attackRange = 2f;
    [Range(0.0f,0.5f)] public float damagePercentage = 0.1f;
    [SerializeField] private bool hasHurt = false, start = true;
    public GameObject waypoints;
    List<GameObject> patrol_point_s = new List<GameObject>();
    private NavMeshAgent agent;
    [Range(0.01f,0.99f)] public float probabilidadSearchRandom = 0.15f;
    [Range(0,270)]public float rot_left, rot_right;
    public float rot_speed = 0.5f;
    bool end_rot_left = false, end_rot_right = false;
    float obj_left, obj_right = new float();
    Vector3 add_left; // lo que hay que sumarles
    Vector3 add_right;
    [Range(0f,20f)]public float knockbackForce = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        foreach(Transform child in waypoints.transform){
            patrol_point_s.Add(child.gameObject);
        }
    }

    void FixedUpdate()
    {
        // Verificar si el jugador (objetivo) y el agente existen.
        if (jugador != null && agent != null)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Patrol")) {
                Vector3 target = new Vector3();

                if(start) {
                    target = patrol_point_s[2].transform.position;
                    //agent.transform.LookAt(new Vector3(target.x, target.y-transform.position.y, target.z));
                    agent.SetDestination(target);
                    start = false;
                }         
                
                if(agent.remainingDistance < agent.stoppingDistance) {
                    target = patrol_point_s[ (Random.Range (0, patrol_point_s.Count))].transform.position;
                    //agent.transform.LookAt(new Vector3(target.x, target.y-transform.position.y, target.z));
                    agent.SetDestination(target);
                }
            } 
            
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Search") && Random.Range(0f, 100f) >= 100f-probabilidadSearchRandom) {
                animator.SetBool("PlayerOnSight", false);
                animator.SetBool("Random_search_action", true);
            }

            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Search")){
                agent.SetDestination(transform.position);
                SetUpRotation();
                if(end_rot_left == false) { //(Quaternion.Angle(transform.rotation, obj_left) > 0)

                    transform.rotation *= Quaternion.AngleAxis(rot_speed, Vector3.up); 
                    if (transform.eulerAngles.y > obj_left) { end_rot_left = true; }
                } else if(end_rot_right == false) {

                    transform.rotation *= Quaternion.AngleAxis((rot_speed * -1), Vector3.up);
                    if (transform.eulerAngles.y < obj_right) { end_rot_right = true; }
                } else {
                    end_rot_left = false;
                    end_rot_right = false;
                    animator.SetBool("Random_search_action", false);
                }
            }
            
            CheckCollisions();
        }
    }

    void CheckCollisions(){
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
            //Use the OverlapBox to detect if there are any other colliders within this box area.
            //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
            Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + transform.forward * fov_distance*3/4, new Vector3(1.25f,1f,1.5f)*fov_distance/2f, transform.rotation, LayerMask.GetMask("Player"));
            
            //Check when there is a new collider coming into contact with the box
            if(hitColliders.Length > 0){
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.forward, out hit, (transform.position-jugador.position).magnitude, LayerMask.GetMask("Player"))){
                    // Configurar el destino del agente para que persiga al jugador.
                    agent.SetDestination(jugador.position);
                    transform.LookAt(new Vector3(jugador.position.x, jugador.position.y-transform.position.y, jugador.position.z));
                    animator.SetBool("PlayerOnSight", true);
                    if((transform.position-jugador.position).magnitude < 1.5f){
                        agent.SetDestination(transform.position);
                        animator.SetBool("OnAttackRange", true);        
                    } 
                }
            } else {
                animator.SetBool("PlayerOnSight", false);
                animator.SetBool("OnAttackRange", false);                    
            }
        } else {
            
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
                hasHurt = false;
                animator.SetBool("OnAttackRange", false);
            }

            if (!hasHurt){
                Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + transform.forward * attackRange*3/4, new Vector3(1, 1f, 1f)*attackRange/2f, transform.rotation, LayerMask.GetMask("Player"));
                
                //Check when there is a new collider coming into contact with the box
                if(hitColliders.Length > 0){
                    jugador.GetComponent<Controller>().Hurt(damagePercentage, knockbackForce);
                    hasHurt = true;
                }
            }
        }
    }

    void SetUpRotation(){
        add_left = new Vector3(0f,rot_left,0f); // lo que hay que sumarles
        add_right = new Vector3(0f,rot_right,0f);
        

        obj_left = transform.eulerAngles.y + rot_left; // angulo de inicio
        //obj_right = transform.eulerAngles.y - rot_right;
        obj_right = obj_left - rot_right;

        if (obj_left < 0) {obj_left = 360 + obj_left; }
        else if (obj_left > 360) {obj_left = obj_left - 360; }
        
        if (obj_right < 0) {obj_right = 360 + obj_right; }
        else if (obj_right > 360) {obj_right = obj_right - 360; }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (true)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(gameObject.transform.position + transform.forward * fov_distance*3/4, new Vector3(1.25f,1f,1.5f)*fov_distance);
    }

}

