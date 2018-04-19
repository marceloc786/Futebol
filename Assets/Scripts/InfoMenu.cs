using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour {

    private Animator info;
    private AudioSource musica;
    public Sprite somLigado, somDesligado;
    private Button btnSom;

    void Start()
    {
        info = GameObject.FindGameObjectWithTag("menuInfo").GetComponent<Animator>() as Animator;
        musica = GameObject.Find("AudioManager").GetComponent<AudioSource>() as AudioSource;
        btnSom = GameObject.Find("Som").GetComponent<Button>() as Button;
    }

    public void AnimaInfoPositivo()
    {
        info.Play("AnimaInfo");
    }

    public void AnimaInfoNegativo()
    {
        info.Play("AnimaInfoInverte");
    }

    public void LigaDesligaSom()
    {
        musica.mute = !musica.mute;

        if(musica.mute == true)
        {
            btnSom.image.sprite = somDesligado;
        }
        else
        {
            btnSom.image.sprite = somLigado;
        }
    }

    public void Facebook()
    {
        Application.OpenURL("www.facebook.com/");
    }
}
