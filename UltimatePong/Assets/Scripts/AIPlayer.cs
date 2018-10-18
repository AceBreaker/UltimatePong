using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaddleBehaviour))]
public class AIPlayer : MonoBehaviour {

    PaddleBehaviour paddle = null;
    Transform ballTransform = null;

    private void Awake()
    {
        paddle = GetComponent<PaddleBehaviour>();
        GameObject ballObject = GameObject.Find("Ball");
        if(ballObject == null)
        {
            Debug.Log("There is no ball object named 'Ball'");
            return;
        }
        ballTransform = ballObject.transform;
    }

    // Update is called once per frame
    void Update () {
        if(ballTransform ==null)
        {
            Debug.Log("You got issues bro. ballTransform is null");
        }
		if(ballTransform.position.y - 0.25f > transform.position.y)
        {
            paddle.MovePaddle(Vector2.up);
        }
        else if(ballTransform.position.y + 0.25f < transform.position.y)
        {
            paddle.MovePaddle(Vector2.down);
        }
	}
}
