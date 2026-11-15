using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapScript : MonoBehaviour
{
    
    public Tilemap DoorCollisionMap;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerState>().DeathState == "Dead")
        {
            DoorCollisionMap.gameObject.SetActive(false);
        }
    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        DoorCollisionMap.gameObject.SetActive(true);
    }


}
