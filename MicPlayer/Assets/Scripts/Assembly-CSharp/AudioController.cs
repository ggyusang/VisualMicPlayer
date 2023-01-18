using TMPro;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	[SerializeField]
	private MP4Loader mp4Loader;

	public int audioChannel;

	public float audioSensibility = 70f;

	public float threshold = 100f;

	public float defaultSpeed = 1f;

	public float highLightSpeed = 4f;

	[SerializeField]
	private TMP_InputField audioChannelInput;

	[SerializeField]
	private TMP_InputField audioSensibilitylInput;

	[SerializeField]
	private TMP_InputField thresholdInput;

	[SerializeField]
	private TMP_InputField defaultSpeedInput;

	[SerializeField]
	private TMP_InputField highLightSpeedInput;

	private void Start()
	{
		audioChannelInput.onValueChanged.AddListener(delegate(string input)
		{
			SetChannel(input);
		});
		audioSensibilitylInput.onValueChanged.AddListener(delegate(string input)
		{
			SetSensibility(input);
		});
		thresholdInput.onValueChanged.AddListener(delegate(string input)
		{
			SetThres(input);
		});
		defaultSpeedInput.onValueChanged.AddListener(delegate(string input)
		{
			SetDefault(input);
		});
		highLightSpeedInput.onValueChanged.AddListener(delegate(string input)
		{
			SetHighLight(input);
		});
	}

	private void SetChannel(string input)
	{
		audioChannel = int.Parse(input);
	}

	private void SetSensibility(string input)
	{
		audioSensibility = float.Parse(input);
	}

	private void SetThres(string input)
	{
		threshold = float.Parse(input);
	}

	private void SetDefault(string input)
	{
		defaultSpeed = float.Parse(input);
	}

	private void SetHighLight(string input)
	{
		highLightSpeed = float.Parse(input);
	}

	private void Update()
	{
		if (Mp3Loader._freqBand[audioChannel] * threshold >= audioSensibility)
		{
			mp4Loader.videoPlayer.playbackSpeed = highLightSpeed;
		}
		else
		{
			mp4Loader.videoPlayer.playbackSpeed = defaultSpeed;
		}
	}
}
