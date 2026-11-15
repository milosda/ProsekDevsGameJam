using UnityEngine;

public class AmpuleScript : MonoBehaviour
{
    public GameObject Ampule;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            
            collision.GetComponent<PlayerState>().AmpuleCount += 1;
            Destroy(Ampule);
        }
    }
}
