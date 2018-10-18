using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehaviour : MonoBehaviour {

    [SerializeField]
    float moveSpeed = 1.0f;

    float boundaryoffset = 3.8f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MovePaddle(Vector2 moveVector)
    {
        if (moveVector == Vector2.zero || !CanMoveThere(moveVector))
            return;

        Vector2 finalMoveVector = moveVector * Time.deltaTime * moveSpeed;
        transform.position += new Vector3(finalMoveVector.x, finalMoveVector.y);
    }

    private bool CanMoveThere(Vector2 moveVector)
    {
        if (moveVector.y > 0 && transform.position.y >= boundaryoffset)
            return false;
        else if (moveVector.y < 0 && transform.position.y <= -boundaryoffset)
            return false;
        return true;
    }
}
