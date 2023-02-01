using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is reponsable to move the character in the third person pov.
[RequireComponent(typeof(CharacterController))]   
public class TPMovement : MonoBehaviour
{


    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject walkingFootSteps;
    [SerializeField] private GameObject runningFootSteps;
    [SerializeField] private Gravity gravityScript;

    [Header("Movement")]
    [SerializeField] private float speed = 0;
    [SerializeField] private float walkingSpeed = 6f;
    [SerializeField] private float runningSpeed = 10f;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isWalking = false;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;









    private void Start()
    {
        
        controller = GetComponent<CharacterController>();
        
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // It is normalized because you dont want to move faster when you are moving horizontaly and verticaly in the movement  axis.

        if(direction.magnitude >= 0.1f)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                isWalking = true;
                speed = walkingSpeed;
            }
            else
            {
                isWalking = false;
            }
            
            if (Input.GetKey(KeyCode.LeftShift))
            {

                
                isRunning = true;
                Run();
            }
           

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f,targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime); //same
        }
        else
        {
            isWalking = false;
            isRunning = false;
        }
        if (isWalking)
        {
            animator.SetBool("isWalking", true);
            walkingFootSteps.SetActive(true);
        }
        else
        {
            walkingFootSteps.SetActive(false);
            animator.SetBool("isWalking", false);
            
        }
        if(isRunning)
        {
            runningFootSteps.SetActive(true);
            animator.SetBool("isRunning", true);
        }
        else
        {
            runningFootSteps.SetActive(false);
            animator.SetBool("isRunning", false);
        }
        
        if(Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            
            isRunning= false; 
            Run();
        }
        if (!gravityScript.isGrounded)
        {
            walkingFootSteps.SetActive(false);
            runningFootSteps.SetActive(false);
        }


    }
    private void Run()
    {
        if (isRunning)
        {         
            speed = runningSpeed;
        }
        else
        {            
            speed = walkingSpeed;
        }
    }
    private void OnDisable()
    {
        isRunning = false;
        isWalking = false;
    }
}
