using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private int energy = 10;

    private List<Blob> owner = new List<Blob>();
    private bool isHawk = false;
    private bool isDestroyed = false;

    public void SetOwner(Blob blob)
    {
        if(blob is Hawk) 
            isHawk = true;
        owner.Add(blob);
    }

    public void RemoveOwner(Blob blob)
    {
        owner.Remove(blob);
    }

    public void TakeFood()
    {
        energy--;

        if (energy < 1)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public bool IsHawkEating() => isHawk;
    
    public bool IsDestroyed() => isDestroyed; // == return isDestroyed;

    // Hawk¸¸ »ç¿ë
    public bool CheckEnemy()
    {
        // Hawk vs Hawk
        if(isHawk)
        {
            // flag°¡ ¸¹À»¼ö·Ï µÞ´ú¹Ì°¡ ÀâÈù´Ù.
            isDestroyed = true;
            Destroy(gameObject);
            return false;
        }
        // Hawk vs Dove
        else
        {
            foreach(Dove dove in owner)
            {
                dove.SetKicked();
            }

            return true;
        }
    }
}
