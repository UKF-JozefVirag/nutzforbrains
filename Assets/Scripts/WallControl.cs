using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class WallControl : MonoBehaviour
{
    [SerializeField]
    private int currentHp;


    // Start is called before the first frame update
    void Start()
    {
            
        switch (PlayerPrefs.GetInt("U_Repair"))
        {
            case 0:
                currentHp = 10;
                break;
            case 1:
                currentHp = 20;
                break;
            case 2:
                currentHp = 30;
                break;
            default: 
                currentHp = 10;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
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

    private void resolveWallEffect()
    {
        
        if (PlayerPrefs.GetInt("repair") == 1)
        {
            Debug.Log("repair");
            //TODO: ak je uroven repairu X tak sa hp zvysi o X a nemoze presiahnuť uroven HP daneho lvlu

        }
        else if (PlayerPrefs.GetInt("upgrade") == 1)
        {
            Debug.Log("upgrade");
            //TODO: ak je uroven repairu X tak sa hp zvysi o X a nemoze presiahnuť uroven HP daneho lvlu

        }
        
    }


    public void sayHello()
    {
        Debug.Log(gameObject.name);
    }
}

