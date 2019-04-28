using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusScript : MonoBehaviour
{
    public string virusName;
    public int yearsTaken;
    public float heartDamage;
    public float speed;

    private bool hit = false;
    float destructTimer;

    public Quaternion prefRotation;

    private Vector3 velocity = Vector3.zero;
    Rigidbody rb;

    ParticleSystem ps;
    GameObject player;
    GameManager gm;

    AudioSource splashSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.Find("Canvas").GetComponent<GameManager>();
        if (player == null)
            Debug.Log("WTF");

        ps = GetComponent<ParticleSystem>();
        transform.rotation = Quaternion.Euler(prefRotation.x, prefRotation.y, prefRotation.z);

        splashSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, speed);
       // if (velocity == null)
        //    Debug.Log("OMG");
        //lower speed value = higher in-game speed
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            if (PlayerScript.isReady)
            {
              //  Debug.Log("WHAT");

                AffectPlayer();
            }
          //  Destroy(gameObject, 0.125f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject == player)
        {
            if (!PlayerScript.isReady)
            {
                StartCoroutine(SelfDestruct());
                splashSound.Play();
                ps.Play();
            }          
        }
    }

    IEnumerator SelfDestruct()
    {
       
            while (transform.localScale.x > 1f)
            {
                destructTimer += Time.deltaTime;
                transform.localScale -= new Vector3(4f, 4f, 4f) * Time.deltaTime * 8f;
                yield return null;

                if (transform.localScale.x <= 1f)
                    Destroy(gameObject);
            }
       
            while (transform.localScale.x > 0.02f)
            {
                destructTimer += Time.deltaTime;
                transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime * 3f;
                yield return null;

                if (transform.localScale.x <= 0.02f)
                    Destroy(gameObject);
            }

       
    }

    void AffectPlayer()
    {
        if (!PlayerScript.gameOver && !hit)
        {

            PlayerScript.life -= heartDamage;
            GameManager.yearsLived -= yearsTaken;
            splashSound.Play();

            if (heartDamage > 0)
                gm.PopUpText(virusName, 1);
            else
                gm.PopUpText(virusName, 0);

            ps.Play();
            hit = true;
            StartCoroutine(SelfDestruct());
            Destroy(gameObject, 1f);
        }
    }

}
