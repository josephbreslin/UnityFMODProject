using UnityEngine;

public class Player_Movement : MonoBehaviour 
{
	public float speed = 1f;
    public float rotSpeed = 2f;
    public float speedLimit = 5f;
    Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        if (!Game_Manager.isGameOver)
        {
            float x = Input.GetAxisRaw("Horizontal") * 2;
            transform.Rotate(transform.up, x * rotSpeed);
        }
    }
    void FixedUpdate()
	{
        if (!Game_Manager.isGameOver)
        {
            Move();
        }
	}

	void Move()
	{
		float z = Input.GetAxisRaw("Vertical");

        rb.AddForce(transform.forward * z * speed, ForceMode.VelocityChange);

        if(z == 0)
        {
            rb.velocity = Vector3.zero;
        }

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedLimit);
    }
}