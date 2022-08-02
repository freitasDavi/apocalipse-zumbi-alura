using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float velocidade = 10;
    Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public bool Vivo = true;
    // Update is called once per frame

    private void Start()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        
        direcao = new Vector3()
        {
            x = eixoX,
            z = eixoZ,
            y = 0
        };

        if (direcao != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("Movendo", true); 
        } else
        {
            GetComponent<Animator>().SetBool("Movendo", false);
        }

        if (Vivo == false)
        {
            if (Input.GetButton("Fire1"))
            {
                SceneManager.LoadScene("game");
            }
        }
    }

    void FixedUpdate()
    {
        // É necessário multiplicar pelo tempo, caso contrário andará de acordo com a quantidade de frames por Segundo
        GetComponent<Rigidbody>().MovePosition(
                GetComponent<Rigidbody>().position + (direcao * velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(raio.origin, raio.direction * 100 , Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}
