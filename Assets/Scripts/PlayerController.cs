using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Yeni kontrol sistemi için bu şart!

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Topun hızı (Inspector'dan değiştirebilirsin)
    private Rigidbody rb;

    // X ve Y eksenindeki hareket değerlerini tutacak değişkenler
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody bileşenini rb değişkenine atıyoruz
    }

    // Klavyeden (WASD veya Ok tuşları) gelen girdiyi okuyan fonksiyon
    void OnMove(InputValue movementValue)
    {
        // Gelen girdiyi 2 boyutlu bir vektör olarak al
        Vector2 movementVector = movementValue.Get<Vector2>();

        // X ve Y değerlerini değişkenlerimize kaydet
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Fizik hesaplamaları için Update yerine FixedUpdate kullanıyoruz
    void FixedUpdate()
    {
        // Y eksenini (yukarı/aşağı) 0 tutarak X ve Z ekseninde hareket vektörü oluşturuyoruz
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Topa gücü (hızı) uyguluyoruz
        rb.AddForce(movement * speed);
    }
}
