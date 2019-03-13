using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuControl : MonoBehaviour {


	GameObject levels, locks;

	void Start () {
        // PlayerPrefs.DeleteAll();
		locks = GameObject.Find ("locks");                  //objeler bulundu.

		levels = GameObject.Find ("levels");               //objeler bulundu.

		//level buttonu seçilmediği için leveller  ve kilitlerin görünürlülüğü kapatıldı (false).
		for(int i=0;i<levels.transform.childCount;i++){                     
			levels.transform.GetChild (i).gameObject.SetActive (false);

		}
		for(int i=0;i<locks.transform.childCount;i++){
			locks.transform.GetChild (i).gameObject.SetActive (false);

		}


		//kaçınca levelse o level buttonu aktif edildi.PlayPrefs ile level kaydedildi. 
		for (int i = 0; i < PlayerPrefs.GetInt ("whichlevel"); i++) {
			levels.transform.GetChild (i).GetComponent<Button> ().interactable = true;

		}

	}
	public void selectbutton(int btn){
		if (btn == 1) {
        
			SceneManager.LoadScene ( PlayerPrefs.GetInt ("whichlevel"));

		} else if (btn == 2) {
			for(int i=0;i<locks.transform.childCount;i++){
				locks.transform.GetChild (i).gameObject.SetActive (true);
			}
			for(int i=0;i<levels.transform.childCount;i++){
				levels.transform.GetChild (i).gameObject.SetActive (true);
			}
			//for döngüsü kayıt kadar döndürülür ve gelinen kayıda kadar olan levelların kilit görünürlükleri kaldırılır.(false)
			for (int i = 0; i < PlayerPrefs.GetInt ("whichlevel"); i++) {
				locks.transform.GetChild (i).gameObject.SetActive (false);
			}
		} else if (btn == 3) {
			Application.Quit ();
		}
	} 
	public void levelsbutton(int levelno){ 
		SceneManager.LoadScene (levelno);
	}
}




































