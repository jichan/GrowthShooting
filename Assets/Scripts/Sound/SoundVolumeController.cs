using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オーディオを再生するボリュームをコントロールする
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundVolumeController : MonoBehaviour {
	AudioSource source;

	void Awake()
	{
		source = GetComponent<AudioSource>();
		if(source.playOnAwake)
		{
			SetVolume();
		}
	}
	
	/// <summary>
	/// ボリュームを設定する
	/// </summary>
	public void SetVolume()
	{
		source.volume = SoundPlayTimeManager.GetVolume(source);
		SoundPlayTimeManager.RegisterPlayTime(source);
	}
}
