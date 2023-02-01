

using UnityEngine;

//this scipt is reponsable to move the character in the first person pov.
[RequireComponent(typeof(CharacterController))]
public class FPMovement : MonoBehaviour
{
    [Header("Character Information")]
    [SerializeField] private float speed = 0;
    [SerializeField] private float walkingSpeed = 6f;
    [SerializeField] private float runningSpeed = 10f;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isWalking = false;

    [Header("Animation and Audiosources")]
    [SerializeField] private Animator torchAnimator;
    [SerializeField] private GameObject walkingFootSteps;
    [SerializeField] private GameObject runningFootSteps;


    [Header("Refferences")]
    private CharacterController fPcontroller;
    [SerializeField] private Gravity gravityScript;
    void Start()
    {
        fPcontroller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            fPcontroller.Move(move * speed * Time.deltaTime);

            if (move.magnitude >= 0.1 && !Input.GetKey(KeyCode.LeftShift))
            {
                isWalking = true;

            }
            else
                isWalking = false;

            if (isWalking)
            {
                speed = walkingSpeed;
                torchAnimator.SetBool("isWalking", true);
                walkingFootSteps.SetActive(true);
            }
            else
            {
                walkingFootSteps.SetActive(false);
                torchAnimator.SetBool("isWalking", false);
                torchAnimator.SetBool("isRunning", false);
            }
            if (Input.GetKey(KeyCode.LeftShift) && move.magnitude >= 0.1)
            {
                isRunning = true;
                Run();
                
            }
            else            
            isRunning= false;
            
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRunning = false;
                Run();
            }
            if(isRunning)
            {
            torchAnimator.SetBool("isRunning", true);
            runningFootSteps.SetActive(true);
            }
            else
            {
            torchAnimator.SetBool("isRunning", false);
            runningFootSteps.SetActive(false);
            }
            if(!gravityScript.isGrounded)
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
        isRunning= false;
        isWalking= false;
    }
}

