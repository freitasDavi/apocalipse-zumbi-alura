﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float velocidade = 10;
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    private Rigidbody rigidbodyJogador;
    private Animator animatorJogador;
    public int Vida = 100;
    public ControlaInterface scriptControlaInterface;
    // Update is called once per frame

    private void Start()
    {
        Time.timeScale = 1;
        rigidbodyJogador = GetComponent<Rigidbody>();
        animatorJogador = GetComponent<Animator>();
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
            animatorJogador.SetBool("Movendo", true); 
        } else
        {
            animatorJogador.SetBool("Movendo", false);
        }

        if (Vida <= 0)
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
        rigidbodyJogador.MovePosition(
                rigidbodyJogador.position + (direcao * velocidade * Time.deltaTime));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100 , Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            rigidbodyJogador.MoveRotation(novaRotacao);
        }
    }

    public void TomarDano (int dano)
    {
        Vida -= dano;

        scriptControlaInterface.AtualizarSliderVidaJogador();

        if (Vida <= 0)
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }
    }
}
