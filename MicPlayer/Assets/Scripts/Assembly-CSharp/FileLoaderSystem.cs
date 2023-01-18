using System.IO;
using UnityEngine;

public class FileLoaderSystem : MonoBehaviour
{
	private Mp3Loader mp3Loader;

	private MP4Loader mp4Loader;

	private void Start()
	{
		mp3Loader = GetComponent<Mp3Loader>();
		mp4Loader = GetComponent<MP4Loader>();
	}

	public void LoadFIle(FileInfo file)
	{
		if (file.FullName.Contains(".mp3"))
		{
			mp3Loader.OnLoad(file);
		}
		else if (file.FullName.Contains(".mp4"))
		{
			mp4Loader.OnLoad(file);
		}
	}

	private void offAllPanel()
	{
		mp3Loader.offLoad();
		mp4Loader.OffLoad();
	}
}
