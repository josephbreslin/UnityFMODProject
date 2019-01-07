using UnityEngine;

public class Player_Gun : MonoBehaviour 
{
	public GameObject projectile;
	public bool gunIsActive = false;
	public string fireButton = "Fire1";
    

	void Start()
	{
        gunIsActive = false;
	}

	void Update()
	{
        if (Game_Manager.isGameOver)
        {
            gunIsActive = false;
        }
        if (gunIsActive)
		{
			Shoot();
		}      
	}

	void Shoot()
	{
		if(Input.GetButtonDown(fireButton))
		{
			GameObject projectileInstance = 
						Instantiate(projectile, transform) as GameObject;
            projectileInstance.transform.SetParent(null);
			//Add force with Force component on Projectile
		}
	}
}
