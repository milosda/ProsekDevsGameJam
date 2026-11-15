using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;




public class ChangeMenusScript : MonoBehaviour
{
    public GameObject ActiveMenu;
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







}
