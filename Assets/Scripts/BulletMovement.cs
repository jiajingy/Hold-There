using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {


    public float speed;


    private Rigidbody rb;
     

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 PlayerRelativeDirection = (player.transform.position - rb.transform.position).normalized;
        rb.velocity = PlayerRelativeDirection * speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (OutOfBoundary())
            Destroy(rb.gameObject);
	}

    private bool OutOfBoundary()
    {
        return (Mathf.Abs(rb.transform.position.x) > GameController.xAxis+4 || Mathf.Abs(rb.transform.position.z) > GameController.zAxis+4);
    }
}
