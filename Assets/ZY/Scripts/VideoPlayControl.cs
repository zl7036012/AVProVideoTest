using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using UnityEngine.UI;
public class VideoPlayControl : MonoBehaviour {
	public MediaPlayer player;
	private float curPlayRate = 1;
	private Text displayCurPlayRate;
	// Use this for initialization
	void Start () {
		displayCurPlayRate = GameObject.Find ("CurrentPlayRate").GetComponent<Text> ();
		if (null == displayCurPlayRate)
			Debug.LogError ("Text DisplayComponent Not Found !");
		UpdateRateDisplay ();
		//player = this.GetComponent<MediaPlayer> ();
		player.OpenVideoFromFile (MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, "AVProVideoSamples/BigBuckBunny_720p30.mp4", false);
		if (false == player.Control.IsPlaying ())
			player.Control.Play ();
	}

	private float prePlayRate=1;
	private bool bBackward=false;
	// Update is called once per frame
	void Update () {
		if (curPlayRate != prePlayRate)
		{
			if (0 < curPlayRate)
			{
				bBackward = false;
				player.Control.SetPlaybackRate (curPlayRate);
			} 
			else
			{
				bBackward = true;
			}
			prePlayRate = curPlayRate;
		}

		if (true == bBackward)
			BackwardPlay ();
	}
	/// <summary>
	/// 反向播放
	/// </summary>
	private void BackwardPlay()
	{
		float curTime = player.Control.GetCurrentTimeMs ();
		float targetTime = curTime + Time.deltaTime*1000 * curPlayRate;
		if(0<=targetTime)
			player.Control.Seek (targetTime);
	}
	public void Pause()
	{
		player.Control.Pause ();
	}

	public void Seek()
	{
		player.Control.Seek (2000);
	}
	public void Rewind()
	{
		player.Control.Rewind ();
	}
	public void Play()
	{
		player.Control.Play ();
	}
	public void Stop()
	{
		player.Control.Stop ();
	}
	public void ForwardFast()
	{
		if (0 > curPlayRate)
		{
			curPlayRate = 1;
			return;
		}
		if (8 <= curPlayRate)
			return;
		curPlayRate *= 2;
		UpdateRateDisplay ();
	}
	public void ForwardSlow()
	{
		if (0 > curPlayRate)
		{
			curPlayRate = 1;
			return;
		}
		if (0.125 >= curPlayRate)
			return;
		curPlayRate /= 2;
		UpdateRateDisplay ();
	}
	public void BackwardFast()
	{
		if (0 < curPlayRate)
		{
			curPlayRate = -1;
			return;
		}
		if (-8 >= curPlayRate)
			return;
		curPlayRate *= 2;
		UpdateRateDisplay ();
	}
	public void BackwardSlow()
	{
		if (0 < curPlayRate) {
			curPlayRate = -1;
			return;
		}
		if (-0.125 <= curPlayRate)
			return;
		curPlayRate /= 2;
		UpdateRateDisplay ();
	}
	private void UpdateRateDisplay()
	{
		displayCurPlayRate.text = "PlayRate：" + curPlayRate.ToString ();
	}
}
