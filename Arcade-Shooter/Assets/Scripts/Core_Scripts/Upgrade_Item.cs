
using UnityEngine;


public class Upgrade_Item : MonoBehaviour
{
    [SerializeField] private GameObject PowerText;
    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-28));
        Destroy(gameObject,5f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Damage")&&other.gameObject.CompareTag("Player"))
        {
            Instantiate(PowerText, other.transform.position,Quaternion.identity);
            other.GetComponent<Main_SpaceShip>().UpgradeWeapons();   
            Destroy(gameObject);
        }

    }
}
