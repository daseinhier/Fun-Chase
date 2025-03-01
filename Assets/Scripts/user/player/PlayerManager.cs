using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player_data player_data_base;
    private SpriteRenderer sprite_renderer;
    private List<Player_data> player_data;
    
    private void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.enabled = false;


        player_data = new List<Player_data>();

        // Resources klasöründen "Enemy/Data" içerisindeki tüm Player_data varlýklarýný yükle
        Player_data[] loadplayers = Resources.LoadAll<Player_data>("Player/Data");

        // Listeye ekle
        player_data.AddRange(loadplayers);

        ChoosePlayerData();
    }
    private void Start()
    {
        sprite_renderer.sprite = player_data_base.sprite;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("startgame"))
        {
            sprite_renderer.enabled=true;
        }
    }


    private void ChoosePlayerData()
    {
        int random = Random.Range(0, player_data.Count);
        player_data_base = player_data[random];
    }
  

}
