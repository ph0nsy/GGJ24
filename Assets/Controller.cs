using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    [HideInInspector] float x = 0, y = 0, z = 0;
    [HideInInspector] Vector2 rotation = Vector2.zero;
    [HideInInspector] Quaternion xQuat, yQuat; //Strings in direct code generate garbage, storing and re-using them creates no garbage
	[HideInInspector] const string xAxis = "Mouse X";
    [HideInInspector] bool jump = false, grounded = false;
    [HideInInspector] CharacterController controller;
    [HideInInspector] Transform groundCheck, lantern;
    [Range(1, 100)] public float mouseSensibility = 10f;
    [Range(0f, 90f)] public float yRotationLimit = 88f;
    [Range(1, 50)] public float movementSpeed = 10f;
    [Range(1, 20)] public float jumpHeight = 10f;
    [Range(0, -50)] public float gravityForce = -10f;
    public float vida = 3;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.transform.GetComponent<CharacterController>();
        groundCheck = this.transform.GetChild(0);
        lantern = this.transform.GetChild(1);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // FixedUpdate is called 50 times/second (each 0.02s) --> should be used for Physics
    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, 0.4f, LayerMask.GetMask("Floor"));

        if(!grounded)
            y += gravityForce;
        else 
            y = 0f;
        
        if(grounded && jump)
            y = jumpHeight;

        // Time.deltaTime for the actual time
        transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        controller.Move((transform.right * x + transform.up * y + transform.forward * z) * movementSpeed * Time.deltaTime);
    }

    // Update is called once per frame --> should be used for User Input
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        
        rotation.x += Input.GetAxis(xAxis) * mouseSensibility;
		xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        jump = Input.GetKey(KeyCode.Space);
        
        if(Input.GetMouseButtonDown(0)){
            lantern.gameObject.SetActive(!lantern.gameObject.activeSelf);
        }
    }
    
    // LateUpdate is called right after Update --> should be used for Camera stuff
    void LateUpdate()
    {
        
    }

    [ContextMenu("Test")]
    void Test(){
        Debug.Log("Current Move Vector\n" + new Vector3(x,y,z)*movementSpeed*Time.deltaTime);
    }

    [ContextMenu("RuntimeTest")]
    void RuntimeTest(){
        controller.Move(new Vector3(x,y,z)*movementSpeed);
    }
    
    [ContextMenu("FlashlightTest")]
    void FlashlightTest(){
        controller.Move(new Vector3(x,y,z)*movementSpeed);
    }
}
