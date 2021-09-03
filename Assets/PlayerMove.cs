using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : CharacterMove
{
    public float movementSpeed = 1f;
    Rigidbody2D rb;
    Vector2 movement;
    PlayerPickup pickup;
    Animator animator;
    public bool isFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        pickup = GetComponent<PlayerPickup>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteObject = animator.gameObject;
    }

    bool canMove()
    {
        return !pickup.isPickingUp;
    }
    // Update is called once per frame
    void Update()
    {
        if (!canMove())
        {

            animator.SetFloat("speed", 0);
            return;
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = horizontalInput * Vector2.right + verticalInput * Vector2.up;
        //Vector2 inputVector = new Vector2(horizontalInput  , verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        movement = inputVector * movementSpeed;
        animator.SetFloat("speed", movement.magnitude);
        testFlip(movement);
    }
    private void FixedUpdate()
    {
        if (!canMove())
        {
            return;
        }
        Vector2 currentPosition = rb.position;
        //rb.MovePosition(currentPosition + movement * Time.deltaTime);
        rb.MovePosition(currentPosition+ movement * Time.deltaTime);
        
    }
}
