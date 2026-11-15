using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;




public class ChangeMenusScript : MonoBehaviour
{
    public GameObject ActiveMenu;

    public GameObject ActiveObject;

    public GameObject ActiveObject2;

    public GameObject DeactObject;
    public GameObject DeactObject2;

    public GameObject DeactivateMenu;
    public string LevelName;
    

    public void ChangeMenus()
    {
        ActiveMenu.SetActive(true);
        DeactivateMenu.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void ActivateMenu() 
    {
        ActiveMenu.SetActive(true);
    }

    public void DeactMenu() 
    {
        DeactivateMenu.SetActive(false);
    }


    public void ActivateObject()
    {
        ActiveObject.SetActive(true);
        ActiveObject2.SetActive(true);
    }

    public void DeactivateObject() 
    {
        DeactObject.SetActive(false);
        DeactObject2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            ChangeMenus();
            DeactivateObject();
        }
    }




}
