using System.IO;
using UnityEngine;

public class FileLoader : MonoBehaviour
{
	private FileInfo fileInfo;

	public void OpenFile()
	{
		Application.OpenURL("file:////" + fileInfo.FullName);
	}
}
