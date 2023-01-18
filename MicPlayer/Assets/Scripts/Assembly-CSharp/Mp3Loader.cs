using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Mp3Loader : MonoBehaviour
{
	[Header("Mp3 Player Panel, FIle Name")]
	[SerializeField]
	private GameObject panelMP3Player;

	[SerializeField]
	private TextMeshProUGUI textFileName;

	[Header("MP3 PlayTime (Slider,Text")]
	[SerializeField]
	private Slider sliderPlaybar;

	[SerializeField]
	private TextMeshProUGUI textCurrentPlaytime;

	[SerializeField]
	private TextMeshProUGUI textMaxPlaytime;

	[SerializeField]
	public AudioSource audioSource;

	public AudioClip _audioClip;

	public bool useMicrophone;

	private string selectedDevice;

	private GameObject dialog;

	public static float[] _spectrumdata = new float[512];

	public static float[] _freqBand = new float[8];

	public static float[] _bandBuffer = new float[8];

	private float[] _bufferDecrease = new float[8];

	private void Awake()
	{
		if (useMicrophone && Microphone.devices.Length != 0)
		{
			selectedDevice = Microphone.devices[0].ToString();
			audioSource.clip = Microphone.Start(selectedDevice, true, 10, 44100);
			audioSource.Play();
		}
		if (!useMicrophone)
		{
			audioSource.clip = _audioClip;
		}
	}

	private void OnGUI()
	{
	}

	private void GetSpectrumAudioSource()
	{
		audioSource.GetSpectrumData(_spectrumdata, 0, FFTWindow.Blackman);
	}

	private void MakeFrequencyBands()
	{
		int num = 0;
		for (int i = 0; i < 8; i++)
		{
			float num2 = 0f;
			int num3 = (int)Mathf.Pow(2f, i) * 2;
			if (i == 7)
			{
				num3 += 2;
			}
			for (int j = 0; j < num3; j++)
			{
				num2 += _spectrumdata[num] * (float)(num + 1);
				num++;
			}
			num2 /= (float)num;
			_freqBand[i] = num2 * 10f;
		}
	}

	private void Update()
	{
		GetSpectrumAudioSource();
		MakeFrequencyBands();
	}

	public void OnLoad(FileInfo file)
	{
		panelMP3Player.SetActive(true);
		textFileName.text = file.Name;
		ResetPlaytimeUI();
		StartCoroutine(LoadAudio(file.FullName));
	}

	private IEnumerator LoadAudio(string fileName)
	{
		fileName = "file://" + fileName;
		UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(fileName, AudioType.MPEG);
		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.Success)
		{
			Debug.Log("Load Success : " + fileName);
			DownloadHandlerAudioClip.GetContent(request);
			AudioClip clip = ((Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.WindowsPlayer) ? DownloadHandlerAudioClip.GetContent(request) : NAudioPlayer.FromMp3Data(request.downloadHandler.data, fileName));
			audioSource.clip = clip;
			Play();
		}
		else
		{
			Debug.Log("Load Failed");
		}
	}

	public void offLoad()
	{
		Stop();
		panelMP3Player.SetActive(false);
	}

	public void Play()
	{
		audioSource.Play();
		StartCoroutine("OnPlaytimeUI");
	}

	public void Pause()
	{
		audioSource.Pause();
	}

	public void Stop()
	{
		audioSource.Stop();
		StartCoroutine("OnPlaytimeUI");
		ResetPlaytimeUI();
	}

	private void ResetPlaytimeUI()
	{
		sliderPlaybar.value = 0f;
		textCurrentPlaytime.text = "00:00:00";
		textMaxPlaytime.text = "00:00:00";
	}

	private IEnumerator OnPlaytimeUI()
	{
		while (true)
		{
			int num = (int)audioSource.time / 3600;
			int num2 = (int)(audioSource.time % 3600f) / 60;
			int num3 = (int)(audioSource.time % 3600f) % 60;
			textCurrentPlaytime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", num, num2, num3);
			num = (int)audioSource.clip.length / 3600;
			num2 = (int)(audioSource.clip.length % 3600f) / 60;
			num3 = (int)(audioSource.clip.length % 3600f) % 60;
			textMaxPlaytime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", num, num2, num3);
			sliderPlaybar.value = audioSource.time / audioSource.clip.length;
			yield return new WaitForSeconds(1f);
		}
	}
}
