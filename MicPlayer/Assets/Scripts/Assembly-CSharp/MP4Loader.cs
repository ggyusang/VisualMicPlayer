using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class MP4Loader : MonoBehaviour
{
	[Header("MP4 Player Panel, File Name")]
	[SerializeField]
	private GameObject panelMP4Player;

	[Header("Play Video & Audio")]
	[SerializeField]
	private RawImage rawImageDrawVideo;

	[SerializeField]
	public VideoPlayer videoPlayer;

	[SerializeField]
	private AudioSource audioSource;

	public void Start()
	{
		/*if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}Mp4"))
		{
			StartCoroutine(LoadVideo(PlayerPrefs.GetString($"{SceneMang.instance.currentScene}Mp4")));
		}
		videoPlayer.loopPointReached += CheckOver;*/
    }

	private void Update()
	{

	}

	public void CheckOver(UnityEngine.Video.VideoPlayer vp)
	{
	    if(SceneMang.instance.currentScene==SceneMang.Scene.MicPlayer)
		{
			SceneMang.instance.currentScene = SceneMang.Scene.MusicPlayer;
			SceneManager.LoadScene(SceneName.MusicPlayer);
		}
		else if (SceneMang.instance.currentScene == SceneMang.Scene.MusicPlayer)
		{
			SceneMang.instance.currentScene = SceneMang.Scene.IdleScene;
			SceneManager.LoadScene(SceneName.IdleScene);
		}
	}



public void OnLoad(FileInfo file)
	{
		StartCoroutine(LoadVideo(file.FullName));
	}

	private IEnumerator LoadVideo(string fullpath)
	{
		PlayerPrefs.SetString($"{SceneMang.instance.currentScene}Mp4", fullpath);
		videoPlayer.url = "file://" + fullpath;
		videoPlayer.Play();
		videoPlayer.EnableAudioTrack(0, false);
		videoPlayer.SetTargetAudioSource(0, audioSource);
		rawImageDrawVideo.texture = videoPlayer.targetTexture;
		videoPlayer.Prepare();
		while (!videoPlayer.isPrepared)
		{
			yield return null;
		}
		Play();
	}

	public void OffLoad()
	{
		Stop();
		panelMP4Player.SetActive(false);
	}

	public void Play()
	{
		videoPlayer.Play();
	//	audioSource.Play();
		rawImageDrawVideo.gameObject.SetActive(true);
	}

	public void Pause()
	{
		videoPlayer.Pause();
		audioSource.Pause();
	}

	public void Stop()
	{
		videoPlayer.Stop();
		audioSource.Stop();
	}
}
