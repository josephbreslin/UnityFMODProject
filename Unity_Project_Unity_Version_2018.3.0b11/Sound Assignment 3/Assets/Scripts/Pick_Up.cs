using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class Pick_Up : MonoBehaviour
{
	public enum PickUpType {GUN,ORB,HEALTH};
	public PickUpType pickUpType;

	void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            if (pickUpType == PickUpType.ORB)
            {
                PickUpOrb();
            }
            else if (pickUpType == PickUpType.GUN)
            {
                PickUpGun();
            }
            else if(pickUpType == PickUpType.HEALTH)
            {
                PickUpHealth();
            }
            Destroy(this.gameObject,0.2f);
        }
	}

    void PickUpHealth()
    {
        Game_Manager.playerHealth = 30f;
        int health = (int)Game_Manager.playerHealth;
        GameObject.FindGameObjectWithTag("FMOD_Ambiance").GetComponent<StudioEventEmitter>().SetParameter("Player_Health", health);
    }

	void PickUpOrb()
	{
  		Game_Manager.orbCount += 1;
  		Game_Manager.score += 5;
	}

	void PickUpGun()
	{
		Player_Gun playerGun = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Gun>();
		playerGun.gunIsActive = true;
        Text controlText = GameObject.FindGameObjectWithTag("Text_Controls").GetComponent<Text>();
        controlText.text = "Space to shoot. Destroy Enemies and collect green orbs";
	}
}
