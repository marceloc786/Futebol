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
    public bool liberaRot = false;
    public bool liberaTiro = false;

	// Use this for initialization
	void Start () {
        PosicionaSeta();
        PosicionaBola();
    }
	
	// Update is called once per frame
	void Update () {
        RotacionaSeta();
        InputDeRotacao();
        LimitaRotacao();
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
        if (liberaRot == true)
        {
            float moveY = Input.GetAxis("Mouse Y");

            if(zRotate < 90)
            {
                if (moveY > 0)
                {
                    zRotate += 2.5f;
                }
            }
            
            if(zRotate > 0)
            {
                if (moveY < 0)
                {
                    zRotate -= 2.5f;
                }
            }
            
        }
    }

    void LimitaRotacao()
    {
        if(zRotate >= 90)
        {
            zRotate = 90;
        }
        if(zRotate <= 0)
        {
            zRotate = 0;
        }
    }

    void OnMouseDown()
    {
        liberaRot = true;
    }

    void OnMouseUp()
    {
        liberaRot = false;
        liberaTiro = true;
    }
}
