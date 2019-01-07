using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD;

public class Game_Manager : MonoBehaviour
{
	static public float score;
	static public float playerHealth;
	static public float orbCount;
	static public bool isGameOver;
	public Damage_Red damageRed;
	public UserInterface ui;
    float timer = 0.5f;
    public float setPlayerHealth = 20f;
    StudioEventEmitter UIEventEmitter;
    StudioEventEmitter musicEvent;
    StudioEventEmitter masterAmbiance;

    void Start()
	{
        UIEventEmitter = GetComponent<StudioEventEmitter>();
        musicEvent = GameObject.FindGameObjectWithTag("FMOD_Music").GetComponent<StudioEventEmitter>();
        masterAmbiance = GameObject.FindGameObjectWithTag("FMOD_Ambiance").GetComponent<StudioEventEmitter>();

        Reset();
	}

    void ResetHealthLowPass(int health)
    {
        GameObject.FindGameObjectWithTag("FMOD_Ambiance").GetComponent<StudioEventEmitter>().SetParameter("Player_Health", health);
    }

    void Update()
	{

        if (playerHealth <= 0)
		{
            ResetHealthLowPass(50);
            musicEvent.Stop();
            masterAmbiance.SetParameter("Location",4);

            if (!isGameOver){
				Death();	
			}else
			{
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (Input.anyKeyDown)
                    {
                        PlayUISound();
                        Loading();
                        Invoke("LoadScene", 2);                    
                    }
                }
			}
		}
	}

    void Loading()
    {
        ui.loading = true;
    }

    void LoadScene()
    {
        Reset();
        SceneManager.LoadScene(0);
    }

    void PlayUISound()
    {
        UIEventEmitter.Play();
    }

	void Death()
	{
		//Fade To Black UI
		//Pop Up message & Turn off all UI elements
		ui.GameOver();
		isGameOver = true;
        damageRed.FadeToBlack();
    }

	public void Reset()
	{
		isGameOver = false;
		playerHealth = setPlayerHealth;
		orbCount = 0;
		score = 0;
        timer = 2f;
    } 

}