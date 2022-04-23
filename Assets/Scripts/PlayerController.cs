using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float mySpeedX;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onGround;
    private bool canDoubleJump;
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked;
    [SerializeField] float currentAttackTimer;
    [SerializeField] float defaultAttackTimer;
    private Animator myAnimator;
    [SerializeField] int arrowNumber;
    //[SerializeField] Text arrowNumberText;
    [SerializeField] AudioClip dieMusic;
    [SerializeField] GameObject winPanel, losePanel;
    public GamEController gameController;
    public AudioSource ses;
    public AudioClip[] sesler;
    public bool sol;
    public bool sag;

    // Start is called before the first frame update
    void Start()
    {
        attacked = false;
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        //arrowNumberText.text = arrowNumber.ToString();
        gameController = GetComponent<GamEController>();
        ses = GetComponent<AudioSource>();
    }

    // Update is called once per frame


    void Update()
    {
       
       

        #region karakterin saða ve sola gitme kodu
        if (sol)
        {
            mySpeedX = -1;
        }

        if(sag)
        {
            mySpeedX = 1;
        }

        if(sol == false && sag == false)
        {
            mySpeedX = 0;
        }

     //mySpeedX = Input.GetAxis("Horizontal");
        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));
        myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y);

        #endregion


       



        #region playerýn sað ve sol hareket yönüne göre yüzünün dönmesi
        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion


        #region playerýn zýplamasýnýn kontrol edilmesi

        #endregion


        #region playerýn ok atmasýnýn kontrolü
        /* if(Input.GetMouseButtonDown(0) && arrowNumber > 0)
         {
             if(attacked == false)
             {
                 attacked = true;
                 myAnimator.SetTrigger("Attack");
                 Invoke("Fire", 0.5f);
             }


         }*/
        #endregion

        #region 
        if (attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;

        }
        else
        {
            currentAttackTimer = defaultAttackTimer;
        }
        if(currentAttackTimer <= 0)
        {
            attacked = false;
        }
        #endregion

      if (Input.GetKeyDown(KeyCode.Space)) { 

            if (onGround == true)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            canDoubleJump = true;
            myAnimator.SetTrigger("Jump");
        }
        else
        {
            if (canDoubleJump == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                canDoubleJump = false;
            }
        }

    } 
    }

    #region iptal edilen kod

    /* public void Hareket_Baslasin(bool sol_yonde_mi)
     {
         if(sol_yonde_mi)
         {
             if (transform.localScale.x > 0)
             {
                 transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                 hiz *= -1;
             }

         }
         else
         {
             if (transform.localScale.x < 0)
             {
                 transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                 hiz *= -1;
             }

         }
         hareket_ediyor = true;
     }

     public void Hareket_Bitsin()
     {
         hareket_ediyor = false;
     } */


    #endregion

  /*  #region ok atma kodu, kod iptal edildi
    void Fire()
    {
        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);
        okumuz.transform.parent = GameObject.Find("Arrows").transform;

        if (transform.localScale.x > 0)
        {
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
        }
        else
        {
            Vector3 okumuzScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);
        }

        arrowNumber--;
       // arrowNumberText.text = arrowNumber.ToString();
    }
    #endregion */

    #region karakterin canvar ile temas etmesi durumunda ölme fonksiyonun baþlamasý ve bitiþ rozetinin alýnmasý ile oyunun bitmesi
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    #endregion

    public void Die()
    {
        //GameObject.Find("Sound Controller").GetComponent<AudioSource>().clip = null;
        //GameObject.Find("Sound Controller").GetComponent<AudioSource>().PlayOneShot(dieMusic);
        myAnimator.SetFloat("Speed", 0);
        myAnimator.SetTrigger("Die");
        //myBody.constraints = RigidbodyConstraints2D.FreezePosition;
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;

        enabled = false;
        //losePanel.SetActive(true); //losePanel.active = true;
        //Time.timeScale = 0;
        StartCoroutine(Wait(false));
    }

    IEnumerator Wait(bool win)
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0;
        if (win==true)
        {
            winPanel.SetActive(true); //winPanel.active = true;
        }
        else
        {
            losePanel.SetActive(true);
        }
       
    }



     #region kaarkterin zýplama kodu
        public void zipla()
        {


                if (onGround == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    canDoubleJump = true;
                    myAnimator.SetTrigger("Jump");
                }
                else
                {
                    if (canDoubleJump == true)
                    {
                        myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                        canDoubleJump = false;
                    }
                }


        }
        #endregion   


    public void sol_press()
    {
        sol = true;
    }

    public void sol_break()
    {
        sol = false;
    }

    public void sag_press()
    {
        sag = true;
    }

    public void sag_break()
    {
        sag = false;
    }
}
