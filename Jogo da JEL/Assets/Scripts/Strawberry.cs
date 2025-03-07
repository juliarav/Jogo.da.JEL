using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{
    private SpriteRenderer sprite;
    private CircleCollider2D cc;
    private AudioSource audioSource;

    public GameObject collected;
    public int Score;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>(); // Obtém o componente de som
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            sprite.enabled = false;
            cc.enabled = false;
            collected.SetActive(true);

            GameController.instance.totalScore += Score;
            GameController.instance.UpdateScoreText();

            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play(); // Toca o som da coleta
                Destroy(gameObject, audioSource.clip.length); // Espera o som terminar antes de destruir
            }
            else
            {
                Destroy(gameObject, 0.25f); // Tempo padrão caso não haja som
            }
        }
    }
}
