using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLocomative : MonoBehaviour
{
    public float moveSpeed = 10f;   
    public bool canMove;
    public bool isGrounded;

    private AudioSource audioSource;
    public AudioClip portalEnter_sound;
    public AudioClip destroy_sound;
    public AudioClip deleted_sound;
    public AudioClip jump_sound;
    private Rigidbody2D rb;
    public Camera_holder camera_Holder;
    public Enemy_creator enemy_creator;
    public Map_controller map_Controller;
    private PlayerManager player_manager;
    public GameObject destroyParticle;
    public GameObject deletedParticle;
    public GameObject StarterParticle;
    private SpriteRenderer sprite_Renderer;
    public GameObject jumpParticle;
    public AudioClip world_changer_sound;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player_manager = GetComponent<PlayerManager>();
        sprite_Renderer = GetComponent<SpriteRenderer>();
        audioSource = GameObject.FindGameObjectWithTag("audiosource").GetComponent<AudioSource>();
    }

    private void Update()
    {
        Jump();
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            PlayerMove();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("canMove"))
        {
            canMove = true;
            isGrounded = true;
            audioSource.PlayOneShot(portalEnter_sound);
            camera_Holder.ShakeCamera();
            GameObject particle = Instantiate(StarterParticle, new Vector3(
                transform.position.x + 10.5f,
                -2.86f,
                transform.position.z
                ), Quaternion.Euler(
                    -180,270,-180));
            particle.SetActive(true);
            Destroy(particle, 1.2f);
        }
        if (collision.CompareTag("enemyProducer"))
        {
            foreach (Transform child in collision.transform)
            {
                if (child.CompareTag("startgame"))
                {
                    enemy_creator.Create_Enemy(child.transform.position);
                }
            }
        }
        if (collision.gameObject.CompareTag("world_changer"))
        {
            map_Controller.WriteText("map changer => forward");
        }
        if (collision.CompareTag("camera_changer"))
        {
            Camera.main.GetComponent<Camera_controller>().firstPart = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("world_changer"))
        {
            map_Controller.GetNewWorld();
            map_Controller.WriteText("");
            audioSource.PlayOneShot(world_changer_sound);
        }

    }


    private void PlayerMove()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy_manager_fallen>())
        {
            if (map_Controller.mep_type == Map_type.Geometry)
            {
                float enemyAngle = collision.gameObject.GetComponent<Enemy_manager_fallen>().playerData.shape_data.angle;
                float playerAngle = player_manager.player_data_base.shape_data.angle;
                if (playerAngle > enemyAngle)
                {
                    audioSource.PlayOneShot(destroy_sound);
                    map_Controller.IncreaseSlider(0.15f);
                    GameObject particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
                    particle.SetActive(true);
                    Destroy(particle, 1.2f);
                    Destroy(collision.gameObject);

                    StartCoroutine(DeactivateParticleAfterTime(0.4f));
                }
                else if (playerAngle == enemyAngle)
                {
                    audioSource.PlayOneShot(destroy_sound);
                    GameObject particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
                    map_Controller.IncreaseSlider(0.15f);
                    particle.SetActive(true);
                    Destroy(particle, 1.2f);
                    Destroy(collision.gameObject);

                    StartCoroutine(DeactivateParticleAfterTime(0.4f));
                }
                else
                {
                    canMove = false;
                    audioSource.PlayOneShot(deleted_sound);
                    map_Controller.WriteText(enemyAngle + ">" + playerAngle);
                    Instantiate(deletedParticle, transform.position, Quaternion.identity);
                    Invoke("DestroyPlayer", 1.5f);
                    Invoke("ResetScene", 1.7f);
                }

            }
            if (map_Controller.mep_type == Map_type.None)
            {
                canMove = false;
                audioSource.PlayOneShot(deleted_sound);
                Instantiate(deletedParticle, transform.position, Quaternion.identity);
                Invoke("DestroyPlayer", 1.5f);
                Invoke("ResetScene", 1.7f);
            }
            if (map_Controller.mep_type == Map_type.Color)
            {
                bool strong = collision.gameObject.GetComponent<Enemy_manager_fallen>().playerData.color_data.isMainColor;
                bool amIStrong = player_manager.player_data_base.color_data.isMainColor;

                if (strong && amIStrong)
                {
                    audioSource.PlayOneShot(destroy_sound);
                    GameObject particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
                    map_Controller.IncreaseSlider(0.2f);
                    particle.SetActive(true);
                    Destroy(particle, 1.2f);
                    Destroy(collision.gameObject);
                }
                if (!strong && amIStrong)
                {
                    audioSource.PlayOneShot(destroy_sound);
                    GameObject particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
                    map_Controller.IncreaseSlider(0.2f);
                    particle.SetActive(true);
                    Destroy(particle, 1.2f);
                    Destroy(collision.gameObject);
                }
                if (strong && !amIStrong)
                {
                    canMove = false;
                    audioSource.PlayOneShot(deleted_sound);
                    Instantiate(deletedParticle, transform.position, Quaternion.identity);
                    Invoke("DestroyPlayer", 1.5f);
                    Invoke("ResetScene", 1.7f);
                }
            }
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded= true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("obstacles"))
        {
            canMove = false;
            audioSource.PlayOneShot(deleted_sound);
            map_Controller.WriteText("Upss"); 
            Instantiate(deletedParticle, transform.position, Quaternion.identity);
            Invoke("DestroyPlayer", 1.5f);
            Invoke("ResetScene", 1.7f);
        }
    }
    private void Jump()
    {

        if (Input.GetMouseButtonDown(0))
        { 

            if (isGrounded)
            {
                isGrounded = false;
                audioSource.PlayOneShot(jump_sound);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(new Vector2(0, 20f), ForceMode2D.Impulse); // Daha hızlı yükselme için artırıldı
                rb.gravityScale = 5f; // Daha hızlı düşmesini sağlamak için artırıldı
                GameObject particle = Instantiate(jumpParticle, transform.position, Quaternion.identity);
                particle.SetActive(true);
                Destroy(particle, 1.2f);
            }
            else
            {
                rb.AddForce(new Vector2(0, -15f), ForceMode2D.Impulse);
            }
        }
    }
    private IEnumerator DeactivateParticleAfterTime(float time)
    {
        yield return new WaitForSeconds(time);  // Belirtilen s�re kadar bekle
        destroyParticle.SetActive(false);      // Partik�l� kapat
    }

    private void DestroyPlayer()
    {
        sprite_Renderer.enabled = false;
        
    }

    private void ResetScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void EnderGame()
    {
        canMove = false;
        audioSource.PlayOneShot(deleted_sound);
        Instantiate(deletedParticle, transform.position, Quaternion.identity);
        Invoke("DestroyPlayer", 1.5f);
        Invoke("ResetScene", 1.7f);
    }


}
