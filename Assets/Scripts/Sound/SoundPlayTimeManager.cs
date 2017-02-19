using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドの鳴っている時間や数を管理する
/// </summary>
public static class SoundPlayTimeManager {

	/// <summary>
	/// オーディオデータを管理するためのクラス
	/// </summary>
	class AudioData
	{
		public int clipInstanceId;
		public float startTime;
		public float endTime;
		public AudioSource audio;
	}

	// 最小ボリューム
	static readonly float minVolume = 0.1f;

	// 再生時間のリスト
	static List<AudioData> playTimeList = new List<AudioData>();

	/// <summary>
	/// 再生時間の登録
	/// </summary>
	/// <param name="source">再生するデータ</param>
	/// <param name="playTime">何秒後に再生開始するのか</param>
	public static void RegisterPlayTime(AudioSource source, float playTime = 0)
	{
		AudioData data = new AudioData();
		data.audio = source;
		data.startTime = Time.time + playTime;
		data.endTime = Time.time + playTime + source.clip.length;
		data.clipInstanceId = source.clip.GetInstanceID();

		playTimeList.Add(data);
	}

	/// <summary>
	/// 再生し終わったものは削除する
	/// </summary>
	public static void RemovePlayTime()
	{
		for(int i = playTimeList.Count - 1; i >= 0; i--)
		{
			if(playTimeList[i].endTime - Time.time < 0)
			{
				playTimeList.RemoveAt(i);
			}
		}
	}

	/// <summary>
	/// 現在登録されている曲データリストの中から
	/// 現在再生されている数を数えてそれに応じた音量を返す
	/// </summary>
	/// <param name="audio"></param>
	/// <returns></returns>
	public static float GetVolume(AudioSource audio)
	{
		RemovePlayTime();
		int currentAudioCount = playTimeList.Count((playTime) => playTime.clipInstanceId == audio.clip.GetInstanceID());
		return Mathf.Max(Mathf.Pow(2.0f, -currentAudioCount), minVolume);
	}
}
