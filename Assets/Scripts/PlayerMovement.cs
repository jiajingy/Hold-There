using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float tilt;
    public SimpleTouchPad touchPad;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        



	}

    private void FixedUpdate()
    {
        /**PC Control**/
        /*
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        */

        Vector2 direction = Vector2.zero;
        if (gameObject.activeSelf)
        {
            direction = touchPad.GetDirection();
        }

        
        Vector3 movement = new Vector3(direction.x, 0f, direction.y);


        rb.velocity = movement * speed;


        rb.MoveRotation(Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt));

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, -GameController.xAxis, GameController.xAxis),
            0f,
            Mathf.Clamp(rb.position.z,-GameController.zAxis, GameController.zAxis)
        );
    }

    
}
