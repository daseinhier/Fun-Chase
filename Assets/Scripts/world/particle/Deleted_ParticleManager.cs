using UnityEngine;

public class Deleted_ParticleManager : MonoBehaviour
{
    public PlayerLocomative player;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLocomative>();
    }


    private void Update()
    {
        transform.position = new Vector3(
            player.transform.position.x,
            transform.position.y,
            transform.position.z
            
            );
    }




}
