using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainPanel;
    
    public Slider soundsVolSlider;
    public Slider musicVolSlider;

    public string[] tips;
    int tipIndex;
    public Text tipText;
    public Text recordText;

    public AudioMixer audioMixer;

    public int record = 0;

    // Start is called before the first frame update
    void Start()
    {
        float valueSound;
        audioMixer.GetFloat("Sounds", out valueSound);
        soundsVolSlider.value = valueSound;

        float valueMusic;
        audioMixer.GetFloat("Music", out valueMusic);
        musicVolSlider.value = valueMusic;

        PlayerPrefs.GetInt("Record", record);
        record = PlayerPrefs.GetInt("Record", record);
        recordText.text = record.ToString();

        Debug.Log(record);
    }

    // Update is called once per frame
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSoundVolume(float volume)
    {
        audioMixer.SetFloat("Sounds", volume);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {

        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
       

        tipIndex = Random.Range(0, tips.Length);
        tipText.text = "Tip: " + tips[tipIndex];     
    }

    public void HideOptions()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }



}
