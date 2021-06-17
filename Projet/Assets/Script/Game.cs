using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game: MonoBehaviour
{
    public List<Player> players;
    public int fieldwidth;
    public int fieldheight;

    public void setfield(int w, int h)
    {
        fieldheight = h;
        fieldwidth = w;
    }
        

    public bool ValidPosition(int x, int y)
    {
        if (!(x > 0 && x < (fieldwidth - 1) && y > 0 && y < fieldheight - 1))
            return false;
        foreach (var player in players)
        {
            if (player.Posx() == x && player.Posy() == y)
                return false;
        }
        //ajoutez les obstacles
        return true;
    }
}

