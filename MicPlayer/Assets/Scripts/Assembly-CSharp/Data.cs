using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Data : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerClickHandler, IPointerExitHandler
{
	[SerializeField]
	private Sprite[] spriteIcons;

	private Image imageIcon;

	private TextMeshProUGUI textDataName;

	private DataType dataType;

	private string fileName;

	private int maxFileNameLength = 25;

	public DirectoryController directoryController;

	public string FileName
	{
		get
		{
			return fileName;
		}
	}

	public void Setup(DirectoryController controller, string fileName, DataType dataType)
	{
		directoryController = controller;
		imageIcon = GetComponentInChildren<Image>();
		textDataName = GetComponentInChildren<TextMeshProUGUI>();
		this.fileName = fileName;
		this.dataType = dataType;
		imageIcon.sprite = spriteIcons[(int)this.dataType];
		textDataName.text = this.fileName;
		if (fileName.Length >= maxFileNameLength)
		{
			textDataName.text = fileName.Substring(0, maxFileNameLength);
			textDataName.text += "..";
		}
		SetTextColor();
	}

	private void SetTextColor()
	{
		if (dataType == DataType.Directory)
		{
			textDataName.color = Color.yellow;
		}
		else
		{
			textDataName.color = Color.white;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		textDataName.color = Color.red;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		directoryController.UpdateInputs(FileName);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		SetTextColor();
	}
}
