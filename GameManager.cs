using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public Text yearsText;
    public Text illnessText;
    public Text deathText;
    public static int yearsLived = 0;
    Animator anim;
    public bool gameEnded = false;
    
    public AudioSource music;

    public Button musicButton;
    public Button soundButton;



    // Start is called before the first frame update
    void Start()
    {
        yearsLived = 0;
        InvokeRepeating("IncrementLife", 1f, 0.45f);
        anim = GetComponent<Animator>();
        gameEnded = false;
    }

    // Update is called once per fram

    void IncrementLife()
    {
        if (!PlayerScript.gameOver)
        {
            yearsLived++;
            yearsText.text = "Years Lived: " + yearsLived.ToString();
        }
        else
        {
            if (!gameEnded)
                StartCoroutine(GameOver());
        }
    }
    
    public void DecrementLife(int value)
    {
        yearsLived -= value;
        yearsText.text = "Years Lived: " + yearsLived.ToString();
        //anim
    }

    public void PopUpText(string bacteriaName, int colorIndex)
    {
        illnessText.text = bacteriaName + "!";
        if (colorIndex == 0)
        {
            illnessText.color = Color.green;
        }
        else
        {
            illnessText.color = Color.magenta;
        }
        anim.SetTrigger("PopUp");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    /*
    public void StopMusic(bool choice)
    {
        if (choice)
        {
            music.enabled = true;
            musicButton.image.overrideSprite = musicButton.spriteState.highlightedSprite;
        }
        else
        {
            musicButton.image.overrideSprite = musicButton.spriteState.pressedSprite;
            music.enabled = false;
        }

    }
    */
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator GameOver()
    {
        gameEnded = true;

        yield return new WaitForSeconds(1.5f);      
        anim.SetTrigger("GameOver");

        if (yearsLived > PlayerPrefs.GetInt("Record"))
        {
            anim.SetTrigger("NewRecord");
            PlayerPrefs.SetInt("Record", yearsLived);
        }


        if (yearsLived < 75)
        {
            if (yearsLived < 35)
            {
                deathText.text = "You lived for a mere " + yearsLived.ToString() + " years ... RIP.";
            }
            else
            {
                deathText.text = "You lived for an average " + yearsLived.ToString() + " years ... Could have been more.";
            }

        }
        else
        {
            if (yearsLived < 95)
            {
                deathText.text = "You lived for " + yearsLived.ToString() + " years! Pretty long!";
            }
            else
            {
                if (yearsLived < 120)
                {
                    deathText.text = "You lived for a whole " + yearsLived.ToString() + " years! That is incredible!";
                }
                else
                {
                    if (yearsLived < 175)
                    {
                        deathText.text = "You have been alive for " + yearsLived.ToString() + " years! An amazing achievement!";
                    }
                    else
                    {
                        deathText.text = "You have survived on this planet for " + yearsLived.ToString() + " years. You have proven science wrong. You beat life.";
                    }

                }

            }




        }
    }


}
