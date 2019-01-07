using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Music_Enemy_Parameter_Trigger : MonoBehaviour
{
    StudioEventEmitter musicEvent;

    public int bossState = 2;
    public int enemyState = 1;

    public void Start()
    {
        musicEvent = GameObject.FindGameObjectWithTag("FMOD_Music").GetComponent<StudioEventEmitter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            int state = enemyState;

            if(other.name == "Boss")
            {
                state = bossState;
            }

            IsInRange(state);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            int state = enemyState;

            if (other.name == "Boss")
            {
                state = bossState;
            }

            IsInRange(state);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            musicEvent.SetParameter("Music_State", 0);
        }
    }

    void IsInRange(int state)
    {
        musicEvent.SetParameter("Music_State", state);
    }
}
