using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class SoundManager {
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    public void Init() {
        GameObject root = GameObject.Find("@Sound");

        if (root == null) {
            root = new GameObject("@Sound");
            Object.DontDestroyOnLoad(root);
        }

        string[] soundNames = Enum.GetNames(typeof(Define.Sound));
        for (int i = 0; i < soundNames.Length - 1; i++) {
            GameObject go = new GameObject {name = soundNames[i]};
            _audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = root.transform;
        }
        
        _audioSources[(int)Define.Sound.Bgm].loop = true;
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) {
        AudioClip audioClip = Manager.Resource.Load<AudioClip>($"Sounds/{path}");
        if (audioClip is null) {
            Debug.Log($"AudioClip {path} is not founded");
            return;
        }
        
        Play(audioClip, type, pitch);
    } 
    
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) {
        if (type == Define.Sound.Bgm) {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying) {
                audioSource.Stop();
            }
            
            audioSource.pitch = pitch; 
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch; 
            audioSource.PlayOneShot(audioClip);
        }
    }
}
