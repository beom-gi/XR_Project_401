using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField] protected SoundProfileData soundProfileData;
    [SerializeField] protected SoundProfileData BGMProfileData;

    private AudioManager audioManager => AudioManager.Instance;

    private void Start()
    {
        //audioManager.PlayMusic(BGMProfileData.GetRandomClip());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.FadeInMusic(BGMProfileData.GetRandomIndex(0), 3.0f);
        }

    }
}