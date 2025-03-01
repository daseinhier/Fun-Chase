using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Map_type
{
    Geometry,
    Color,
    None
}


public class Map_controller : MonoBehaviour
{
    public Map_type mep_type;

    public GameObject geometry;
    public GameObject colorful;
    public GameObject none;

    private AudioSource audioSource;
    public AudioClip dice_sound;

    public TextMeshProUGUI message;

    public Slider irregularity_bar;
    private float sliderDecreaseRate;
    public AudioClip winSound;
    public TextMeshProUGUI notify;

    private void Awake()
    {
        irregularity_bar.value = 1;
        message.text = "";
        notify.text = "";
        audioSource= GameObject.FindGameObjectWithTag("audiosource").GetComponent<AudioSource>();
        sliderDecreaseRate = 1f / 15;
        GetNewWorld();

    }

    private void Update()
    {
        if (irregularity_bar.value > 0)
        {
            irregularity_bar.value -= sliderDecreaseRate * Time.deltaTime;
        }
        if (irregularity_bar.value == 0)
        {
            GameObject.FindGameObjectWithTag("camera_holder").GetComponent<Camera_holder>().EndGamerShake();
        }

    }

    public void IncreaseSlider(float var)
    {
        
        if (irregularity_bar.value < 1)
        {
            audioSource.PlayOneShot(winSound);
            irregularity_bar.value += var;
            notify.text = "+" + var;
            Invoke("CleanText", 1f);
        }
    }
    public void CleanText()
    {
        notify.text = "";
    }

    public void GetNewWorld()
    {
        // Enum deðerlerini diziye çevir
        Map_type[] values = (Map_type[])System.Enum.GetValues(typeof(Map_type));

        // Rastgele bir indeks seç
        int randomIndex = Random.Range(0, values.Length);

        // Seçilen deðeri ata
        mep_type = values[randomIndex];

        if (mep_type == Map_type.Geometry)
        {
            ShowGeometry();
        }
        if (mep_type == Map_type.Color)
        {
            ShowColor();
        }
        if (mep_type == Map_type.None)
        {
            ShowNone();
        }


    }
    public void ShowGeometry()
    {

        audioSource.PlayOneShot(dice_sound);

        geometry.GetComponent<RectTransform>().DOScale(1.2f, 1.5f);
        geometry.GetComponent<Image>().DOFade(1, 0.3f);
        colorful.GetComponent<Image>().DOFade(0.3f, 0.1f);
        none.GetComponent<Image>().DOFade(0.3f, 0.1f);
    }

    public void ShowColor()
    {
        audioSource.PlayOneShot(dice_sound);

        colorful.GetComponent<RectTransform>().DOScale(1.2f, 1.5f);
        colorful.GetComponent<Image>().DOFade(1, 0.3f);
        geometry.GetComponent<Image>().DOFade(0.3f, 0.1f);
        none.GetComponent<Image>().DOFade(0.3f, 0.1f);
    }

    public void ShowNone()
    {
        audioSource.PlayOneShot(dice_sound);

        none.GetComponent<RectTransform>().DOScale(1.2f, 1.5f);
        none.GetComponent<Image>().DOFade(1, 0.3f);
        colorful.GetComponent<Image>().DOFade(0.3f, 0.1f);
        geometry.GetComponent<Image>().DOFade(0.3f, 0.1f);
    }


    public void WriteText(string mes)
    {
        message.text = mes;
    }
}
