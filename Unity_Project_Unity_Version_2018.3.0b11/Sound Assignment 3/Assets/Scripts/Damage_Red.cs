using UnityEngine;
using UnityEngine.UI;

public class Damage_Red : MonoBehaviour 
{
	public Image cameraScreenImg;
	public Color returnColor;
	public float fadeSpeed = 2f;

	void Start()
	{
		cameraScreenImg.color = returnColor;
	}

	void Update()
	{
		if(cameraScreenImg.color != returnColor)
		{
			cameraScreenImg.color = Color.Lerp(cameraScreenImg.color, returnColor, fadeSpeed* Time.deltaTime);

		}
	}

	public void FadeToBlack()
	{
        returnColor = Color.black;
        
	}

	public void TakeDamage()
	{
		cameraScreenImg.color = Color.red;
	}


}