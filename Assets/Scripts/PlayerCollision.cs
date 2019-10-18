using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public GameObject explosion;


    private GameController gameController;

	// Use this for initialization
	void Start () {
        GameObject gameControllerObj = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObj != null)
            gameController = gameControllerObj.GetComponent<GameController>();
        if (gameController == null)
            Debug.Log("Cannot find 'GameController' script");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            //Destroy(explosion, 5f);

            gameObject.SetActive(false);
            Destroy(other.gameObject);

            gameController.GameOver();
        }
        
    }
}
