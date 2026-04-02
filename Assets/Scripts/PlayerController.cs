using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // Ekrana yazı yazdırmak için bu şart!

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    // UI Yazı Elementleri
    public TextMeshProUGUI countText; // Skor yazısı
    public TextMeshProUGUI winTextObject; // "You Win!" yazısı (Son fotoğraftaki yenilik)

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();

        // --- YENİ: Oyun başlarken "You Win!" yazısını gizle ---
        winTextObject.gameObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText(); // Skor her arttığında kontrol et
        }
    }

    // Skoru güncelleyen ve oyunun bitip bitmediğini kontrol eden fonksiyon
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        // --- YENİ: Kazanma kontrolü (Kritik Nokta!) ---
        // Sahnede kaç tane altın (PickUp) varsa buradaki sayıyı ona göre değiştir.
        // Ben bir önceki fotoğrafına bakarak 8 tane saydım, o yüzden 8 yazıyorum.
        if (count >= 8)
        {
            // Tüm altınlar toplanınca "You Win!" yazısını göster
            winTextObject.gameObject.SetActive(true);
        }
    }
}