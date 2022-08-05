using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade;
    private Rigidbody rigidbodyBala;
    public AudioClip SomDeMorte;

    void Start()
    {
        rigidbodyBala = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rigidbodyBala.MovePosition(
            rigidbodyBala.position + (transform.forward * Velocidade * Time.deltaTime)
        );    
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);
            ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        }

        Destroy(gameObject);
    }
}
