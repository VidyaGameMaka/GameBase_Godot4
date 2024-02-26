using Godot;
using System;
using System.Collections.Generic;

public partial class Sound : Node2D {

    //Declare static instance field
    public static Sound instance;

    //Declare node fields
	private AudioStreamPlayer musicPlayer;
    private AudioStreamPlayer voicePlayer;
    private AudioStreamPlayer malePlayer;
    private AudioStreamPlayer femalePlayer;
    private List<AudioStreamPlayer> sfxPlayers = new List<AudioStreamPlayer>();

    //Which music is currently playing?
    private string currentMusicPlaying = string.Empty;

    //Is Voice Playing?
    public bool voiceIsPlaying { get; private set; } = false;

    public override void _Ready() {
        //Assign reference to static field.
        instance = this;

        //Assign references to nodes
        musicPlayer = GetNode<AudioStreamPlayer>("music_AudioStreamPlayer");
        voicePlayer = GetNode<AudioStreamPlayer>("voice_AudioStreamPlayer");
        malePlayer = GetNode<AudioStreamPlayer>("male_AudioStreamPlayer");
        femalePlayer = GetNode<AudioStreamPlayer>("female_AudioStreamPlayer");     

        //Assign references to sfxPlayers
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer1"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer2"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer3"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer4"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer5"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer6"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer7"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer8"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer9"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer10"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer11"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer12"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer13"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer14"));
        sfxPlayers.Add(GetNode<AudioStreamPlayer>("sfx_AudioStreamPlayer15"));
    }

    public override void _Process(double delta) {
        //Update field we are going to share for voice
        voiceIsPlaying = voicePlayer.Playing;
    }

    /// <summary>
    /// Play Music on the music channel. This is normally handled by: RequiredSceneComponents.tscn
    /// </summary>
    /// <param name="myPath"></param>
    public void PlayMusic(string myPath) {
        if (currentMusicPlaying == myPath) { return; }
        currentMusicPlaying = myPath;
        musicPlayer.Stream = GD.Load<AudioStream>(myPath); ;
        musicPlayer.Play();
    }

    /// <summary>
    /// Stop music playing on the music channel.
    /// Be sure to check your import settings in Godot to enable looping.
    /// </summary>
    public void StopMusic() { musicPlayer.Stop(); }

    /// <summary>
    /// Play Voice on the Voice Channel
    /// </summary>
    /// <param name="myPath"></param>
    public void PlayVoice(string myPath) {
        voicePlayer.Stream = GD.Load<AudioStream>(myPath);
        voicePlayer.Play();
    }

    /// <summary>
    /// Play Voice on the Male Channel
    /// </summary>
    /// <param name="myPath"></param>
    public void PlayMale(string myPath) {
        malePlayer.Stream = GD.Load<AudioStream>(myPath);
        malePlayer.Play();
    }

    /// <summary>
    /// Play Voice on the Female Channel
    /// </summary>
    /// <param name="myPath"></param>
    public void PlayFemale(string myPath) {
        femalePlayer.Stream = GD.Load<AudioStream>(myPath);
        femalePlayer.Play();
    }

    /// <summary>
    /// Play a sound effect on the SFX channel. Up to 15
    /// sound effects can be played at the same time.
    /// </summary>
    /// <param name="myPath"></param>
    public void PlaySFX(string myPath) {
        //Find an available player to play
        foreach (var player in sfxPlayers) {
            if (player.Playing == false) {
                player.Stream = GD.Load<AudioStream>(myPath);
                player.Play();
            }
        }
    }

}
