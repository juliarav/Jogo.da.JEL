using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public AudioSource jump;
    public AudioSource walk;  
    public AudioSource attack;

    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    private Animator anim;
    private bool Attacking;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
        UpdateAttackStatus();
    }

    void Move()
      {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0f) // Movendo para a direita
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);

            if (!walk.isPlaying) // Verifica se o som de andar não está tocando
            {
                walk.Play(); // Toca o som de andar
            }
        }
        else if (Input.GetAxis("Horizontal") < 0f) // Movendo para a esquerda
        {
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);

            if (!walk.isPlaying) // Verifica se o som de andar não está tocando
            {
                walk.Play(); // Toca o som de andar
            }
        }
        else // Quando o jogador não está se movendo
        {
            anim.SetBool("Walk", false);

            if (walk.isPlaying) // Se o som de andar estiver tocando
            {
                walk.Stop(); // Para o som de andar
            }
        }
    }
    
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                if (jump != null)
                    jump.Play();

                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("Jump", true);
            }
            else
            {
                if (doubleJump)
                {
                    if (jump != null)
                        jump.Play();

                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X)) // Se o jogador pressionar o botão de ataque (Fire1) e não estiver atacando
        {
            anim.SetTrigger("Attack"); // Ativa a animação de ataque

            if (attack != null && !attack.isPlaying) // Toca o som do ataque
            {
                attack.Play();
            }
        }
    }

     void UpdateAttackStatus()
    {
        if (Attacking && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Attacking = false; // Permite que o jogador ataque novamente
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }

        if (collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
}
