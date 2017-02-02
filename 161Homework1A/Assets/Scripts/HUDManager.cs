using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public static HUDManager i;

	//Player resources
	float playerHealth = 100;
	float playerEnergy = 100;
	public Slider healthSlider;
	public Slider energySlider;

	//Death objects
	public GameObject deathPanel;
	public GameObject winPanel;
	public Text winText;

	//Timer
	float timer;
	public Text timerText;

	public float goalDistance;
	public Text progressText;

	void Awake(){
		i = this;
	}

	void Start(){
		healthSlider.value = playerHealth;
		energySlider.value = playerEnergy;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		UpdateText ();
		UpdateProgress ();
	}

	void UpdateText(){
		timerText.text = timer.ToString("F1") + "s";
	}

	public void modifyHealth(float value){
		playerHealth += value;
		healthSlider.value = playerHealth;

		if (healthSlider.value <= 0) {
			Death ();
		}
	}

	public void modifyEnergy(float value){
		playerEnergy += value;
		energySlider.value = playerEnergy;
	}

	//When player dies, display death menu
	public void Death (){
		Debug.Log ("Death");
		MovementController.i.isDead = true;
		deathPanel.SetActive (true);
	}

	void UpdateProgress(){
		playerEnergy = Player.i.gameObject.transform.position.z / goalDistance * 100;
		energySlider.value = playerEnergy;
		progressText.text = "Progress: " + (energySlider.value * 3.88f).ToString("F1") + " ft / " + goalDistance + " ft";

		if (energySlider.value >= 100 && winPanel.activeSelf == false) {
			winPanel.SetActive (true);
			winText.text = "Final Time: " + timer.ToString ("F1") + "s";
		}
	}

}
