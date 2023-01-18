using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DirectorySpawner : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI textDirectoryName;

	[SerializeField]
	private Scrollbar verticalScrollbar;

	[SerializeField]
	private GameObject panelDataPrefab;

	[SerializeField]
	private Transform parentContent;

	private DirectoryController directoryController;

	private List<Data> fileList;

	public void Setup(DirectoryController controller)
	{
		directoryController = controller;
		fileList = new List<Data>();
	}

	public void UpdateDirectory(DirectoryInfo currentDirectory)
	{
		for (int i = 0; i < fileList.Count; i++)
		{
			Object.Destroy(fileList[i].gameObject);
		}
		fileList.Clear();
		textDirectoryName.text = currentDirectory.Name;
		verticalScrollbar.value = 1f;
		SpawnData("...", DataType.Directory);
		DirectoryInfo[] directories = currentDirectory.GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			SpawnData(directoryInfo.Name, DataType.Directory);
		}
		FileInfo[] files = currentDirectory.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			SpawnData(fileInfo.Name, DataType.File);
		}
		fileList.Sort((Data a, Data b) => a.FileName.CompareTo(b.FileName));
		for (int k = 0; k < fileList.Count; k++)
		{
			fileList[k].transform.SetSiblingIndex(k);
			if (fileList[k].FileName.Equals("..."))
			{
				fileList[k].transform.SetAsFirstSibling();
			}
		}
	}

	public void SpawnData(string fileName, DataType type)
	{
		GameObject obj = Object.Instantiate(panelDataPrefab);
		obj.transform.SetParent(parentContent);
		obj.transform.localScale = Vector3.one;
		obj.transform.position = parentContent.position;
		Data component = obj.GetComponent<Data>();
		component.Setup(directoryController, fileName, type);
		fileList.Add(component);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
