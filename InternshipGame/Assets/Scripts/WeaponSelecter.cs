using UnityEngine;
using UnityEngine.UI;

public class WeaponSelecter: MonoBehaviour {

	public int gunSelect = 0;
	bool buttonDown = false;
	public Text currentGun;

	// Use this for initialization
	void Start () {
		SelectGun ();
	}

	// Update is called once per frame
	void Update () {
		int previousGun = gunSelect;

		if (Input.GetKey ("space") && !buttonDown) {
			buttonDown = true;
			if (gunSelect >= transform.childCount - 1)
				gunSelect = 0;
			else
				gunSelect++;
		}
		if (Input.GetKeyUp ("space")) {
			buttonDown = false;
		}

		if (previousGun != gunSelect)
			SelectGun ();
	}

	//set the selected gun active 
	void SelectGun()
	{
		int i = 0;
		foreach (Transform weapon in transform) {
			if (i == gunSelect) {
				weapon.gameObject.SetActive (true);
				if (i == 0)
					currentGun.text = "Silah: Desert Eagle";
				else {
					currentGun.text = "Silah: AK-47";
				}
			}
			else
				weapon.gameObject.SetActive (false);
			i++;
		}

	}
		
}
