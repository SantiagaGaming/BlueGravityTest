using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;
    [SerializeField] private AudioClip _coinSound, _tradeSound;
    private AudioSource _audioSource;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        _audioSource= GetComponent<AudioSource>();    
    }
    public void PlayCoinSound()
    {
        _audioSource.PlayOneShot(_coinSound);
    } public void PlayTradeSound()
    {
        _audioSource.PlayOneShot(_tradeSound);
    }
}