using UnityEngine;
using System.Collections;
using FMODUnity;

public class Enemy_AI : MonoBehaviour
{
    
	public enum EnemyType { MINION, BOSS };

	public GameObject projectile;
    public GameObject[] bossProjectiles;
    public EnemyType enemyType;
	Vector3 returnPosition;
	public bool isBossFight;
	public float distanceToPlayer;
    public float minionSpeed, bossSpeed;
	Transform player;
    public bool isShooting;
    Rigidbody rb;
    public int bossShotIndex = 0;
    public float circleShotSpeed = 2f;
    float bossShotTime;


    public float 	singleShotFireRate = 1f, 
					trippleAttackFireRate = 1.5f, 
					waveAttackFireRate = 0.25f, 
					restTime = 3f,
					minionAlertDistance = 4f,
                    bossAlertDistance = 6f,
					minionStopDistance = 1f,
					bossStopDistance = 2f;

	void Start()
	{
        rb = GetComponent<Rigidbody>();
        isShooting = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		isBossFight = false;
		returnPosition = transform.position;

        if(enemyType == EnemyType.BOSS)
        {
            bossShotIndex = 0;
            bossShotTime = singleShotFireRate;
            StartCoroutine(StepBossShotIndex());
            StartCoroutine(BossShotTime());
        }
        else
        {
            StartCoroutine(Shoot());
        }       
    }

	void FixedUpdate(){

		distanceToPlayer = Vector3.Distance(transform.position, player.position);
		if(enemyType == EnemyType.MINION)
		{
			Minion();
		}
		else
		{
			Boss();
		}
	}

	void Minion()
	{
		if(distanceToPlayer < minionAlertDistance)
		{
			transform.LookAt(player.position);
			
            isShooting = true;        
            if(distanceToPlayer > minionStopDistance)
            {
                rb.AddForce(transform.forward * minionSpeed);
                //Vector3.Lerp(transform.position, player.position, Time.fixedDeltaTime * minionSpeed);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            isShooting = false;
            transform.LookAt(returnPosition);
            //Vector3.Lerp(transform.position, returnPosition, Time.fixedDeltaTime * minionSpeed);
        }
	}

	IEnumerator Shoot(){
        yield return new WaitForSeconds(singleShotFireRate);
        if (isShooting && !Game_Manager.isGameOver)
        {
            Transform child = transform.GetChild(0);
            GameObject projectileInstance = Instantiate(projectile, child) as GameObject;
            projectileInstance.transform.SetParent(null);
        }   
        StartCoroutine(Shoot());
    }

    #region Boss Shot Methods


    void Boss()
    {   
        if (isBossFight)
        {
            if (bossShotIndex == 4)
            {
                //circle
                transform.Rotate(transform.up, 1 * circleShotSpeed);
            }
            else
            {
                BossMovement();
            }
        }
    }

    IEnumerator BossShotTime()
    {     
        yield return new WaitForSeconds(bossShotTime);
        BossShoot();
        StartCoroutine(BossShotTime());
            
    }

    void BossShoot()
    {
        if (isBossFight)
        {
            switch (bossShotIndex)
            {
                case 0:
                    BossSingleShot();
                    break;
                case 1:
                    Rest();
                    break;
                case 2:
                    BossTripleShot();
                    break;
                case 3:
                    Rest();
                    break;
                case 4:
                    BossCircleShot();
                    break;
                case 5:
                    Rest();
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator StepBossShotIndex()
    {
        yield return new WaitForSeconds(5);

        if (bossShotIndex == 5)
            bossShotIndex = 0;
        else
            bossShotIndex++;

        StartCoroutine(StepBossShotIndex());
    }

    void BossMovement()
    {
        if (distanceToPlayer < bossAlertDistance)
        {
            Vector3 playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(playerPos);

            if (distanceToPlayer > bossStopDistance)
            {
                rb.AddForce(transform.forward * bossSpeed);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.LookAt(returnPosition);
        }
    }

    void Rest()
    {

    }

    void BossTripleShot()
    {
        bossShotTime = trippleAttackFireRate;
        Transform[] children = { transform.GetChild(0), transform.GetChild(1), transform.GetChild(2) };
        foreach(Transform t in children)
        {
            GameObject projectileInstance = Instantiate(bossProjectiles[1], t) as GameObject;
            projectileInstance.transform.SetParent(null);           
        }
    }

    void BossCircleShot()
    {
        bossShotTime = waveAttackFireRate;
        Transform child = transform.GetChild(0);
        GameObject projectileInstance = Instantiate(bossProjectiles[2], child) as GameObject;
        projectileInstance.transform.SetParent(null);
    }

    void BossSingleShot()
    {
        bossShotTime = singleShotFireRate;
        Transform child = transform.GetChild(0);
        GameObject projectileInstance = Instantiate(bossProjectiles[0], child) as GameObject;
        projectileInstance.transform.SetParent(null); 
    }

    #endregion
}