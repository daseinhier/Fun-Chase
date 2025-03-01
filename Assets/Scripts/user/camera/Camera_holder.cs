using DG.Tweening;
using UnityEngine;

public class Camera_holder : MonoBehaviour
{
    public void ShakeCamera()
    {
        transform.DOShakePosition(0.5f, 5, 8, 70).OnKill(() => SetCameraYPosition());
    }
    public void ShakeCamera2()
    {
        transform.DOShakePosition(0.2f, 2, 6, 50).OnKill(() => SetCameraYPosition());
    }
    private void SetCameraYPosition()
    {
        // Y pozisyonunu sabitliyoruz
        Vector3 currentPosition = transform.position;
        currentPosition.y = 1.11f;
        transform.position = currentPosition;
    }

    public void EndGamerShake()
    {
        //transform.DOShakePosition(1, 5, 6, 50).OnKill(() => 
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLocomative>().EnderGame());
    }

}
