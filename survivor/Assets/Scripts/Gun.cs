using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
	public GameObject[] guns;
	public GameObject[] playerGuns;
	public int gunKind;
	public Transform FirstPersonCharacter;
	public GameObject[] gunPrefab;
	public Vector3[] bulletCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	void Update () {
		 GunSystem();
		Shoot ();
        bulletchange();

    }
   
    void FixedUpdate(){
		 RaycastCreate();
  
	}
   
 
        void bulletchange() {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int gunBulletKind = int.Parse(playerGuns[gunKind].name);
            Animation anim = guns[gunBulletKind].GetComponent<Animation>();

            anim["Reload"].wrapMode = WrapMode.Once;
            anim.Play("Reload");


            int manyBulletsfill = (int)(bulletCount[gunBulletKind].y - bulletCount[gunBulletKind].x);
            for(int i = 0; i < manyBulletsfill; i++)

            {
                if (bulletCount[gunBulletKind].z > 0)
                {
                    bulletCount[gunBulletKind].x++;
                    bulletCount[gunBulletKind].z--;
                }
            } 
        }
    }
    void Shoot()
    {
        if (gunKind == 0 || gunKind == 1 ||gunKind==2|| gunKind == 6)
        {
            if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.F) )      //mouse veya F tuşuna basılırsa
            {
                int gunBulletKind = int.Parse(playerGuns[gunKind].name);        

                Animation anim = guns[gunBulletKind].GetComponent<Animation>();
              
                if (anim.IsPlaying("Fire") || anim.IsPlaying("Reload") || anim.IsPlaying("Ready"))
                {
                    return;
                }

                if (bulletCount[gunBulletKind].x < 1)
                {
                    return;
                }
                bulletCount[gunBulletKind].x -= 1;

                switch (gunBulletKind) {
                    case 0:
                        ;
                        break;
                    case 2: anim["Fire"].speed = 3;
                        break;
                   
                }
                anim["Fire"].wrapMode = WrapMode.Once;
                anim.Play("Fire");

            }
        }
        else {
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.F) ) {
                int gunBulletKind = int.Parse(playerGuns[gunKind].name);
                Animation anim = guns[gunBulletKind].GetComponent<Animation>();
               

                if (anim.IsPlaying("Fire")|| anim.IsPlaying("Reload")|| anim.IsPlaying("Ready"))
                {
                    return;
                }
                if (bulletCount[gunBulletKind].x < 1) {
                    return;
                }

                bulletCount[gunBulletKind].x -= 1;

                switch (gunBulletKind)
                {
                    case 3: anim["Fire"].speed = 4;
                        
                        break;
                    case 4: anim["Fire"].speed = 7;
                        break;
                    case 5: anim["Fire"].speed = 7;
                        break;

                }
                anim["Fire"].wrapMode = WrapMode.Once;
                anim.Play("Fire");
            }
        }
        
    }
   
    
	void RaycastCreate(){
            if (Input.GetKeyDown(KeyCode.C))
            {
            try
            {
                RaycastHit ray;
                Physics.Raycast(FirstPersonCharacter.position, FirstPersonCharacter.forward, out ray, 5);                                         //ray oluştur.

                Debug.DrawLine(FirstPersonCharacter.position, ray.point, Color.blue);                                                             //ray çizdir. 

                if (ray.collider.tag == "PickWeapon")                        //PickWeapon isimli tag yerdeki silahların tag'ı.
                {

                    int gunName = int.Parse(ray.collider.name);                    //ışının temas ettiği alınabilir silahın adını , int' e çevirir.
                    int guntype = int.Parse(playerGuns[3].name);
                    GameObject gn = (GameObject)Instantiate(gunPrefab[3], FirstPersonCharacter.position, Quaternion.identity);       //silah oluştur.
                    gn.name = guntype.ToString();

                    playerGuns[gunKind].SetActive(false);
                    playerGuns[3] = guns[gunName];
                    gunKind = 3;

                    for (int i = 0; i < playerGuns.Length; i++)
                    {

                        playerGuns[i].SetActive(false);

                    }
                    playerGuns[gunKind].SetActive(true);
                    Destroy(ray.collider.gameObject);
                }
            }
            catch
            {

            }
            }
                
		
	}
	void GunSystem(){

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {                                                  //mouse tekerleğini ileri itersek
			if (gunKind < playerGuns.Length - 1) {
				gunKind++;	                                                                           //silah çeişidini arttir.
				for (int i = 0; i < playerGuns.Length; i++) {                                          //döngüyü başlat.
					playerGuns [i].SetActive (false);	                                               //oyuncu silahları değişkeninin bütün objelerini inaktif yap.
				}
				playerGuns [gunKind].SetActive (true);                                                 //silah çeşidini ait olan objeyi aç.

			}

		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {                                                  //mouse tekerliğini geri itersek
			if (gunKind > 0) {
				gunKind--;							                                            		//silah çeşidini azalt.
				for (int i = 0; i < playerGuns.Length; i++) {	                                       //döngüyü başlat.
					playerGuns [i].SetActive (false);	                                           	//Oyuncu silahlar değişkeninin bütün objelerini aktif yap.
				}
				playerGuns [gunKind].SetActive (true);		                                         //silah çeşidini ait olan objeyi aç.

			}
		}
	}
}
