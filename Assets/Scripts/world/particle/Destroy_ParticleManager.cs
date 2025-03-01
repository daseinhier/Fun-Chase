using System.Collections.Generic;
using UnityEngine;

public class Destroy_ParticleManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerLocomative locomative; 

    public List<ParticleSystem> particles = new List<ParticleSystem>();


    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        locomative = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLocomative>();
    }

    private void OnEnable()
    {
        foreach (var particle in particles)
        {
            var mainModule = particle.main;
            mainModule.startColor = new Color(
                playerManager.player_data_base.color_data.color_RGB.x,
                playerManager.player_data_base.color_data.color_RGB.y,
                playerManager.player_data_base.color_data.color_RGB.z
                );  // Rengi kýrmýzý yap
        }
    }


    private void Update()
    {

        transform.position = new Vector3(
            locomative.transform.position.x,
            transform.position.y,
            transform.position.z

            );
    }
}
