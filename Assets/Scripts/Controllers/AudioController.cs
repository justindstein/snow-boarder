using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource victorySound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource oughSound;
    [SerializeField] private AudioSource umphSound;

    private List<AudioSource> ouchSounds;

    private void Awake()
    {
        this.ouchSounds = new List<AudioSource>() { this.oughSound, this.umphSound };
    }

    public void OnFinishLineCrossed(Component sender, object data)
    {
        Debug.Log(string.Format("AudioController.OnFinishLineCrossed: [sender: {0}] [data: {1}]", sender, data));
        this.playVictorySound();
    }

    //public void OnPlayerAirborn(Component sender, object data)
    //{
    //Debug.Log(string.Format("BeaconManager.OnPlayerAirborn: [sender: {0}] [data: {1}]", sender, data));
    //this.playJumpSound();
    //}

    public void OnHeadCollision(Component sender, object data)
    {
        Debug.Log(string.Format("AudioController.OnHeadCollision: [sender: {0}] [data: {1}]", sender, data));
        //this.playOuchSound();
        this.oughSound.Play();
    }

    public void playVictorySound()
    {
        this.victorySound.Play();
    }

    public void playJumpSound()
    {
        this.jumpSound.Play();
    }

    public void playOuchSound()
    {
        //Debug.Log(string.Format("AudioController.playOuchSound: [sender: {0}] [data: {1}]"));
        //int randomIndex = Random.Range(0, this.ouchSounds.Count);
        //new List<AudioSource>() { this.oughSound, this.umphSound }[randomIndex].Play();
        this.oughSound.Play();
    }

}

