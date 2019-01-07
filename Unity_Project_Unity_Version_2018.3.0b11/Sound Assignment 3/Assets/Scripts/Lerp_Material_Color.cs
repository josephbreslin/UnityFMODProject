using UnityEngine;

public class Lerp_Material_Color : MonoBehaviour 
{
	Renderer renderer;
	Color enemyColor;
	Enemy_Health enemyHealth;

	void Start()
	{
		enemyHealth = GetComponent<Enemy_Health>();
		
		renderer = GetComponent<Renderer>();
        enemyColor = renderer.material.color;
    }

	void Update()
	{
		if(enemyHealth.isDead)
		{
			enemyColor = Color.clear;
		}
		renderer.material.color = Color.Lerp(renderer.material.color, enemyColor, 1* Time.deltaTime);
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = Color.Lerp(renderer.material.color, enemyColor, 1 * Time.deltaTime);
        }
    }

}