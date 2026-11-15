using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLevelScript : MonoBehaviour
{

    public string NextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(NextLevel);
    }

}
