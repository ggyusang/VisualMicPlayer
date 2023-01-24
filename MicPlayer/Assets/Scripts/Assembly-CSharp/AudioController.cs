using TMPro;
using UnityEngine;
using UnityEngine.Video;
public class AudioController : MonoBehaviour
{
	[SerializeField]
	private MP4Loader mp4Loader;

	[SerializeField]
	private VideoPlayer videoPlayer;

	public int audioChannel;

	public float audioSensibility;

	public float threshold;

	public float defaultSpeed;

	public float highLightSpeed;

	public float lerpTime;

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

	[SerializeField]
	private TMP_InputField lerpTimeInput;

	public void Init()
	{
		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}audioChannel"))
		{
			this.audioChannel = PlayerPrefs.GetInt($"{SceneMang.instance.currentScene}audioChannel");
			this.audioChannelInput.text= PlayerPrefs.GetInt($"{SceneMang.instance.currentScene}audioChannel").ToString();
		}
		else
		{
			PlayerPrefs.SetInt($"{SceneMang.instance.currentScene}audioChannel", 0);
			audioChannel = 0;
			this.audioChannelInput.text = 0.ToString();
		}

		////
		
		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}audioSensibility"))
		{
			this.audioSensibility = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}audioSensibility");
			this.audioSensibilitylInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}audioSensibility").ToString();
		}
		else
		{
			PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}audioSensibility", 70);
			audioSensibility = 70;
			this.audioSensibilitylInput.text = 70.ToString();
		}

		////

		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}threshold"))
		{
			this.threshold = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}threshold");
			this.thresholdInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}threshold").ToString();
		}
		else
		{
			PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}threshold", 100);
			threshold = 100;
			this.thresholdInput.text = 100.ToString();
		}

		////

		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}defaultSpeed"))
		{
			this.defaultSpeed = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}defaultSpeed");
			this.defaultSpeedInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}defaultSpeed").ToString();
		}
		else
		{
			PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}defaultSpeed", 1);
			defaultSpeed = 1;
			this.defaultSpeedInput.text = 1.ToString();
		}

		////

		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}highLightSpeed"))
		{
			this.highLightSpeed = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}highLightSpeed");
			this.highLightSpeedInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}highLightSpeed").ToString();
		}
		else
		{
			PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}highLightSpeed", 5);
			highLightSpeed = 5;
			this.highLightSpeedInput.text = 5.ToString();
		}

		////

		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}lerpTime"))
		{
			this.lerpTime = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}lerpTime");
			this.lerpTimeInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}lerpTime").ToString();
		}
		else
		{
			PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}lerpTime", 0.7f);
			lerpTime = 0.7f;
			this.lerpTimeInput.text = 0.7.ToString();
		}
	}
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
		lerpTimeInput.onValueChanged.AddListener(delegate (string input)
		{
			SetLerpTime(input);
		});

		this.Init();
	}

	private void SetChannel(string input)
	{		    
		PlayerPrefs.SetInt($"{SceneMang.instance.currentScene}audioChannel", int.Parse(input));
		audioChannel = int.Parse(input);
	}

	private void SetSensibility(string input)
	{
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}audioSensibility", float.Parse(input));
		audioSensibility = float.Parse(input);
	}

	private void SetThres(string input)
	{
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}threshold", float.Parse(input));
		threshold = float.Parse(input);
	}

	private void SetDefault(string input)
	{
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}defaultSpeed", float.Parse(input));
		defaultSpeed = float.Parse(input);
	}

	private void SetHighLight(string input)
	{
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}highLightSpeed", float.Parse(input));
		highLightSpeed = float.Parse(input);
	}

	private void SetLerpTime(string input)
	{
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}lerpTime", float.Parse(input));
		lerpTime = float.Parse(input);
	}


	private void ChageSpeed()
	{
		videoPlayer.playbackSpeed = highLightSpeed;
	}
	private void Update()
	{
		if (SpectrumKernel.spects[audioChannel] * threshold >= audioSensibility)
		{
			ChageSpeed();
		}
		else
		{
			videoPlayer.playbackSpeed = Mathf.Lerp(videoPlayer.playbackSpeed, defaultSpeed,lerpTime);
		}

	
	}
}
