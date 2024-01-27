using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class momevent : MonoBehaviour
{
    [SerializeField] float speed = 0;
    Rigidbody rb;
    Transform target;
    Vector3 moveDir;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target) {
            transform.position = Vector3.MoveTowards( transform.position, target.transform.position, speed * Time.deltaTime );
            transform.LookAt( target.transform.position, Vector3.up );
            /*
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = new Quaternion(angle, 0, 0, 0).normalized;
            moveDir = direction;
            */
        } else {
            Debug.Log("no target");
        }
    }

    void FixedUpdate()
    {
        if(target) {
            //rb.velocity = new Vector3(moveDir.x, moveDir.y) * speed;
        }
    }
}
