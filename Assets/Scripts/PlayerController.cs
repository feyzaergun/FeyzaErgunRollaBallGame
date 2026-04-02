using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Hareket Ayarları
    public float speed = 10f;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // UI Ayarları
    public TextMeshProUGUI countText;
    public GameObject winObject;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Başlangıç değerleri
        count = 0;
        SetCountText();
        winObject.SetActive(false);
    }

    // Yeni Input System hareketi
    void OnMove(InputValue movementValue)
    {
        Vector2 v = movementValue.Get<Vector2>();
        movementX = v.x;
        movementY = v.y;
    }

    void FixedUpdate()
    {
        // Karakteri hareket ettir
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    // İksirleri (Magic Tubes) toplama
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        // 8 tane toplayınca kazandın yazısı çıksın
        if (count >= 8)
        {
            winObject.SetActive(true);
        }
    }
}