using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour 
{
	public Text score, orbs, playerHealth, controls, restartText, gameOverText;
	bool isGameOver;
    public bool loading;

	void Start()
	{
        loading = false;
		isGameOver = false;
		gameOverText.text = "";
		restartText.text = "";
		score.text = "Score: " + Game_Manager.score;
		playerHealth.text = "Health: " + Game_Manager.playerHealth.ToString();
        controls.text = "Arrows to move. Pick up red orb to enable weapon";
		orbs.text = "Orbs: " + Game_Manager.orbCount;
	}

	public void GameOver()
	{
		isGameOver = true;
	}

	void Update()
	{
		if(!isGameOver)
		{
			gameOverText.text = "";
			restartText.text = "";
			score.text = "Score: " + Game_Manager.score;
			playerHealth.text = "Health: " +  Game_Manager.playerHealth.ToString();
			orbs.text = "Orbs: " + Game_Manager.orbCount;
		}
		else
		{
            if (!loading)
            {
                gameOverText.text = "You finished with " + Game_Manager.orbCount + " Orbs and a score of " + Game_Manager.score + "!!";
                restartText.text = "Press anykey to Restart";
                score.text = "";
                playerHealth.text = "";
                controls.text = "";
                orbs.text = "";
            }
            else
            {
                gameOverText.text = "LOADING..";
                restartText.text = "";
                score.text = "";
                playerHealth.text = "";
                orbs.text = "";
            }
		}
	}
}
