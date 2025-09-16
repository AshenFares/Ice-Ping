using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{

    int playerOneScore = 0;
    int playerTwoScore = 0;
    //we use below line to be able use another method in another class 
    public PlayerMovement playerMovement;
    public BallMovement ballMovement;
    public TMP_Text playerOneTxt;
    public TMP_Text playerTwoText;
    public Button myButton; 
    public TMP_Text winner;

    private CinemachineCamera virtualCamera;

   

    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ball"))
        {
            if (gameObject.CompareTag("Goal Right") || gameObject.CompareTag("Goal Up"))
            {
                playerOneScore++;
                Debug.Log("Player One Scored : " + playerOneScore);
                playerOneTxt.text = playerOneScore.ToString();
                if (playerOneScore >= 3)
                {
                    ballMovement.gameOn = false;
                    ballMovement.StopBall();
                    winner.gameObject.SetActive(true);
                    winner.text = "Player One has Won!!!!!";
                    myButton.gameObject.SetActive(true);


                    Debug.Log("player one has won");
                }
                else
                {
                    playerMovement.SwitchPlayers();
                    ballMovement.ResetBall();

                }
            }
            else if (gameObject.CompareTag("Goal Left") || gameObject.CompareTag("Goal Down"))
            {
                playerTwoScore++;
                Debug.Log("Player Two Scored " + playerTwoScore);
                playerTwoText.text = playerTwoScore.ToString();
                if (playerTwoScore >= 3)
                {

                    ballMovement.gameOn = false;
                    ballMovement.StopBall();
                    winner.gameObject.SetActive(true);
                    winner.text = "Player Two has Won!!!!";
                    Debug.Log("player2 has won");
                    myButton.gameObject.SetActive(true);

                }
                else
                {
                    playerMovement.SwitchPlayers();
                    ballMovement.ResetBall();

                }

            }

        }

    }

    void Update()
    {


    }

    public void Rematch()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
