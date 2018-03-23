using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotacao : MonoBehaviour {

    //Posição Seta
    [SerializeField] private Transform posStart;
    //Seta
    [SerializeField] private Image setaImg;
    //Angulo
    public float zRotate;

	// Use this for initialization
	void Start () {
        PosicionaSeta();
        PosicionaBola();
    }
	
	// Update is called once per frame
	void Update () {
        RotacionaSeta();
        InputDeRotacao();
    }

    void PosicionaSeta()
    {
        setaImg.rectTransform.position = posStart.position;
    }

    void PosicionaBola()
    {
        this.gameObject.transform.position = posStart.position;
    }

    void RotacionaSeta()
    {
        setaImg.rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    void InputDeRotacao()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            zRotate += 2.5f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            zRotate -= 2.5f;
        }
    }
}
