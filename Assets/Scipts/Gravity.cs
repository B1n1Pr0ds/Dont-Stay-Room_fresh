using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;


//This script is reponsable to control the character velocity down and up.
[RequireComponent(typeof(CharacterController))]
public class Gravity : MonoBehaviour
{

    private Vector3 velocity;
    public bool isGrounded;
    bool isJumping;

    [SerializeField] private float gravity = -9.18f;
    [SerializeField] private float jumpHeight = 1.5f;
    

    private CharacterController player;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance= 0.5f;
    [SerializeField] LayerMask groundMask;


    [SerializeField] GameObject jumpingSoundEffect;
    
    private void Start()
    {
        player = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            isJumping= false;
            velocity.y = -2f;
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);

        if(isJumping)
        {
            animator.SetBool("isJumping", true);
            jumpingSoundEffect.SetActive(true);
        }
        else
        {
            animator.SetBool("isJumping", false);
            jumpingSoundEffect.SetActive(false);
        }
    }

  
}
