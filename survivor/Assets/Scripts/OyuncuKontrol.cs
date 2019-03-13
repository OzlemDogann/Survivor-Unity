using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class OyuncuKontrol : MonoBehaviour {
	public AudioClip shotVoice, deadVoice, takelifeVoice, injuryVoice;
	public Transform bulletPos;
	public GameObject bullet;
	public GameObject bang;
	public Image life_imaj;
	public OyunKontrol oControl;
	private float life_value  = 100f;
    private AudioSource aSource;
    FirstPersonController fpc;
    private float hungry_value = 100f;
    public Image hungry_imaj;
    public float sayac = 3f;



    // Use this for initialization
    void Start () {
      
		aSource = GetComponent<AudioSource> ();

        oControl = GameObject.Find("_Scripts").GetComponent<OyunKontrol>();

        fpc = GameObject.FindObjectOfType<FirstPersonController>();
        if (SceneManager.GetActiveScene ().buildIndex>PlayerPrefs.GetInt("whichlevel")){
			PlayerPrefs.SetInt ("whichlevel", SceneManager.GetActiveScene ().buildIndex);
		}
	}
   
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F) || Input.GetMouseButtonDown(0)) {
			aSource.PlayOneShot (shotVoice, 1f);
			GameObject go = Instantiate (bullet, bulletPos.position, bulletPos.rotation) as GameObject;
			GameObject goPatlama = Instantiate (bang, bulletPos.position, bulletPos.rotation) as GameObject;
			go.GetComponent<Rigidbody> ().velocity = bulletPos.transform.forward * 10f;
			Destroy (go.gameObject, 2f);
			Destroy (goPatlama.gameObject, 2f);
		}
        if (fpc.m_WalkSpeed != 0 && fpc.m_RunSpeed != 0)
        {
            sayac -= Time.deltaTime;
            if (sayac < 0)
            {
                sayac = 3f;
                fpc.m_WalkSpeed -= 0.01f;
                fpc.m_RunSpeed -= 0.01f;

            }
            // fpc.m_WalkSpeed -= Time.deltaTime* 0.01f;
            // fpc.m_RunSpeed -= Time.deltaTime *0.01f; 
            hungry_value -=  0.01f;
            float y = hungry_value / 100f;
            hungry_imaj.fillAmount = y;
            hungry_imaj.color = Color.Lerp(Color.red, Color.green, y);

            if (hungry_value < 0)
            {
               fpc.m_WalkSpeed = 1;
               fpc.m_RunSpeed = 1;
           }
        }

    }

  
	void OnCollisionEnter(Collision c){
		if (c.collider.gameObject.tag.Equals ("zombi")) {
			aSource.PlayOneShot (injuryVoice, 1f);
			life_value  -= 10f;
			float x = life_value  / 100f;
			life_imaj.fillAmount = x;
			life_imaj.color = Color.Lerp (Color.red, Color.green, x);
			if (life_value  <= 0) {
				aSource.PlayOneShot (deadVoice, 1f);
				oControl.OyunBitti ();
			}
		}
    }
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag.Equals ("kalp")) {
			aSource.PlayOneShot (takelifeVoice, 1f);
			if(life_value <100f)
				life_value  += 10f;
			float x = life_value  / 100f;
			life_imaj.fillAmount = x;
			life_imaj.color = Color.Lerp (Color.red, Color.green, x);
			Destroy (c.gameObject);
		}

        if (c.gameObject.tag.Equals("food"))
        {
            aSource.PlayOneShot(takelifeVoice, 1f);
            fpc.m_WalkSpeed += Time.deltaTime;
            fpc.m_RunSpeed += Time.deltaTime;

            if (hungry_value < 100f)
            {
                hungry_value +=  0.01f; 
                float y = hungry_value/100f;
                hungry_imaj.fillAmount = y;
                hungry_imaj.color = Color.Lerp(Color.red, Color.green, y);
                Destroy(c.gameObject);
               
            }

       
        }
        if (c.gameObject.tag.Equals("levelcomplete"))
        {
            oControl.levelComplete();
        }
        if (c.gameObject.tag.Equals("gamecomplete"))
        {
            oControl.gamecomplete();
        }
	}
}
       