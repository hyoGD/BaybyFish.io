using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] private Image musicOn;
    [SerializeField] private Image musicOff;
    [SerializeField] private Image soundOn;
    [SerializeField] private Image soundOff;

    public bool music = false;
    public bool sound = false;


    private void Start()
    {
        sound = PlayerPrefs.GetInt("SOUND") == 0;
        music = PlayerPrefs.GetInt("MUSIC") == 0;
        MusicOn();
        SoundOn();
        GameController.controller.Onmusic();
    }

    #region settingMusic and Sound
    public void MusicOn()
    {
        if (!music)
        {
            musicOff.gameObject.SetActive(true);
            musicOn.gameObject.SetActive(false);
            GameController.controller.music.volume = 0;

        }
        else
        {
            musicOff.gameObject.SetActive(false);
            musicOn.gameObject.SetActive(true);
            GameController.controller.music.volume = 0.1f;

        }
        music = !music;
        PlayerPrefs.SetInt("MUSIC", music ? 1 : 0);
    }

    public void SoundOn()
    {
        if (!sound)
        {
            soundOff.gameObject.SetActive(true);
            soundOn.gameObject.SetActive(false);

            GameController.controller.sound_eat.volume = 0;
            GameController.controller.sound_fight.volume = 0;
            GameController.controller.sound_rush.volume = 0;
            GameController.controller.sound_warning.volume = 0;

        }
        else
        {
            soundOff.gameObject.SetActive(false);
            soundOn.gameObject.SetActive(true);

            GameController.controller.sound_fight.volume = 0.05f;
            GameController.controller.sound_eat.volume = 1;
            GameController.controller.sound_rush.volume = 1;
            GameController.controller.sound_warning.volume = 1;
        }
        sound = !sound;
        PlayerPrefs.SetInt("SOUND", sound ? 1 : 0);
    }
    #endregion


}
