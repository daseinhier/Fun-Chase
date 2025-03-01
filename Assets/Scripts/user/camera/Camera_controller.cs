using DG.Tweening;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool firstPart;
    public float lerpSpeed = 2f;
    private bool isLerping = true; // Lerp iþleminin yapýlýp yapýlmadýðýný takip eder

    private void Awake()
    {
        firstPart = false;
    }

    private void Update()
    {

        offset = transform.position - target.position;
        if (!firstPart)
        {         
            transform.position = new Vector3(transform.position.x - (offset.x - 6.5f), transform.position.y, transform.position.z);
        }
        else
        {
            if (isLerping)
            {
                float newY = Mathf.Lerp(transform.position.y, -44.5f, Time.deltaTime * lerpSpeed);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);

                // Kamera -44.5f'e yeterince yaklaþtýðýnda Lerp iþlemini durdur
                if (Mathf.Abs(transform.position.y - (-44.5f)) < 0.01f)
                {
                    transform.position = new Vector3(transform.position.x - (offset.x - 6.5f), -44.5f, transform.position.z);
                    isLerping = false; // Lerp iþlemi bitti
                }
            }
        }
    }
}
