using TMPro;
using UnityEngine;

public class WallControl : MonoBehaviour
{
    [SerializeField]
    private int currentHp;

    [SerializeField]
    public int hpPerLevel;

    public TMP_Text textHp;

    // Start is called before the first frame update
    void Start()
    {
        hpPerLevel = PlayerPrefs.GetInt("U_Walls");
        currentHp = (10 + PlayerPrefs.GetInt("U_Repair") * hpPerLevel) - 5;
        textHp = GetComponentInChildren<TMP_Text>();
        if (textHp == null) Debug.Log("xd");
    }

    // Update is called once per frame
    void Update()
    {

        textHp.SetText("" + currentHp);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                resolveWallEffect();
            }
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Brain")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-collision.contacts[0].normal +
         new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)));
        }
        
    }

    public void resolveWallEffect()
    {
        
        if (PlayerPrefs.GetInt("repair") == 1)
        {
            currentHp = PlayerPrefs.GetInt("U_Repair");
            int maxWallHp = 10 + PlayerPrefs.GetInt("U_Repair") * hpPerLevel;

            if (currentHp > maxWallHp)
            {
                currentHp = maxWallHp;
            }
            else currentHp++;

        }
        else if (PlayerPrefs.GetInt("upgrade") == 1)
        {
            Debug.Log("upgrade");

        }
        
    }


    public void sayHello()
    {
        Debug.Log(gameObject.name);
    }
}

