using UnityEngine;

public class Enemy_manager_fallen : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Player_data playerData;
    private Camera_holder camera_holer;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        camera_holer = GameObject.FindGameObjectWithTag("camera_holder").GetComponent<Camera_holder>();
    }
    private void Start()
    {
        spriteRenderer.sprite = playerData.sprite;
        spriteRenderer.color = new Color
           (
               playerData.color_data.color_RGB.x,
               playerData.color_data.color_RGB.y,
               playerData.color_data.color_RGB.z
           );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("startgame"))
        {
            spriteRenderer.enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            camera_holer.ShakeCamera2();
        }
    }




}
