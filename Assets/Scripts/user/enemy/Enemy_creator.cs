using UnityEngine;
using System.Collections.Generic;

public class Enemy_creator : MonoBehaviour
{
    public List<Player_data> enemy_data;
    public GameObject enemy_prefab;

    private void Awake()
    {
        enemy_data = new List<Player_data>();

        // Resources klasöründen "Enemy/Data" içerisindeki tüm Player_data varlýklarýný yükle
        Player_data[] loadedEnemies = Resources.LoadAll<Player_data>("Enemy/Data");

        // Listeye ekle
        enemy_data.AddRange(loadedEnemies);
    }
    public void Create_Enemy(Vector3 position)
    {
        GameObject enemy =Instantiate(enemy_prefab, position, Quaternion.identity);

        int random = Random.Range(0, enemy_data.Count);
        enemy.GetComponent<Enemy_manager_fallen>().playerData = enemy_data[random];
    }



}
