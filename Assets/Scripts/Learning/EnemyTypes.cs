using UnityEngine;

public class EnemyTypes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Enemy zombie = new Enemy();
        Enemy MagicZombie = new Enemy();

        zombie.health = 22f;
        zombie.name = "OozeZombie";

        MagicZombie.health = 55f;
        MagicZombie.name = "MagicZombie";

        zombie.EnemyInfo();
        MagicZombie.EnemyInfo();

    }

    // Update is called once per frame
    void Update()
    {

        
    }

}//class
