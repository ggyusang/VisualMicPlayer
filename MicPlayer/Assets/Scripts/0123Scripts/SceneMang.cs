using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMang : MonoBehaviour
{
	public static SceneMang instance = null;
	public Scene currentScene ;
	public enum Scene
	{
		SettingScene = 0,
		IdleScene,
		MicPlayer,
		MusicPlayer
	}

	private void Awake()
	{
		if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
		{
			instance = this; //내자신을 instance로 넣어줍니다.
			DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
		}
		else
		{
			if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
				Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
		}
	}


	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Z))
		{
			SceneManager.LoadScene(SceneName.SettingScene);
			currentScene = Scene.SettingScene;
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			SceneManager.LoadScene(SceneName.IdleScene);
			currentScene = Scene.IdleScene;
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			SceneManager.LoadScene(SceneName.MicPlayer);
			currentScene = Scene.MicPlayer;
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			SceneManager.LoadScene(SceneName.MusicPlayer);
			currentScene = Scene.MusicPlayer;
		}

	}
}

public class SceneName
{
	public const string SettingScene = "SettingScene";
	public const string IdleScene = "IdleScene";
	public const string MicPlayer = "MicPlayer";
	public const string MusicPlayer = "MusicPlayer";
	

}


