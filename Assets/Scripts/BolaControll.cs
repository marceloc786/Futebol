using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControll : MonoBehaviour {

    //Seta
    public GameObject setaGO;
    //Angulo
    public float zRotate;
    public bool liberaRot = false;
    public bool liberaTiro = false;
    //Forca
    private Rigidbody2D bola;
    public float force = 0;
    public GameObject seta2Img;
    //Paredes
    private Transform paredeE, paredeD;
    //MorteBola anim
    [SerializeField] private GameObject morteBolaAnim;

    void Awake()
    {
        setaGO = GameObject.Find("Seta");
        seta2Img = setaGO.transform.GetChild(0).gameObject;
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
        paredeE = GameObject.Find("ParedeE").GetComponent<Transform>();
        paredeD = GameObject.Find("ParedeD").GetComponent<Transform>();
    }

    // Use this for initialization
    void Start()
    {
        //Forca
        bola = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotacionaSeta();
        InputDeRotacao();
        LimitaRotacao();
        PosicionaSeta();
        //Forca
        AplicaForca();
        ControlaForca();
        //Parede
        Paredes();
    }

    void PosicionaSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.position = transform.position;
    }

    void RotacionaSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    void InputDeRotacao()
    {
        if (liberaRot == true)
        {
            float moveY = Input.GetAxis("Mouse Y");

            if (zRotate < 90)
            {
                if (moveY > 0)
                {
                    zRotate += 2.5f;
                }
            }

            if (zRotate > 0)
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
        if (zRotate >= 90)
        {
            zRotate = 90;
        }
        if (zRotate <= 0)
        {
            zRotate = 0;
        }
    }

    void OnMouseDown()
    {
        if(GameManager.instance.tiro == 0)
        {
            liberaRot = true;
            setaGO.GetComponent<Image>().enabled = true;
            seta2Img.GetComponent<Image>().enabled = true;
        }

    }

    void OnMouseUp()
    {
        liberaRot = false;
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
        if (GameManager.instance.tiro == 0 && force > 0)
        {
            liberaTiro = true;
            seta2Img.GetComponent<Image>().fillAmount = 0;
            AudioManager.instance.SonsFXToca(1);
            GameManager.instance.tiro = 1;
        }

    }

    //Forca
    void AplicaForca()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (liberaTiro == true)
        {
            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
        }
    }

    void ControlaForca()
    {
        if (liberaRot == true)
        {
            float moveX = Input.GetAxis("Mouse X");
            if (moveX < 0)
            {
                seta2Img.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000f;
            }
            if (moveX > 0)
            {
                seta2Img.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
                force = seta2Img.GetComponent<Image>().fillAmount * 1000;
            }
        }
    }

    void BolaDinamica()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void Paredes()
    {
        if(this.gameObject.transform.position.x > paredeD.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
        if (this.gameObject.transform.position.x < paredeE.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("morte"))
        {
            Instantiate(morteBolaAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.bolasNum -= 1;
        }

        if (other.gameObject.CompareTag("win"))
        {
            GameManager.instance.win = true;
            int temp = OndeEstou.instance.fase - 2;
            temp++;
            PlayerPrefs.SetInt("Level"+temp, 1);
        }
    }
}
