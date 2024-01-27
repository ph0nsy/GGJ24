using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class search_action : MonoBehaviour
{
    [Range(0,270)]public float rot_left, rot_right;
    //public bool fisrt_left = true;
    public float rot_speed = 0.5f;

    float obj_left, obj_right = new float();

    bool end_rot_left = false, end_rot_right = false;
    Vector3 add_left; // lo que hay que sumarles
    Vector3 add_right;

    
    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    { 
        if(end_rot_left == false) { //(Quaternion.Angle(transform.rotation, obj_left) > 0)
            Debug.Log("doing first rot, angle: " + transform.eulerAngles.y.ToString() + ", obj_left: " + obj_left);

            transform.rotation *= Quaternion.AngleAxis(rot_speed, Vector3.up); 
            if (transform.eulerAngles.y > obj_left) { end_rot_left = true; }
        } else if(end_rot_right == false) {
            Debug.Log("doing second rot, angle: " + transform.eulerAngles.y.ToString() + ", obj_right: " + obj_right);

            transform.rotation *= Quaternion.AngleAxis((rot_speed * -1), Vector3.up);
            if (transform.eulerAngles.y < obj_right) { end_rot_right = true; }
        } else {
            Debug.Log("Finished");
        }
        

        // neg_angle = angle * -1;
        
        
    }

}




































