using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;
    int scoreP1;
    int scoreP2;
    Text scoreUIP1;
    Text scoreUIP2;

    AudioSource audio;
    public AudioClip hitSound;
    public AudioClip hitPembatas;

    GameObject panelSelesai;
    Text txPemenang;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(2, 0).normalized;
        rigid.AddForce(arah * force);

        scoreP1 = 0;
        scoreP2 = 0;
        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();

        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);
        audio = GetComponent<AudioSource>();
    }
    
    void ResetBall()
    {
        transform.localPosition = new Vector2(0, 0);
        rigid.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D (Collision2D coll)
    {
        
        if (coll.gameObject.name == "TepiKanan")
        {
            audio.PlayOneShot(hitPembatas);
            ResetBall();
            Vector2 arah = new Vector2(2, 0).normalized;
            rigid.AddForce(arah * force);
            scoreP1++;
            TampilkanScore();
            if (scoreP1 == 5)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Biru Pemenang!";
                Destroy(gameObject);
                return;
            }
        }
        if (coll.gameObject.name == "TepiKiri")
        {
            audio.PlayOneShot(hitPembatas);
            ResetBall();
            Vector2 arah = new Vector2(-2, 0).normalized;
            rigid.AddForce(arah * force);
            scoreP2++;
            TampilkanScore();
            if (scoreP2 == 5)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "Player Merah Pemenang!";
                Destroy(gameObject);
                return;
            }
        }
        if (coll.gameObject.name =="Pemukul1" || coll.gameObject.name == "Pemukul2")
        {
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(arah * force * 2);

            audio.PlayOneShot(hitSound);
        }
    }

    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + "\nScore P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }
}
