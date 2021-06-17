using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    protected int health; 
    protected int damage;
            
    public int Gethealth()
    {
        return health;
    }
    
    public int Getdamage() 
    {
        return damage;
    }
    
}
