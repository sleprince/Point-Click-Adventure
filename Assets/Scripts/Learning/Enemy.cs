using UnityEngine;

public class Enemy
{

    public float health = 100f;
    public string name;


    public void EnemyInfo()
    {
        Debug.Log("The Enemy is called " + name + " and has " + health + " health");


    }


} //class


