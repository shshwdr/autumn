using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    protected GameObject spriteObject;
    public bool facingRight = true;
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
        if (facingRight == false && movement.x > 0f)
        {
            flip();
        }
        if (facingRight == true && movement.x < 0f)
        {
            flip();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
