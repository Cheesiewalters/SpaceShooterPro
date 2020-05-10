using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;
    private Player player;
    [SerializeField]
    private int powerupID;

    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);

        if (transform.position.y <= -4)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.TripleShopActive();
                        break;
                    case 1:
                        player.SpeedPowerUpActive();
                        break;
                    case 2:
                        player.ShieldPowerUpActive();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
