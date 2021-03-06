using System.Collections;
using System.Collections.Generic;
using FMODUnity;

public class FMODHelper
{
    public Dictionary<string, FMOD.Studio.EventInstance> FMODEvents;

    private Dictionary<string, bool> releasedEvents;

    public FMODHelper(string[] eventNames)
    {
        FMODEvents = new Dictionary<string, FMOD.Studio.EventInstance>();
        releasedEvents = new Dictionary<string, bool>();

        foreach(string eventName in eventNames)
        {
            if(eventName != null && eventName.Length > 0)
            {
                FMODEvents.Add(eventName, RuntimeManager.CreateInstance("event:/" + eventName));
                releasedEvents.Add(eventName, true);
            }
        }
    }

    public void PlayOneshot(string eventName)
    {
        if(FMODEvents.ContainsKey(eventName))
        {
            FMODEvents[eventName].start();
            releasedEvents[eventName] = false;
        }
    }

    public void PlayLoop(string eventName)
    {
        if(FMODEvents.ContainsKey(eventName))
        {
            FMODEvents[eventName].start();
        }
    }

    public void StopSound(string eventName, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {
        if(FMODEvents.ContainsKey(eventName))
        {
            FMODEvents[eventName].stop(stopMode);
            if(!releasedEvents[eventName])
            {
                FMODEvents[eventName].release();
                releasedEvents[eventName] = true;
            }
        }
    }
}
