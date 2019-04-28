using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject heartBone;
    public GameObject heartObject;
    public LayerMask clickField;
    public GameObject materialObject;

    public Color healthyColor;
    public Color illColor;
    SkinnedMeshRenderer matRenderer;

    private Vector3 mousePosition;

    Vector3 heartPos;
    Animator anim;
    Animator camAnim;
    Camera cam;

    private Vector3 mousePos;
    
    public static bool isTraveling = false;
    public static bool isReady = true;

    private bool gameEnded = false;

    float beatRate = 0.3f;
    float currentBeatRate;
    public static bool gameOver = false;
    public float speed = 10f;

    public static float life;
    public static int years;
    int matIndex = 0;

    AudioSource idleSound;
    public AudioSource[] hurtSounds;
    BoxCollider col;

    GameManager gm;

    AudioSource heartBeat;

    // Start is called before the first frame update
    void Start()
    {
        anim = heartObject.GetComponent<Animator>();
       // if (SceneManager.GetActiveScene().buildIndex != 0)
      //  {
        camAnim = FindObjectOfType<Camera>().GetComponent<Animator>();

        //  Debug.Log("hello!");
        heartPos = heartBone.transform.position;
        InvokeRepeating("GetMousePos", 5f, 1f);
        // InvokeRepeating("SetRandomRot", 2f, 2f);    

        col = GetComponent<BoxCollider>();
        //  col.enabled = false;
        
        matRenderer = materialObject.GetComponent<SkinnedMeshRenderer>();

        Cursor.lockState = CursorLockMode.None;

        //  healthyColor = matRenderer.materials[matIndex].color;
        life = 100f;
        isTraveling = false;
        isReady = true;

        matIndex = 0;
        currentBeatRate = beatRate;
        gameOver = false;

        cam = Camera.main;
        gm = GameObject.Find("Canvas").GetComponent<GameManager>();
       // }
        heartBeat = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {       
      //  Debug.Log(life);
      //    Debug.Log(gameOver);
        // Debug.Log(isReady);
        if (!gameOver)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
               

                if (Physics.Raycast(ray2, out hit, 100, clickField))
                {
                    //   StartCoroutine(PropelPlant(hit.point));


                    if (isReady)
                    {
                        gm.DecrementLife(8);
                        mousePos = hit.point;
                        isTraveling = true;
                        isReady = false;
                    }
                    //hit.point is the position of your mouse
                }

                Debug.Log(hit.point);
                //   Debug.DrawRay(plantHead.transform.position, hit.point, Color.green, 2f);
            }

        }
        else
        {
            //if (SceneManager.GetActiveScene().buildIndex != 0)   
            if (!gameEnded)
                StartCoroutine(GameOver());

        }
        //float timeToStart = Time.time;

        if (isTraveling)
        {
            life -= Time.deltaTime * 2.5f;
            // heartBone.transform.position = Vector3.Lerp(heartBone.transform.position, mousePos, (Time.time - timeToStart) * 0.5f);
            heartBone.transform.position = Vector3.MoveTowards(heartBone.transform.position, mousePos, Time.deltaTime * speed);

            if (Vector3.Distance(heartBone.transform.position, mousePos) < 0.2f)
                isTraveling = false;

        }
        else
        {
            life -= Time.deltaTime * 1.5f;

            if (heartBone.transform.position != heartPos)
            {
                heartBone.transform.position = Vector3.MoveTowards(heartBone.transform.position, heartPos, Time.deltaTime * speed);
                isReady = false;
  
            }
            else
            {
                isReady = true;
            }
        }

        matRenderer.materials[matIndex].color = Color.Lerp(illColor, healthyColor, life / 100);

        if (life <= 0f)
            gameOver = true;

        beatRate -= Time.deltaTime;

        if(beatRate <= 0f && !gameOver)
        {
            anim.SetTrigger("HeartBeat");
            beatRate = currentBeatRate;
        }

    }

    IEnumerator GameOver()
    {
        anim.enabled = false;
        yield return null;
    }

    void GetMousePos()
    {
        mousePosition = Input.mousePosition;
        if (life < 40f)
            currentBeatRate = 1.25f;
       // anim.speed = 1f;
    }

    public void PlayHeatBeat()
    {
        heartBeat.Play();
    }
}
