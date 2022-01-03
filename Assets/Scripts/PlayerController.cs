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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<TimeControl>().enabled = false;
            Die();
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            /* winPanel.active = true;
            Time.timeScale = 0; */
            Destroy(collision.gameObject);
            StartCoroutine(Wait(true));
        }
        #endregion
    }
    #region nesnelerin seslendirme kodlarý karakter temas ettiði anda nesnenin Türkçe ve Ýngilizce seslendirilmesi yapýlýyor

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Coin")
        {
            ses.PlayOneShot(sesler[0], 1);
        }
        else if (collision.gameObject.tag == "1")
        {
            ses.PlayOneShot(sesler[1], 1);
    
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "2")
        {
            ses.PlayOneShot(sesler[2], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "3")
        {
            ses.PlayOneShot(sesler[3], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "4")
        {
            ses.PlayOneShot(sesler[4], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "5")
        {
            ses.PlayOneShot(sesler[5], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "6")
        {
            ses.PlayOneShot(sesler[6], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "7")
        {
            ses.PlayOneShot(sesler[7], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "8")
        {
            ses.PlayOneShot(sesler[8], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "9")
        {
            ses.PlayOneShot(sesler[9], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Ananas")
        {
            ses.PlayOneShot(sesler[10], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Armut")
        {
            ses.PlayOneShot(sesler[11], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Hindistan Cevizi")
        {
            ses.PlayOneShot(sesler[12], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Karpuz")
        {
            ses.PlayOneShot(sesler[13], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kiraz")
        {
            ses.PlayOneShot(sesler[14], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kivi")
        {
            ses.PlayOneShot(sesler[15], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Muz")
        {
            ses.PlayOneShot(sesler[16], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Portakal")
        {
            ses.PlayOneShot(sesler[17], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Çilek")
        {
            ses.PlayOneShot(sesler[18], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Üzüm")
        {
            ses.PlayOneShot(sesler[19], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Telefon")
        {
            ses.PlayOneShot(sesler[20], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Radyo")
        {
            ses.PlayOneShot(sesler[21], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kamera")
        {
            ses.PlayOneShot(sesler[22], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bilgisayar")
        {
            ses.PlayOneShot(sesler[23], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Akýllý Saat")
        {
            ses.PlayOneShot(sesler[24], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Laptop")
        {
            ses.PlayOneShot(sesler[25], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Televizyon")
        {
            ses.PlayOneShot(sesler[26], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Yazýcý")
        {
            ses.PlayOneShot(sesler[27], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Hesap Makinesi")
        {
            ses.PlayOneShot(sesler[28], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Tablet")
        {
            ses.PlayOneShot(sesler[29], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Doktor")
        {
            ses.PlayOneShot(sesler[30], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Avukat")
        {
            ses.PlayOneShot(sesler[31], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Asker")
        {
            ses.PlayOneShot(sesler[32], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Mühendis")
        {
            ses.PlayOneShot(sesler[33], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Çiftçi")
        {
            ses.PlayOneShot(sesler[34], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Sporcu")
        {
            ses.PlayOneShot(sesler[35], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Pilot")
        {
            ses.PlayOneShot(sesler[36], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Polis")
        {
            ses.PlayOneShot(sesler[37], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Astronot")
        {
            ses.PlayOneShot(sesler[38], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Mimar")
        {
            ses.PlayOneShot(sesler[39], 1);

            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Gri")
        {
            ses.PlayOneShot(sesler[40], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kahverengi")
        {
            ses.PlayOneShot(sesler[41], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kýrmýzý")
        {
            ses.PlayOneShot(sesler[42], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Mavi")
        {
            ses.PlayOneShot(sesler[43], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Mor")
        {
            ses.PlayOneShot(sesler[44], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Pembe")
        {
            ses.PlayOneShot(sesler[45], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Sarý")
        {
            ses.PlayOneShot(sesler[46], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Siyah")
        {
            ses.PlayOneShot(sesler[47], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Turuncu")
        {
            ses.PlayOneShot(sesler[48], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Yeþil")
        {
            ses.PlayOneShot(sesler[49], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Biber")
        {
            ses.PlayOneShot(sesler[50], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Brokoli")
        {
            ses.PlayOneShot(sesler[51], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Lahana")
        {
            ses.PlayOneShot(sesler[52], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Havuç")
        {
            ses.PlayOneShot(sesler[53], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Patlýcan")
        {
            ses.PlayOneShot(sesler[54], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Domates")
        {
            ses.PlayOneShot(sesler[55], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Sarýmsak")
        {
            ses.PlayOneShot(sesler[56], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Ispanak")
        {
            ses.PlayOneShot(sesler[57], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Soðan")
        {
            ses.PlayOneShot(sesler[58], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Turp")
        {
            ses.PlayOneShot(sesler[59], 1);

            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Kemer")
        {
            ses.PlayOneShot(sesler[60], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kazak")
        {
            ses.PlayOneShot(sesler[61], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Eldiven")
        {
            ses.PlayOneShot(sesler[62], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Pantalon")
        {
            ses.PlayOneShot(sesler[63], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kýravat")
        {
            ses.PlayOneShot(sesler[64], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Þort")
        {
            ses.PlayOneShot(sesler[65], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Çorap")
        {
            ses.PlayOneShot(sesler[66], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Gözlük")
        {
            ses.PlayOneShot(sesler[67], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Tiþört")
        {
            ses.PlayOneShot(sesler[68], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bere")
        {
            ses.PlayOneShot(sesler[69], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Tekli Koltuk")
        {
            ses.PlayOneShot(sesler[70], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Yatak")
        {
            ses.PlayOneShot(sesler[71], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Sehba")
        {
            ses.PlayOneShot(sesler[72], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kanepe")
        {
            ses.PlayOneShot(sesler[73], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Buzdolabý")
        {
            ses.PlayOneShot(sesler[74], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Lamba")
        {
            ses.PlayOneShot(sesler[75], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Fýrýn")
        {
            ses.PlayOneShot(sesler[76], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Ayna")
        {
            ses.PlayOneShot(sesler[77], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kilim")
        {
            ses.PlayOneShot(sesler[78], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Masa")
        {
            ses.PlayOneShot(sesler[79], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Okçuluk")
        {
            ses.PlayOneShot(sesler[80], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Masatenisi")
        {
            ses.PlayOneShot(sesler[81], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Badminton")
        {
            ses.PlayOneShot(sesler[82], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Beyzbol")
        {
            ses.PlayOneShot(sesler[83], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Basketbol")
        {
            ses.PlayOneShot(sesler[84], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bovling")
        {
            ses.PlayOneShot(sesler[85], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Rafting")
        {
            ses.PlayOneShot(sesler[86], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Jimanstik")
        {
            ses.PlayOneShot(sesler[87], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Tenis")
        {
            ses.PlayOneShot(sesler[88], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Voleybol")
        {
            ses.PlayOneShot(sesler[89], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Uçak")
        {
            ses.PlayOneShot(sesler[90], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Tren")
        {
            ses.PlayOneShot(sesler[91], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Otobüs")
        {
            ses.PlayOneShot(sesler[92], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Araba")
        {
            ses.PlayOneShot(sesler[93], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Helikopter")
        {
            ses.PlayOneShot(sesler[94], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Motorsiklet")
        {
            ses.PlayOneShot(sesler[95], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Gemi")
        {
            ses.PlayOneShot(sesler[96], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bisiklet")
        {
            ses.PlayOneShot(sesler[97], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Taksi")
        {
            ses.PlayOneShot(sesler[98], 1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Kamyon")
        {
            ses.PlayOneShot(sesler[99], 1);

            Destroy(collision.gameObject);
        }
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
