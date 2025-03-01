using UnityEngine;

public enum Color_d
{
    blue,
    red,
    yellow,
    green,
    brown
}



[CreateAssetMenu(fileName = "DATA", menuName = "Color_DATA")]
public class Color_data : ScriptableObject
{
    public Color_d color;
    public Vector3 color_RGB;
    public bool isMainColor;
}
