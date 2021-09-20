using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Tooltip("The Handler that is Holding this Item")]
    public Handler Handler;

    [Tooltip("If the Item has been Picked Up")]
    public bool pickedUp;

    // The Audio for the Item when Used
    private AudioSource SoundEffect;

    protected virtual void Awake()
    {
        SoundEffect = GetComponent<AudioSource>();
    }

    public virtual void UseItem()
    {
        // If there is Audio, Play It!
        if(SoundEffect && SoundEffect.clip)
        {
            SoundEffect.pitch = Random.Range(0.7f, 1.3f);
            SoundEffect.Play();
            Invoke("OnUse", SoundEffect.clip.length); // Calls the After Use Event once the Audio has Ended
        }
    }

    // Picks Up the Item
    public virtual void PickUp(Handler handler)
    {
        // Only Run if not already Picked Up
        if(pickedUp)
            return;

        pickedUp = true;
        this.transform.localScale = handler.gameObject.transform.localScale;
        this.Handler = handler;
    }

    // Drops the Item
    public void Drop()
    {
        // Only Run if not already Dropped
        if(!pickedUp)
            return;

        pickedUp = false;
    }

    // Runs After the Item has been Used. Normally at the end of the Sound Effect
    protected virtual void OnUse()
    {
        Handler.itemInUse = false;
    }
}
