using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance;

	public static AudioManager GetInstance()
	{
		if (instance == null)
			instance = new AudioManager();

		return instance;
	}

	[SerializeField]
	private AudioClip bgmClip;
	private AudioSource bgmPlayer;

	[SerializeField]
	private AudioClip[] sfxClips;
	[SerializeField]
	private AudioSource[] sfxPlayers;
	[SerializeField]
	private int channels;
	private int channelIndex;
	
	

	private void Awake()
	{
		instance = this;

		InitAudio();
	}

	private void InitAudio()
	{
		GameObject bgmObj = new GameObject("bgmPlayer");
		bgmObj.transform.parent = transform;	
		bgmPlayer = bgmObj.AddComponent<AudioSource>();
		bgmPlayer.playOnAwake = false;
		bgmPlayer.loop = true;
		bgmPlayer.volume = 0.5f;
		bgmPlayer.clip = bgmClip;

		GameObject sfxObj = new GameObject("sfxPlayer");
		sfxObj.transform.parent = transform;
		sfxPlayers = new AudioSource[channels];

		for (int i = 0; i < channels; i++)
		{
			sfxPlayers[i] = sfxObj.AddComponent<AudioSource>();
			sfxPlayers[i].playOnAwake = false;
			sfxPlayers[i].loop = false;
			sfxPlayers[i].volume = 0.5f;
		}
	}

	public void PlayBgm(bool isPlay)
	{
		if (isPlay)
			bgmPlayer.Play();
		else
			bgmPlayer.Stop();
	}

	public void SetBgmVolume(float volume)
	{
		bgmPlayer.volume = volume;
	}
}
