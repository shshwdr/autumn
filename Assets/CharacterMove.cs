using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float movementSpeed = 1f;

    protected GameObject spriteObject;
    public bool facingRight = true;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected virtual void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteObject = animator.gameObject;
    }
    void flip()
    {
        facingRight = !facingRight;
        if (spriteObject)
        {

            Vector3 scaler = spriteObject.transform.localScale;
            scaler.x = -scaler.x;
            // spriteObject.transform.position = new Vector3(spriteObject.transform.position.x + 1, spriteObject.transform.position.y, -1);
            spriteObject.transform.localScale = scaler;
            //spriteObject.GetComponent<SpriteRenderer>().flipX = !facingRight;
        }
    }
    public void testFlip(Vector3 movement)
    {
        if (facingRight == false && movement.x > 0.1f)
        {
            flip();
        }
        if (facingRight == true && movement.x < -0.1f)
        {
            flip();
        }
    }

}
