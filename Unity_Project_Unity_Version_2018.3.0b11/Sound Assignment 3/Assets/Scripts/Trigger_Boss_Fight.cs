using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Boss_Fight : MonoBehaviour
{
    public Enemy_AI enemy_AI;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemy_AI.isBossFight = true;
            Destroy(this.gameObject);   
        }
    }
}
