using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Zombie : MonoBehaviour {
	public GameObject heart;
	private Transform goal;
	private int zombiePoint=10;
	public int zombieLife = 4;
	private float distance;
	private OyunKontrol oControl;
	private AudioSource aSource;
	private bool zombieDead = false;
  //  private Gun scontroller;
	// Use this for initialization
	void Start () {
		aSource = GetComponent<AudioSource> ();
		oControl = GameObject.Find ("_Scripts").GetComponent<OyunKontrol> ();
       // scontroller = GameObject.Find("Player").GetComponent<Gun>();
        goal = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.LookAt (goal);
		GetComponent<Rigidbody> ().AddForce (transform.forward * 6f, ForceMode.Acceleration);
		distance = Vector3.Distance (transform.position,goal.position);
		if (distance < 10f) {
			if (!aSource.isPlaying)
				aSource.Play ();
			if(!zombieDead)
			GetComponentInChildren<Animation> ().Play ("Zombie_Attack_01");
		} else {
			if (aSource.isPlaying)
				aSource.Stop ();
		}
	}
    
	void OnCollisionEnter(Collision c){
	
		if (c.collider.gameObject.tag.Equals ("mermi")) {

            zombieLife--;

            if (zombieLife==0){
				zombieDead = true;
				oControl.PointIncrease(zombiePoint);
				Instantiate (heart, transform.position, Quaternion.identity);
				GetComponentInChildren<Animation> ().Play ("Zombie_Death_01");
				Destroy (this.gameObject,1.667f);
			}

		}
	}
}
