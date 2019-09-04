
using TMPro;
using UnityEngine;

public class Upgrade_Item : MonoBehaviour
{
    private GameObject temp;
    [SerializeField] private GameObject PowerText;
    private void Start()
    {
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-28));
        Destroy(gameObject,5f);
       // temp= Instantiate(PowerText, transform.position,Quaternion.identity);
       // temp.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var c = other.GetComponent<Main_SpaceShip>();
        if (gameObject.CompareTag("Damage")&&other.gameObject.CompareTag("Player"))
        {
            c.UpgradeWeapons();
           // temp.transform.position = other.transform.position;
           // temp.transform.rotation = other.transform.rotation;
           // temp.SetActive(true);
            Instantiate(PowerText, other.transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
