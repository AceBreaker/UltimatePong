using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaddleBehaviour))]
public class HumanPlayer : MonoBehaviour {

    PaddleBehaviour paddle = null;

    string playerAxis = "";

    private void Awake()
    {
        paddle = GetComponent<PaddleBehaviour>();
        playerAxis = transform.position.x < 0 ? "Vertical" : "Vertical2";
    }
	
	// Update is called once per frame
	void Update () {
        paddle.MovePaddle(new Vector2(0.0f, Input.GetAxisRaw(playerAxis)));
	}
}
