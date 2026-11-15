using System;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public string DeathState { get; set; }

    private PlayerInput PLRInput;

    public Animator PLRAnimator;

    public int AmpuleCount;

    private float TimeForRevive = 3f;


    private void Awake()
    {
        DeathState = "Alive";

        PLRInput = GetComponent<PlayerInput>();
        AmpuleCount = 0;

        

    }


    private void Update()
    {
        if (PLRInput.ReadKillButton())
        {
            SetPlayerStateDeath();
        }

        if (AmpuleCount > 0 && DeathState == "Dead") 
        {
            TimeForRevive -= Time.deltaTime;
            if (TimeForRevive < 0)
            {
                DeathState = "Alive";
                SetPlayerStateAlive();
                AmpuleCount -= 1;
                TimeForRevive = 3f;
            }
        }
    }

    public void SetPlayerStateDeath() 
    {
        DeathState = "Dead";
        PLRAnimator.SetBool("Dead", true);
    }

    public void SetPlayerStateAlive()
    {
        DeathState = "Alive";
        PLRAnimator.SetBool("Dead", false);
    }

}
