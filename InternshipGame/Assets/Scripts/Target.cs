using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour {
	
	public Text targetstate;
	public float health = 100f;
	public bool isHealthy;

	void Start(){
		targetstate.text = "Hedef sağlığı: " + health;
		isHealthy = true;
	}
		
	public void TakeDamage(float amount){
		if (health <= 0 || (health - amount) <= 0) {
			targetstate.text = "Hedef kullanılamaz durumda. Hedefi yenileyin.";
			health = 0;
		}
		else {
			health -= amount;
			targetstate.text = "Hedef sağlığı: " + health;
		}
	}
	public void RenewHealth()
	{
		health = 100f;
		targetstate.text = "Hedef sağlığı: " + health;;
	}

	public bool isDamaged()
	{
		return health <= 0;
	}

	public float getHealth()
	{
		return health;
	}

}
