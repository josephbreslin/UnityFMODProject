using UnityEngine;
using FMODUnity;
using FMOD;

public class Projectile : MonoBehaviour
{

	public enum ProjectileType {	
									PLAYER_PROJECTILE,
									ENEMY_PROJECTILE	
								};

    StudioEventEmitter eventEmitter;
    Rigidbody rb;
	public ProjectileType projectileType;
	string targetTag;
	float projectileDamage;
	Damage_Red damageRed;

	void Start()
	{
       
        rb = GetComponent<Rigidbody>();
		damageRed = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Damage_Red>();
		//Assign Bullet properties
		if(projectileType == ProjectileType.PLAYER_PROJECTILE)
		{
			targetTag = "Enemy";
			projectileDamage = 2f;
		}
		else
		{
            targetTag = "Player";
			projectileDamage = 1f;
		}
        Destroy(this.gameObject, 3f);
        rb.AddForce(transform.forward * 10, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
	{
        //eventEmitter.Event 

        if (other.transform.tag == targetTag)
        {
            eventEmitter = other.gameObject.GetComponent<StudioEventEmitter>();
            //Play SOund tag context fmod play event projectileEventReferenceFMOD
            //DAMAGE THE TARGET, visual que
            if (other.gameObject.GetComponent<Enemy_Health>() != null)
			{        
                eventEmitter.Play();
                other.gameObject.GetComponent<Enemy_Health>().health -= projectileDamage;
				other.gameObject.GetComponent<Renderer>().material.color = Color.red;
                foreach(Renderer r in other.gameObject.GetComponentsInChildren<Renderer>())
                {
                    r.material.color = Color.red;
                }
			}
			else
			{
                eventEmitter.Play();
                Game_Manager.playerHealth -= projectileDamage;
                int health = (int)Game_Manager.playerHealth;
                GameObject.FindGameObjectWithTag("FMOD_Ambiance").GetComponent<StudioEventEmitter>().SetParameter("Player_Health", health);
                damageRed.TakeDamage();
			}
            Destroy(this.gameObject);
		}
	}
}
