using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
	public Transform player;
	public float followSpeed = 1f;

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), followSpeed * Time.deltaTime);

        Vector3 playerEulerRotation = player.rotation.eulerAngles;
 
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(90, 0, -playerEulerRotation.y), 1 * Time.deltaTime);

    }


}