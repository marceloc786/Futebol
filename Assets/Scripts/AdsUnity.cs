using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdsUnity : MonoBehaviour {

    public static AdsUnity instance;
    private Button btnAds;
    public bool adsBtnAcionado = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SceneManager.sceneLoaded += PegaBtn;
    }

    void PegaBtn(Scene cena, LoadSceneMode modo)
    {
        if (OndeEstou.instance.fase != 0 && OndeEstou.instance.fase != 1 && OndeEstou.instance.fase != 2)
        {
            btnAds = GameObject.Find("AdsBtn").GetComponent<Button>();
            btnAds.onClick.AddListener(AdsBtn);
        }
    }

    void AdsBtn()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() {resultCallback = AdsAnalise});
            adsBtnAcionado = true;
        }
    }

    void AdsAnalise(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            ScoreManager.instance.ColetaMoedas(500);
        }
    }

    public void ShowAds()
    {
        if (PlayerPrefs.HasKey("AdsUnity"))
        {
            if (PlayerPrefs.GetInt("AdsUnity") == 3)
            {
                if (Advertisement.IsReady("video"))
                {
                    Advertisement.Show("video");
                }
                PlayerPrefs.SetInt("AdsUnity", 1);
            }
            else
            {
                PlayerPrefs.SetInt("AdsUnity", PlayerPrefs.GetInt("AdsUnity") + 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("AdsUnity", 1);
        }

    }
}
