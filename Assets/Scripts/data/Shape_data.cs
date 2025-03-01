using UnityEngine;

public enum Shape
{
    sqaure,
    triangle,
    fivegen,
    sixgen,
}



[CreateAssetMenu(fileName ="DATA", menuName ="Shape_DATA")]
public class Shape_data : ScriptableObject
{
    public Shape shape;
    public int angle;
}
