using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    [HideInInspector] float x = 0, y = 0, z = 0;
    [HideInInspector] Vector2 rotation = Vector2.zero;
    [HideInInspector] Quaternion xQuat, yQuat; //Strings in direct code generate garbage, storing and re-using them creates no garbage
	[HideInInspector] const string xAxis = "Mouse X";
    [HideInInspector] bool jump = false, grounded = false, crouch = false;
    [HideInInspector] CharacterController controller;
    [HideInInspector] Transform groundCheck, lantern;
    [HideInInspector] public bool firstHitDone = false, canBeHurt = true;
    [HideInInspector] public float invulTimer = 0f;
    [Range(1, 100)] public float mouseSensibility = 10f;
    [Range(1, 50)] public float movementSpeed = 10f;
    [Range(1, 20)] public float jumpHeight = 10f;
    [Range(0, -50)] public float gravityForce = -10f;
    [Range(0, 10)] public float sprintDuration = 10f;
    [Range(1, 5)] public float sprintSpeedMultiplier = 3f;
    [Range(1, 5)] public float invulnerabilityTime = 3f;
    [SerializeField] public int maxHealth = 20;
    [HideInInspector] public int currentHP;
    [SerializeField] public GameObject hurtOverlay;
    [SerializeField] public GameObject deathOverlay;
    [HideInInspector] float deathTimer;
    [HideInInspector] Image deathImage;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.transform.GetComponent<CharacterController>();
        groundCheck = this.transform.GetChild(0);
        lantern = this.transform.GetChild(1);
        Cursor.lockState = CursorLockMode.Locked;
        currentHP = maxHealth;
        deathImage = deathOverlay.GetComponent<Image>();
    }

    // FixedUpdate is called 50 times/second (each 0.02s) --> should be used for Physics
    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(groundCheck.position, 0.2f, LayerMask.GetMask("Floor"));

        if(!grounded)
            y += gravityForce;
        else 
            y = 0f;
        
        if(grounded && jump)
            y = jumpHeight;

        // Time.deltaTime for the actual time
        transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        controller.Move((transform.right * x * movementSpeed + transform.up * y + transform.forward * z * movementSpeed)  * Time.deltaTime);
    }

    // Update is called once per frame --> should be used for User Input
    void Update()
    {
        if(currentHP <= 0){
            var colorUpdated = deathImage.color;
            colorUpdated.a += 0.5f*Time.deltaTime;
            deathImage.color = colorUpdated;
            if(deathImage.color.a >= 1f){
                deathOverlay.transform.GetChild(0).gameObject.SetActive(true);
                if(Input.anyKey)
                    SceneManager.LoadScene(0);
            }
        } else {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            
            rotation.x += Input.GetAxis(xAxis) * mouseSensibility;
            xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            jump = Input.GetAxis("Jump") > 0 ? true : false;
            crouch = Input.GetAxis("Fire1") > 0 ? true : false;

            if(crouch && controller.height >= 2f){
                controller.height = 1f;
                movementSpeed = movementSpeed/2;
                groundCheck.position += (transform.up);
            } else if (!crouch && controller.height < 2f) {
                controller.height = 2f;
                movementSpeed = movementSpeed*2;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
                groundCheck.position += (transform.up * -1f);
            }
            
            if(Input.GetMouseButtonDown(0)){
                lantern.gameObject.SetActive(!lantern.gameObject.activeSelf);
            }
            
            if(!canBeHurt && invulTimer<invulnerabilityTime){
                invulTimer += Time.deltaTime;
            } else {
                canBeHurt = true;
                invulTimer = 0f;
            }
            

            if(!firstHitDone){
                if (sprintDuration<=0) {
                    movementSpeed = movementSpeed/sprintSpeedMultiplier;
                    firstHitDone = true;
                }
                else if(sprintDuration<10f) // < max Valor de la variable anterior
                    sprintDuration -= Time.deltaTime;
            }
        }
    }

    public void Hurt(double damagePercent){
        if(canBeHurt){
            if(!firstHitDone){
                sprintDuration -= Time.deltaTime;
                movementSpeed *= sprintSpeedMultiplier;
            }
            this.currentHP -= (int) (maxHealth*damagePercent);
            canBeHurt = false;
        }   
        // Update Hurt Overlay
        Image image = hurtOverlay.GetComponent<Image>();
        var updatedColor = image.color;
        if(this.currentHP<=maxHealth*0.2)
            updatedColor.a = 0.8f;
        else if(this.currentHP<=maxHealth*0.4)
            updatedColor.a = 0.6f;
        else if(this.currentHP<=maxHealth*0.6)
            updatedColor.a = 0.4f;
        else if(this.currentHP<=maxHealth*0.8)
            updatedColor.a = 0.2f;
        else 
            updatedColor.a = 0f;
         
        image.color = updatedColor;
        Debug.Log("Current HP: " + this.currentHP);
    }

    [ContextMenu("Test")]
    void Test(){
        Debug.Log("Current Move Vector\n" + new Vector3(x,y,z)*movementSpeed*Time.deltaTime);
        Debug.Log("Current Health: " + this.currentHP + " & Current Speed: " + this.movementSpeed);
    }
    
    [ContextMenu("FlashlightTest")]
    void FlashlightTest(){
        lantern.gameObject.SetActive(!lantern.gameObject.activeSelf);
    }

    [ContextMenu("HitTest")]
    void HitTest(){
        this.Hurt(1);
    }
}
