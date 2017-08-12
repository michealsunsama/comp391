using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public bool facePlayer;
    public GameObject player;
    public float downSpeed;
    public int hp;
    public string moveStyle;
    public GameObject shot;
    public GameObject laser;
    private Rigidbody2D rb;
    void Start()
    {
        downSpeed = -downSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        switch (moveStyle)
        {
            case "stationaryNoAttack":
                rb.velocity = new Vector2(0f, downSpeed);
                break;
            case "stationary":
                stationary();
                break;
            case "random":
                random();
                break;
            case "leftSwing":
                leftSwing();
                break;
            case "rightSwing":
                rightSwing();
                break;
            case "Diagonal":
                Diagonal();
                break;
            case "leftRightStop":
                leftRightStop();
                break;
            case "leftRight":
                leftRight();
                break;
            case "sinWave":
                sinWave();
                break;
            case "boss":
                bossMove();
                bossAttack();
                break;
        }
    }

    void FixedUpdate() {
        if (facePlayer){
            float xDelta = this.transform.position.x - player.transform.position.x;
            float yDelta = this.transform.position.y - player.transform.position.y;
            if (yDelta == 0) yDelta = 0.01f;
            this.transform.eulerAngles = new Vector3(0,0,Mathf.Atan(xDelta / yDelta));        
    }
        if (hp == 0) {
            Destroy(this);
        }
    }
    private IEnumerator stationary()
    {
        rb.velocity = new Vector2(0f, downSpeed);
        while (true)
        {
            yield return new WaitForSeconds(2);
            Instantiate(shot);
        }
    }
    private IEnumerator random()
    {
        rb.velocity = new Vector2(Random.Range(-5f, 5f), downSpeed+ Random.Range(-2f, 2f));
        while (true)
        {
            yield return new WaitForSeconds(2);
            Instantiate(shot);
        }
    }

    private IEnumerator leftSwing()
    {
        rb.velocity = new Vector2(0f, downSpeed);
        yield return new WaitForSeconds(2);
        Instantiate(shot);
        rb.velocity = new Vector2(-3f, downSpeed);
        yield return new WaitForSeconds(2);
        Instantiate(shot);
        rb.velocity = new Vector2(0f, downSpeed);
        while (true)
        {
            yield return new WaitForSeconds(2);
            Instantiate(shot);
        }
    }

    private IEnumerator rightSwing()
    {
        rb.velocity = new Vector2(0f, downSpeed);
        yield return new WaitForSeconds(2);
        Instantiate(shot);
        rb.velocity = new Vector2(3f, downSpeed);
        yield return new WaitForSeconds(2);
        Instantiate(shot);
        rb.velocity = new Vector2(0f, downSpeed);
        while (true)
        {
            yield return new WaitForSeconds(2);
            Instantiate(shot);
        }
    }
    private IEnumerator Diagonal()
    {
        float horizontal = Mathf.Tan(transform.eulerAngles.z) * downSpeed;
        rb.velocity = new Vector2(horizontal, downSpeed);
        while (true)
        {
            yield return new WaitForSeconds(2);
            Instantiate(shot);
        }
    }

    private IEnumerator leftRightStop()
    {
        rb.velocity = new Vector2(0, downSpeed);
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 6; i++)
        {
            rb.velocity = new Vector2(-1f, 0);
            yield return new WaitForSeconds(1);
            Instantiate(shot);
            rb.velocity = new Vector2(1f, 0);
            yield return new WaitForSeconds(2);
            Instantiate(shot);
            rb.velocity = new Vector2(-1f, 0);
            yield return new WaitForSeconds(1);
        }
        rb.velocity = new Vector2(0, downSpeed);
    }

    private IEnumerator leftRight()
    {
        while (true)
        {
            rb.velocity = new Vector2(-1f, downSpeed);
            yield return new WaitForSeconds(2);
            Instantiate(shot);
            rb.velocity = new Vector2(1f, 0);
            yield return new WaitForSeconds(2);
            Instantiate(shot);
        }
    }

    private IEnumerator sinWave()
    {
        while (true)
        {
            while (true)
            {
                float xDelta = Mathf.Sin(Time.deltaTime / 100);
                if (Time.deltaTime % 2000 == 0)
                {
                    Instantiate(shot);
                }
            }
        }
    }
    private IEnumerator bossMove()
    {
        rb.velocity = new Vector2(0, downSpeed);
        yield return new WaitForSeconds(2);
        while (true)
        {
            rb.velocity = new Vector2(-1f, 0);
            yield return new WaitForSeconds(2);
            rb.velocity = new Vector2(1f, 0);
            yield return new WaitForSeconds(2);
        }
    }
    private IEnumerator bossAttack() {
        float xDelta=0f;
        float yDelta=0f;
        while (hp < 50)
        {
            for (int i = 0; i < 50; i++)
            {
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Time.deltaTime));
                yield return new WaitForSeconds(0.1f);
            }
            Instantiate(laser);
            yield return new WaitForSeconds(5f);
            for (int i = 0; i < 5; i++)
            {
                xDelta = this.transform.position.x - player.transform.position.x;
                yDelta = this.transform.position.y - player.transform.position.y;
                if (yDelta == 0) yDelta = 0.01f;
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta)));
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) - 2));
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) + 2));
                yield return new WaitForSeconds(0.5f);
            }
        }
        while (hp < 50)
        {
            for (int i = 0; i < 100; i++)
            {
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Time.deltaTime));
                yield return new WaitForSeconds(0.05f);
            }
            xDelta = this.transform.position.x - player.transform.position.x;
            yDelta = this.transform.position.y - player.transform.position.y;
            if (yDelta == 0) yDelta = 0.01f;
            Instantiate(laser, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) - 10));
            Instantiate(laser, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) + 10));
            for (int i = 0; i < 5; i++)
            {
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta)));
                yield return new WaitForSeconds(1f);
            }
            for (int i = 0; i < 5; i++)
            {
                xDelta = this.transform.position.x - player.transform.position.x;
                yDelta = this.transform.position.y - player.transform.position.y;
                if (yDelta == 0) yDelta = 0.01f;
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta)));
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) - 2));
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) + 2));
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) - 4));
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) + 4));
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) - 6));
                Instantiate(shot, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan(xDelta / yDelta) + 6));
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "player")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "shotOne(Clone)")
        {
            hp--;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "shotTwo(Clone)")
        {
            hp-=2;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "shotThree(Clone)")
        {
            hp-=3;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "boundary")
        {
            Destroy(this.gameObject);
        }
    }
}