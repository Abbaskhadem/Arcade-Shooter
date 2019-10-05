
using TMPro;
using UnityEngine;

public class Upgrade_Item : MonoBehaviour
{
    [SerializeField]private float DropSpeed;
    private GameObject temp;
    [SerializeField] private GameObject PowerText;
    private void Start()
    {
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,DropSpeed*-1));
        Destroy(gameObject,5f);
       // temp= Instantiate(PowerText, transform.position,Quaternion.identity);
       // temp.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var c = other.GetComponent<Main_SpaceShip>();
        if (gameObject.CompareTag("Damage")&&other.gameObject.CompareTag("Player"))
        {
            AudioManager._Instance.PlayAudio(0);
            c.UpgradeWeapons();
         //   PowerUpTextController.instance.Creat("Power Up", other.transform.position);
            //Instantiate(PowerText, other.transform.position,Quaternion.identity);

            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Coin") && other.gameObject.CompareTag("Player"))
        {
            int RandomNum = Random.Range(100, 300);
            PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coin")+RandomNum);
            PowerUpTextController.Instance.Creat($"+{RandomNum}", transform.position);
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("Shield") && other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Main_SpaceShip>().Shild = true;
        }
    }
}
