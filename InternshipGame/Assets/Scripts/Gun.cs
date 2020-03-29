using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {

	public float damage = 5f;
	public float totalClips = 30f;
	public float reloadTime = 5f;
	public float accuracy = 0.04f;
	public float shootingTime = 0.15f;
	public bool autoShoot = true;

	public float clips;

	public Camera cam;
	public GameObject targ;
	public Text shootresult;
	public Text bullets;
	public Text fired;
	public ParticleSystem flash;

	bool didshoot = false;
	bool renewal = false;
	bool clipfill = false;
	bool waiting = false;
	bool result;
	bool clipNumberEnabled;

	void Start()
	{
		clips = totalClips;
		shootresult.enabled = false;
		clipNumberEnabled = true;
		bullets.text = clips.ToString ();
		fired.enabled = false;
		fired.text = "Ateş edildi.";
	}

	// Update is called once per frame
	void Update () {

		//recharge the gun
		if (Input.GetKey ("r") && !clipfill) {
			clipfill = true;
			clipNumberEnabled = false;
			bullets.text = "Şarjör dolduruluyor...";
			Invoke ("recharge", reloadTime);
		}

		if (Input.GetKeyUp ("r"))
			clipfill = false;


		if (clipNumberEnabled) {
			if (clips <= 0)
				bullets.text = "Mermileriniz bitti. Şarjörü doldurun.";
			else
				bullets.text = "Kalan mermi sayısı: " + clips.ToString ();
		} 

		//shoot
		if (Input.GetKey ("s") && clips > 0 && !didshoot && !(targ.GetComponent<Target> ().isDamaged ()) && !waiting) { 
			fired.enabled = true;
			shootresult.enabled = false;
			flash.Play ();
			if (autoShoot) {
				didshoot = false;
			} else {
				didshoot = true;
			} 
			result = shoot ();
			waiting = true;
			StartCoroutine ("Wait");
		}

		if (Input.GetKeyUp ("s")) {
			didshoot = false;
		}
			
		//renew target
		if (Input.GetKey ("t") && !renewal) {
			renewal = true;
			targ.GetComponent<Target> ().RenewHealth ();
		}

		if (Input.GetKeyUp ("t")) {
			renewal = false;
		}
	
	}

	//fire the gun
	public bool shoot() {
		clips--;

		RaycastHit hit;

		Vector3 direction = cam.transform.forward;
		direction.x += Random.Range (-accuracy, accuracy);
		direction.y += Random.Range (-accuracy, accuracy);
		direction.z += Random.Range (-accuracy, accuracy);

		if (Physics.Raycast (cam.transform.position, direction, out hit)) {
			Target target = hit.transform.GetComponent<Target> ();
			if (target != null) {
				return true;
			} else {
				return false;
			}
		} 
		return false;
	}

	//recharge the gun
	public void recharge()
	{
		clipNumberEnabled = true;
		clips = totalClips;
	}

	//print whether the bullet hit the target or not
	public void printResult(bool hit)
	{
		shootresult.enabled = true;
		fired.enabled = false;
		if(hit)
		{
			targ.GetComponent<Target> ().TakeDamage (damage);
			shootresult.text = "Hedef vuruldu.";
		}
		else
			shootresult.text = "Hedefe isabet edilmedi.";
		waiting = false;
	}

	//to adjust shooting time
	IEnumerator Wait()
	{
		yield return new WaitForSecondsRealtime (shootingTime);
		printResult (result);
	}

}

