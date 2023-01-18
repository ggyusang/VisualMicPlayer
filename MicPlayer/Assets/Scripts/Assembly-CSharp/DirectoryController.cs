using System;
using System.IO;
using UnityEngine;

public class DirectoryController : MonoBehaviour
{
	[SerializeField]
	private FileLoaderSystem fileLoaderSystem;

	private DirectoryInfo defaultDirectory;

	private DirectoryInfo currentDirectory;

	private DirectorySpawner directorySpawner;

	private void Awake()
	{
		Application.runInBackground = true;
		directorySpawner = GetComponent<DirectorySpawner>();
		directorySpawner.Setup(this);
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		defaultDirectory = new DirectoryInfo(folderPath);
		currentDirectory = new DirectoryInfo(folderPath);
		UpdateDirectory(currentDirectory);
	}

	private void Start()
	{
	}

	private void UpdateDirectory(DirectoryInfo directory)
	{
		currentDirectory = directory;
		directorySpawner.UpdateDirectory(currentDirectory);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UpdateDirectory(defaultDirectory);
		}
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			MoveToParentFolder(currentDirectory);
		}
	}

	private void MoveToParentFolder(DirectoryInfo directory)
	{
		if (directory.Parent != null)
		{
			UpdateDirectory(directory.Parent);
		}
	}

	public void UpdateInputs(string data)
	{
		if (data.Equals("..."))
		{
			MoveToParentFolder(currentDirectory);
			return;
		}
		DirectoryInfo[] directories = currentDirectory.GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			if (data.Equals(directoryInfo.Name))
			{
				UpdateDirectory(directoryInfo);
				return;
			}
		}
		FileInfo[] files = currentDirectory.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			if (data.Equals(fileInfo.Name))
			{
				fileLoaderSystem.LoadFIle(fileInfo);
			}
		}
	}
}
