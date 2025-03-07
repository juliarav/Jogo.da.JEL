using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFasePoint : MonoBehaviour
{
    public string faseName;
    private AudioSource audioSource;

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            SceneManager.LoadScene (faseName);
        }
        
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        
    }
}
