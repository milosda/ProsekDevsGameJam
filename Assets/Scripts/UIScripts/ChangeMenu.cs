using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeMenu : MonoBehaviour
{
    public GameObject MenuToActivate;

    public GameObject MenuToDeactivate;

    public Button ThisButton;


  



    public void ActivateAndDeactivateMenu() 
    {
        MenuToDeactivate.SetActive(false);
        MenuToActivate.SetActive(true);
    }



}
