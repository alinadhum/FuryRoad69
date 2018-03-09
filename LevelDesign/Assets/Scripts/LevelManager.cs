using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	public GameObject quitMenu_popup;
	public Button PlayText;
	public Button OptionText;
	public Button ExitText;
	public AudioClip SecMenuOpen;
	public AudioClip SecMenuClose;
    [SerializeField]
	private int scene;
    [SerializeField]
	private Canvas Loading;

    void Start ()

	{
		Loading.enabled = false;
		quitMenu_popup.SetActive (false);
	}

	public void ExitPress() 

	{
		quitMenu_popup.SetActive (true);
		GetComponent<AudioSource> ().PlayOneShot (SecMenuOpen);
		PlayText.enabled = false;
		OptionText.enabled = false;
		ExitText.enabled = false;

	}

	public void NoPress() 

	{
		quitMenu_popup.SetActive (false);
		GetComponent<AudioSource> ().PlayOneShot (SecMenuClose);
		PlayText.enabled = true;
		OptionText.enabled = true;
		ExitText.enabled = true;
	}

	public void LoadGame(string name) {
		Application.LoadLevel (name);
		StartCoroutine(LoadNewScene());
		Loading.enabled = true;
	}

	public void QuitGame(){
		Application.Quit ();
	}

	IEnumerator LoadNewScene()
	{
		AsyncOperation async = Application.LoadLevelAsync(scene);

		// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
		while (!async.isDone)
		{
			yield return null;
		}

	}
}
