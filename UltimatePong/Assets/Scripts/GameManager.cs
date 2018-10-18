using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    bool blueHumanPlayer = true;
    bool redHumanPlayer = true;

    int blueScore = 0;
    int redScore = 0;

    UnityEngine.UI.Text blueScoreText = null;
    UnityEngine.UI.Text redScoreText = null;
    UnityEngine.UI.Text gameOverText = null;

    int endGameScore = 7;

    GameObject ballReference = null;

    bool respawning = false;
    float timeTilRespawn = 3.0f;

    public delegate void GenericDelegate();
    public static GenericDelegate onScore = null;

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if(!IsGameOver() && respawning && (timeTilRespawn -= Time.deltaTime) <= 0.0f)
        {
            respawning = false;
            timeTilRespawn = 3.0f;
            RandomizeBallSpeed();
        }
        else if(IsGameOver())
        {
            if(redScore > blueScore)
            {
                gameOverText.color = redScoreText.color;
                gameOverText.text = "RED PLAYER WINS";
            }
            else
            {
                gameOverText.color = blueScoreText.color;
                gameOverText.text = "BLUE PLAYER WINS";
            }
            if(Input.anyKeyDown)
            {
                redScore = blueScore = 0;
                blueHumanPlayer = redHumanPlayer = true;
                SceneManager.LoadScene(0);
            }
        }
	}

    bool IsGameOver()
    {
        return (redScore >= endGameScore || blueScore >= endGameScore);
    }

    public void SetBlueHumanPlayer(bool val)
    {
        blueHumanPlayer = !blueHumanPlayer;
    }

    public void SetRedHumanPlayer(bool val)
    {
        redHumanPlayer = !redHumanPlayer;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "SampleScene")//best name of any scene
        {
            InitPaddlePlayer(GameObject.Find("BluePaddle"), blueHumanPlayer);
            InitPaddlePlayer(GameObject.Find("RedPaddle"), redHumanPlayer);
            blueScoreText = GameObject.Find("BlueScore").GetComponent<UnityEngine.UI.Text>();
            redScoreText = GameObject.Find("RedScore").GetComponent<UnityEngine.UI.Text>();
            gameOverText = GameObject.Find("GameOverText").GetComponent<UnityEngine.UI.Text>();
        }
    }

    void InitPaddlePlayer(GameObject paddle, bool humanPlayer)
    {
        if(paddle == null)
        {
            Debug.Log("something is wrong, paddle is null");
            return;
        }
        if (humanPlayer)
            paddle.AddComponent<HumanPlayer>();
        else
            paddle.AddComponent<AIPlayer>();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SetBallReference(GameObject ball)
    {
        ballReference = ball;
        //TODO
        RandomizeBallSpeed();
        if(ballReference.GetComponent<BallBehaviour>().onScore == null)
            ballReference.GetComponent<BallBehaviour>().onScore += PlayerScored;
    }

    private void RandomizeBallSpeed()
    {
        int ranVal = Random.Range(0, 2);
        if (ranVal == 0)
            ranVal--;
        ballReference.GetComponent<BallBehaviour>().SetVelocity(new Vector2(ranVal * 3.0f, Random.Range(-1.0f, 1.0f)));
        
    }

    private void PlayerScored(bool leftGoal)
    {
        if(leftGoal)
        {
            redScore++;
            redScoreText.text = redScore.ToString();
        }
        else
        {
            blueScore++;
            blueScoreText.text = blueScore.ToString();
        }
        respawning = true;
        ballReference.transform.position = Vector3.zero;
        ballReference.GetComponent<BallBehaviour>().SetVelocity(Vector2.zero);
    }
}
