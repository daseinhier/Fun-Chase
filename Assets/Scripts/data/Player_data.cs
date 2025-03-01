using UnityEngine;

public enum Player_type
{
    player,
    enemy
}

[CreateAssetMenu(fileName = "DATA", menuName = "Player_DATA")]
public class Player_data : ScriptableObject
{
    public Color_data color_data;
    public Shape_data shape_data;
    public Player_type player_type;
    public Sprite sprite;
}
