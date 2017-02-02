using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public GameObject loadPanel;
	public GameObject startPanel;

	public void HidePanel(){
		startPanel.SetActive (false);
	}

	public void StartGame(){
		//Display load screen
		loadPanel.SetActive(true);

		SceneManager.LoadScene ("main_game");
		if (SceneManager.GetActiveScene ().name == "title_scene") {
			SoundManager.i.EndSoundFade ("Song2", 0.5f);
		} else {
			SoundManager.i.EndSoundFade ("Song1", 0.5f);
		}
	}

	public void QuitButton(){
		Application.Quit();
	}

	void Start(){
		DontDestroyOnLoad (gameObject);

		if (SceneManager.GetActiveScene().name == "title_scene") {
			SoundManager.i.PlaySound (Sound.Song2, 1f);
		} else {
			//SoundManager.i.EndSoundFade ("Song1", 0.5f);
			SoundManager.i.PlaySound (Sound.Song1, 1f);
		}
	}
}
