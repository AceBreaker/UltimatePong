using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    Rigidbody2D myRB = null;
    public delegate void GenericDelegate(bool leftGoal);
    public GenericDelegate onScore = null;

    public float bounceForce = 3.0f;
	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().SetBallReference(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVelocity(Vector2 newVel)
    {
        myRB.velocity = newVel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Player":
                Vector2 direction = transform.position - collision.transform.position ;
                SetVelocity(direction * bounceForce);
                bounceForce += 1.0f;
                break;
            case "Goal":
                break;
            default:
                Vector2 direction2 = new Vector2(myRB.velocity.x, myRB.velocity.y * -1);
                SetVelocity(direction2);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Vector2 direction = transform.position - collision.transform.position;
                
                SetVelocity(direction.normalized * bounceForce);
                bounceForce += 1.0f;
                break;
            case "Goal":
                if(onScore != null)
                    onScore((collision.transform.position.x < 0));
                bounceForce = 3.0f;
                break;
            default:
                Vector2 direction2 = new Vector2(myRB.velocity.x, myRB.velocity.y * -1);
                SetVelocity(direction2);
                break;
        }
    }
}
