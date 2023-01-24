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
		if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
		{
			instance = this; //���ڽ��� instance�� �־��ݴϴ�.
			DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
		}
		else
		{
			if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
				Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
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


