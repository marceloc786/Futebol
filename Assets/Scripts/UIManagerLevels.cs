using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLevels : MonoBehaviour {

    [SerializeField] private Text moedasLevels;

	// Use this for initialization
	void Start () {
        ScoreManager.instance.UpdateScore();
        moedasLevels.text = PlayerPrefs.GetInt("moedasSave").ToString();
	}
}
