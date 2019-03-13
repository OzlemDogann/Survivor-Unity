using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OyunKontrol : MonoBehaviour {
	public GameObject zombie;
	private float timeCounter;
	private float formation_process = 10f;
	public Text pointText;
	private int point;
    public int pointValue;

	// Use this for initialization
	void Start () {
		timeCounter = formation_process;
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter -= Time.deltaTime;
		if (timeCounter < 0) {
			Vector3 pos = new Vector3 (Random.Range(145f,364f),8.4f,Random.Range(191f,361f));
			Instantiate (zombie, pos, Quaternion.identity);
			timeCounter = formation_process;
}

	}
	public void PointIncrease(int p){
        point += p;
        pointText.text = "Point : " + point;
        
    }
   
   public void levelComplete()
    {
        if (point > pointValue)
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void gamecomplete()
    {
        if (point > pointValue)
        {
            SceneManager.LoadScene("Congratulation");
        }
    }
   
    public void OyunBitti(){
		PlayerPrefs.SetInt ("puan",point);
		SceneManager.LoadScene ("GameOverScene");
	}
}

  