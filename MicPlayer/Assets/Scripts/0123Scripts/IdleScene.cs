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

	public float lerpTime;

	public Slider slider;

	[SerializeField]
	private TMP_InputField thresholdInput;

	[SerializeField]
	private TMP_InputField lerpTimeInput;

	[SerializeField]
	private TMP_InputField weightInput;

	public void Init()
	{
		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}threshold"))
		{
			this.threshold = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}threshold");
			this.thresholdInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}threshold").ToString();
		}
		else
		{
			PlayerPrefs.SetInt($"{SceneMang.instance.currentScene}threshold", 0);
			threshold = 0;
			this.thresholdInput.text = 0.ToString();
		}


		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}weight"))
		{
			this.weight = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}weight");
			this.weightInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}weight").ToString();
		}
		else
		{
			PlayerPrefs.SetInt($"{SceneMang.instance.currentScene}weight", 0);
			weight = 0;
			this.weightInput.text = 0.ToString();
		}


		if (PlayerPrefs.HasKey($"{SceneMang.instance.currentScene}lerpTime"))
		{
			this.lerpTime = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}lerpTime");
			this.lerpTimeInput.text = PlayerPrefs.GetFloat($"{SceneMang.instance.currentScene}lerpTime").ToString();
		}
		else
		{
			PlayerPrefs.SetInt($"{SceneMang.instance.currentScene}lerpTime", 0);
			lerpTime = 0;
			this.lerpTimeInput.text = 0.ToString();
		}
	}



	void Start()
	{
		thresholdInput.onValueChanged.AddListener(delegate (string input)
		{
			SetThres(input);
		});
		lerpTimeInput.onValueChanged.AddListener(delegate (string input)
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
		PlayerPrefs.SetFloat($"{SceneMang.instance.currentScene}lerpTime", float.Parse(input));
		lerpTime = float.Parse(input);
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
		else
		{
			var a = Mathf.Lerp(image.color.a, 0, lerpTime);
			Color color = new Color(1, 1, 1, a);
			slider.value = a ;
			image.color = color;
		}
	}
	public void ChangeColor()
	{
		Color color = image.color;
		color.a = SpectrumKernel.spects[0]*weight;
		image.color = color;
		slider.value = color.a;
		if (color.a>=0.99f)
		{
			SceneMang.instance.currentScene = SceneMang.Scene.MicPlayer;
			SceneManager.LoadScene(SceneName.MicPlayer);
		}
	}
}
