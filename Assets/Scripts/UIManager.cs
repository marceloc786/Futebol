using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
    private Text pontosUI, bolasUI;
    [SerializeField] private GameObject losePanel, winPanel, pausePanel;
    [SerializeField] private Button pauseBtn, pauseBtnReturn;
    [SerializeField] private Button btnNovamenteLose, btnLevelLose;  //Botoes de lose
    [SerializeField] private Button btnNovamenteWin, btnLevelWin, btnAvancaWin; //Botoes de win
    public int moedasNumAntes, moedasNumDepois, moedasResultado;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += Carrega;
        PegaDados();
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        PegaDados();
    }

    void PegaDados()
    {
        if (OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2)
        {
            //Elementos da UI pontos e bolas
            pontosUI = GameObject.Find("PontosUI").GetComponent<Text>();
            bolasUI = GameObject.Find("BolasUI").GetComponent<Text>();
            //Paineis
            losePanel = GameObject.Find("LosePainel");
            winPanel = GameObject.Find("WinPainel");
            pausePanel = GameObject.Find("PausePainel");
            //Botoes do pause
            pauseBtn = GameObject.Find("pause").GetComponent<Button>();
            pauseBtnReturn = GameObject.Find("Pause").GetComponent<Button>();
            //Botoes do lose
            btnNovamenteLose = GameObject.Find("NovamenteLose").GetComponent<Button>();
            btnLevelLose = GameObject.Find("MenuFasesLose").GetComponent<Button>();
            //Botoes do win
            btnLevelWin = GameObject.Find("MenuFasesWin").GetComponent<Button>();
            btnNovamenteWin = GameObject.Find("NovamenteWin").GetComponent<Button>();
            btnAvancaWin = GameObject.Find("AvancarWin").GetComponent<Button>();
            //Eventos pause
            pauseBtn.onClick.AddListener(Pause);
            pauseBtnReturn.onClick.AddListener(PauseReturn);
            //Eventos you Lose
            btnNovamenteLose.onClick.AddListener(JogarNovamente);
            btnLevelLose.onClick.AddListener(Levels);
            //Eventos you win
            btnLevelWin.onClick.AddListener(Levels);
            btnNovamenteWin.onClick.AddListener(JogarNovamente);
            btnAvancaWin.onClick.AddListener(ProximaFase);

            moedasNumAntes = PlayerPrefs.GetInt("moedasSave");
        }
    }

    public void StartUI()
    {
        LigaDesligaPainel();
    }

    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.bolasNum.ToString();
        moedasNumDepois = ScoreManager.instance.moedas;
    }

    public void GameOverUI()
    {
        losePanel.SetActive(true);
    }

    public void WinGameUI()
    {
        winPanel.SetActive(true);
    }

    void LigaDesligaPainel()
    {
        StartCoroutine(Tempo());
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        pausePanel.GetComponent<Animator>().Play("MovUI_Pause");
        Time.timeScale = 0;
    }

    void PauseReturn()
    {
        pausePanel.GetComponent<Animator>().Play("MovUI_PauseR");
        Time.timeScale = 1;
        StartCoroutine(EsperaPause());
    }

    IEnumerator EsperaPause()
    {
        yield return new WaitForSeconds(0.8f);
        pausePanel.SetActive(false);
    }

    IEnumerator Tempo()
    {
        yield return new WaitForSeconds(0.001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    void JogarNovamente()
    {
        if (GameManager.instance.win == false && AdsUnity.instance.adsBtnAcionado == true) {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            AdsUnity.instance.adsBtnAcionado = false;
        }
        else if(GameManager.instance.win == false && AdsUnity.instance.adsBtnAcionado == false)
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            moedasResultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(moedasResultado);
            moedasResultado = 0;
        }
        else
        {
            SceneManager.LoadScene(OndeEstou.instance.fase);
            moedasResultado = 0;
        }
    }

    void Levels()
    {
        if (GameManager.instance.win == false && AdsUnity.instance.adsBtnAcionado == true)
        {
            AdsUnity.instance.adsBtnAcionado = false;
            SceneManager.LoadScene(1);
        }
        else if(GameManager.instance.win == false && AdsUnity.instance.adsBtnAcionado == false)
        {
            moedasResultado = moedasNumDepois - moedasNumAntes;
            ScoreManager.instance.PerdeMoedas(moedasResultado);
            moedasResultado = 0;
            SceneManager.LoadScene(1);
        }
        else
        {
            moedasResultado = 0;
            SceneManager.LoadScene(1);
        }
    }

    void ProximaFase()
    {
        if(GameManager.instance.win == true)
        {
            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene(temp);
        }
    }
}
