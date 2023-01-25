using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class IdleScene : MonoBehaviour
{
	public Image image;

	public float threshold;

	public float weight;

	public float downSpeed;

	public Slider slider;

	[SerializeField]
	private TMP_InputField thresholdInput;

	[SerializeField]
	private TMP_InputField downSpeedInput;

	[SerializeField]
	private TMP_InputField weightInput;

	public float value;
	public void Init()
	{
		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}threshold"))
		{
			this.threshold = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}threshold");
			this.thresholdInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}threshold").ToString();
		}
		else
		{
			PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}threshold", 0.001f);
			threshold = 0.001f;
			this.thresholdInput.text = 0.001f.ToString();
		}


		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}weight"))
		{
			this.weight = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}weight");
			this.weightInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}weight").ToString();
		}
		else
		{
			PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}weight", 4);
			weight = 4;
			this.weightInput.text = 4.ToString();
		}


		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}downSpeed"))
		{
			this.downSpeed = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}downSpeed");
			this.downSpeedInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}downSpeed").ToString();
		}
		else
		{
			PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}downSpeed", 0.001f);
			downSpeed = 0.001f;
			this.downSpeedInput.text = 0.001.ToString();
		}
	}



	void Start()
	{
		thresholdInput.onValueChanged.AddListener(delegate (string input)
		{
			SetThres(input);
		});
		downSpeedInput.onValueChanged.AddListener(delegate (string input)
		{
			SetLerpTime(input);
		});
		weightInput.onValueChanged.AddListener(delegate (string input)
		{
			SetNextThres(input);
		});

		Init();
	}
	private void SetThres(string input)
	{
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}threshold", float.Parse(input));
		threshold = float.Parse(input);
	}


	private void SetLerpTime(string input)
	{
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}downSpeed", float.Parse(input));
		downSpeed = float.Parse(input);
	}

	private void SetNextThres(string input)
	{
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}weight", float.Parse(input));
		weight = float.Parse(input);
	}


	// Update is called once per frame
	void Update()
	{
		if (SpectrumKernel.spects[0]*weight >= threshold)
		{
			ChangeColor();
		}
		if(value>0)
		{
			value -= downSpeed;
			Color color = new Color(1, 1, 1, value);
			slider.value = value;
			image.color = color;
		}
	
	
	}
	public void ChangeColor()
	{
		
		value += SpectrumKernel.spects[0] * weight;
		Color color = image.color;
		color.a = value;
		image.color = color;
		slider.value = value;
		if (value>=0.99f)
		{
			SceneMang.instance.currentScene = SceneMang.Scene.MicPlayer;
			SceneManager.LoadScene(SceneName.MicPlayer);
		}
	}
}
