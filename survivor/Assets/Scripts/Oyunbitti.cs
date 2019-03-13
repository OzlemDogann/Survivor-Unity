using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Oyunbitti : MonoBehaviour {
	public Text point;
	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		point.text ="S C O R E : " +PlayerPrefs.GetInt ("puan");
	}
	
	// Update is called once per frame
	public void YenidenOyna(){
        SceneManager.LoadScene(PlayerPrefs.GetInt("whichlevel"));
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
