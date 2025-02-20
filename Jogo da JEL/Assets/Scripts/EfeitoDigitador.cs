using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EfeitoDigitador : MonoBehaviour
{
    private TextMeshProUGUI componentTexto;
    private AudioSource _audioSource;
    private string mensagemOriginal;
    public bool imprimindo;

    public float tempoEntreLetras = 0.08f;

    private void Awake () 
    {
        TryGetComponent (out componentTexto);
        TryGetComponent (out _audioSource);
        mensagemOriginal = componentTexto.text;
        componentTexto.text = "";
    }

    private void OnEnable () 
    {
        ImprimirMensagem (mensagemOriginal);
    }

    private void OnDisable () 
    {
        componentTexto.text = mensagemOriginal;
        StopAllCoroutines ();
    }

    public void ImprimirMensagem (string msg) 
    {
        if (gameObject.activeInHierarchy) 
        {
            if (imprimindo) return;
            imprimindo = true;
            StartCoroutine (LetraPorLetra(msg));
            
        }
    }

    IEnumerator LetraPorLetra (string msg)
    {
       string mensagem = "";
       foreach (var letra in msg) 
       {
          mensagem += letra;
          componentTexto.text = mensagem;
          _audioSource.Play ();
          yield return new WaitForSeconds (tempoEntreLetras);
       }

       imprimindo = false;
       StopAllCoroutines ();
    }



}
