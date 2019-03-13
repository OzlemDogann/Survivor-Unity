using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silah : MonoBehaviour {
	public GameObject[] silahlar;
	public GameObject[] oyuncuSilahlar;
	public int silahCesidi;
	public Transform oyuncuKamera;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < silahlar.Length; i++) {
			silahlar [i].SetActive (false);
		}

		
	}
	void FixedUpdate(){
		RaycastOlustur ();
	}
	void RaycastOlustur(){
		RaycastHit ray; 

		Physics.Raycast (oyuncuKamera.position, oyuncuKamera.forward, out ray, 2); //ray oluştur.
		Debug.DrawLine(oyuncuKamera.position,ray.point,Color.blue); //rayi çizdir.
		try{
			if(ray.collider.tag=="PickWeapon"){
				if (Input.GetKeyDown (KeyCode.C)) {
					int silahadi=int.Parse(ray.collider.name);
					oyuncuSilahlar[silahCesidi].SetActive(false);
					oyuncuSilahlar [3] = silahlar [silahadi];
					silahCesidi =3;
					for(int i=0;i<oyuncuSilahlar.Length;i++){
						oyuncuSilahlar[i].SetActive(false);

					}

				}
			}
		}
		catch{
		}



	}

	
	// Update is called once per frame
	void Update () {
		SilahSistemi ();
	}
		void SilahSistemi(){
		if (Input.GetAxis ("Mouse ScrollWheel")>0) {
			if (silahCesidi < oyuncuSilahlar.Length - 1) {
			if (silahCesidi < oyuncuSilahlar.Length) {
					silahCesidi++;
				for (int i = 0; i < oyuncuSilahlar.Length; i++) {
						oyuncuSilahlar [i].SetActive (false);

					}
					oyuncuSilahlar [silahCesidi].SetActive (true);
				}
			}

		}

		if (Input.GetAxis ("Mouse ScrollWheel")<0) {
			if (silahCesidi > 0) {
				silahCesidi--;
				for (int i = 0; i < oyuncuSilahlar.Length; i++) {
					oyuncuSilahlar [i].SetActive (false);

				}
				oyuncuSilahlar [silahCesidi].SetActive (true);

			}
		}

	}
	}

