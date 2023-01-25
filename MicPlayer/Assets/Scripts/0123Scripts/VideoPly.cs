using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class VideoPly : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource musicAudioSource;
    // Start is called before the first frame update
    void Start()
    {
	
		videoPlayer.loopPointReached += CheckOver;
		if (SceneMang.instance.currentScene == SceneMang.Scene.MusicPlayer)
		{ StartCoroutine(WaitForSound()); }

	}


	public IEnumerator WaitForSound()
    {
        yield return new WaitUntil(() => musicAudioSource.isPlaying == false);
        SceneMang.instance.currentScene = SceneMang.Scene.IdleScene;
        SceneManager.LoadScene(SceneName.IdleScene);
    }
        public void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        if (SceneMang.instance.currentScene == SceneMang.Scene.MicPlayer)
        {
            SceneMang.instance.currentScene = SceneMang.Scene.MusicPlayer;
            SceneManager.LoadScene(SceneName.MusicPlayer);
        }
     /*   else if (SceneMang.instance.currentScene == SceneMang.Scene.MusicPlayer)
        {
            SceneMang.instance.currentScene = SceneMang.Scene.IdleScene;
            SceneManager.LoadScene(SceneName.IdleScene);
        }*/
    }
}
