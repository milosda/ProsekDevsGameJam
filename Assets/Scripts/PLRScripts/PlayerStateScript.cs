using System;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public string PLRState { get; set; }

    public static event Action<string> OnPlayerStateChanged;

    



    private void Awake()
    {
        PLRState = "Alive";

        

    }


    private void ChangePlayerState(string ChangedState) 
    {
        PLRState = ChangedState;
        Debug.Log(PLRState);
    }
}
