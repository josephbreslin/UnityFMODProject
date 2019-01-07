using UnityEngine;
using System.Collections;
using FMODUnity;

public class Enemy_Health : MonoBehaviour 
{
	public float scoreReward = 10;
	public bool isDead;
	public float health;
	public float startHealth = 5;
	Collider col;
    Enemy_AI enemy_AI;
    public bool isBoss = false;
    StudioEventEmitter eventEmitter;

	void Start()
	{
        eventEmitter = transform.GetChild(0).GetComponent<StudioEventEmitter>();
		isDead = false;
		health = startHealth;
		col = GetComponent<Collider>();
        enemy_AI = GetComponent<Enemy_AI>();
	}

	void Update()
	{
		if(isDead == false){
			if(health <= 0)
			{              
                col.enabled = false;
                enemy_AI.enabled = false;
                GameObject.FindGameObjectWithTag("FMOD_Music").GetComponent<StudioEventEmitter>().SetParameter("Music_State", 0);
                Game_Manager.score += scoreReward;
				StartCoroutine(Death());
				isDead = true;
			}
		}
	}

    void ResetHealthLowPass(int health)
    {
        GameObject.FindGameObjectWithTag("FMOD_Ambiance").GetComponent<StudioEventEmitter>().SetParameter("Player_Health", health);
    }


    IEnumerator Death()
	{
        eventEmitter.Play();
        yield return new WaitForSeconds(1.5f);
        if (isBoss)
        {
            Game_Manager.playerHealth = 0;
            ResetHealthLowPass(50);
        }

        Destroy(this.gameObject);
	}


}