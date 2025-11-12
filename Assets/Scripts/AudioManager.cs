using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;

#region singleton
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                UnityEngine.Debug.LogError("Audio Manager ist null. Nicht gut.");
            }
            return _instance;
        }
    }

    public AudioSource CurrentTrack;
    public AudioSource PreviousTrack;
    public AudioSource FootstepsSFX;

    public AudioSource[] SFXChannels = new AudioSource[4];

    private bool isSwitchingTrack;

    public AudioSource[] Soundtrack = new AudioSource[9];
    public AudioClip[] KeyClankSounds = new AudioClip[4];


    void Awake()
    {
        _instance = this;
        CurrentTrack = Soundtrack[0];
    }

    void Update()
    {
        if (isSwitchingTrack)
        {
            LowerVolumeOnEverySingleTrackButTheCurrentOneCauseOneOfTheWarpsIsDumbAndStupid(Time.deltaTime);
            CurrentTrack.volume += 1 * Time.deltaTime;
            if (CurrentTrack == PreviousTrack || CurrentTrack.volume >= 0.5f && PreviousTrack.volume <= 0)
            {
                isSwitchingTrack = false;
            }
        }
        for (int i = 0; i < SFXChannels.Length; i++) // Empties SFX from channel if finished playing
        {
            if (!SFXChannels[i].isPlaying)
            {
                SFXChannels[i].clip = null;
            }
        }
    }

    public void PlaySFX(AudioClip cleep)
    {
        int i = 0;
        while (i < 4) // Only plays when all 4 channels aren't still playing a sound
        {
            if (SFXChannels[i].clip == cleep)
            { i = 3; }
            else if (SFXChannels[i].clip == null)
            {
                SFXChannels[i].clip = cleep;
                SFXChannels[i].Play();
                i = 3;
            }
            i++;
        }
    }

    public void PlayFootsteps()
    {
        if (!FootstepsSFX.isPlaying)
        {
            FootstepsSFX.Play();
        }
    }

    public void StopFootsteps()
    {
        if (FootstepsSFX.isPlaying)
        {
            FootstepsSFX.Stop();
        }
    }


    public void FadeToTrack(string roomName)
    {
        if (CurrentTrack != Soundtrack[5])
        {
            int roomIndex = 0;
            if (roomName.Equals("RoomSet 1"))
            { roomIndex = 1; }
            else if (roomName.Equals("RoomSet 2"))
            { roomIndex = 2; }
            else if (roomName.Equals("RoomSet 3"))
            { roomIndex = 3; }
            else if (roomName.Equals("SexPuzzleRoomTemplate"))
            { roomIndex = 4; }
            else if (roomName.Equals("Söder Reveal"))
            { roomIndex = 5; }

            PreviousTrack = CurrentTrack;
            CurrentTrack = Soundtrack[roomIndex];

            isSwitchingTrack = true;
        }

    }

    private void LowerVolumeOnEverySingleTrackButTheCurrentOneCauseOneOfTheWarpsIsDumbAndStupid(float delta)
    {
        for (int i = 0; i < 6; i++)
        {
            if (Soundtrack[i] != CurrentTrack)
            { Soundtrack[i].volume -= 1 * delta; }
        }
    }

    public void StopMusicOnSexPuzzle()
    {
        if (CurrentTrack == Soundtrack[4])
        {
            CurrentTrack.volume = 0;
        }
    }

    public void PlayKeyClank()
    {
        System.Random rand = new System.Random();
        int randomIndex = rand.Next(0, 3);
        PlaySFX(KeyClankSounds[randomIndex]);
    }
}
#endregion