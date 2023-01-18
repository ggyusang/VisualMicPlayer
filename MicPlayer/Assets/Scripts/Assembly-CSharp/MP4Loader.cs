using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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

	public void OnLoad(FileInfo file)
	{
		StartCoroutine(LoadVideo(file.FullName));
	}

	private IEnumerator LoadVideo(string fullpath)
	{
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
		audioSource.Play();
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
