using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Entity : MonoBehaviourPun
{
    protected int x;
    protected int y;
    protected int hp;
    protected int damage;
    public LayerMask layer;
    private SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
        
    protected void SetXY(int X0, int Y0)
    {
        x = X0;
        y = Y0;
    }
    public int Posx()
    { 
        return x;
    }

    public int Posy()
    { 
        return y;
    }

    public int GetHealth()
    {
        return hp;
    }

    public void SetHealth(int updatedHp)
    {
        hp = updatedHp;
    }

    public int GetDamages()
    {
        return damage;
    }

    public void SetDamages(int updatedDamage)
    {
        damage = updatedDamage;
    }

    public void Damaged(int loss)
    {
        hp -= loss;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
        
        SetHealth(hp);
    }
        
    protected void Move(game jeu)
    {
        while (true)
        {    
            
            int mx = new System.Random().Next(0,3) - 1;
            int my = new System.Random().Next(0,3) - 1;

            if (jeu.ValidPosition(x + mx, y + my))
            {
                x += mx;
                y += my;
                break;
            }
        }
    }
 
    protected void MoveTo(game jeu, Entity entit)
    {
        int tmpX = x;
        int tmpY = y;
        int dist = 420; // Une valeur au dessus de la distance max

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (jeu.ValidPosition(x+i,y+j))
                {
                    int tmp = Math.Abs(x + i - entit.x) + Math.Abs(y + j - entit.y);
                    if (tmp < dist)
                    {
                        tmpX = x + i;
                        tmpY = y + j;
                        dist = tmp;
                    }
                }
            }
        }
        SetXY(tmpX,tmpY);
    }
}

