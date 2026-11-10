using System;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public string PLRState { get; set; }

    public static event Action<string> OnPlayerStateChanged;

    public static PlayerState PlayerStateScript = null;



    private void Awake()
    {
        PlayerStateScript = this;

        PlayerStateScript.PLRState = "Alive";

        PlayerStateScript.OnPlayerStateChanged += ChangePlayerState;

    }


    private void ChangePlayerState(string ChangedState) 
    {
        PLRState = ChangedState;
        Debug.Log(PLRState);
    }
}
