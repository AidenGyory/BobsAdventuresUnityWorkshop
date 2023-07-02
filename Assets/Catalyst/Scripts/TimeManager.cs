using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale; 
    }
    [ContextMenu("Reset Time Scale")]
    public void ResetTime()
    {
        Time.timeScale = 1; 
    }
    
    [ContextMenu("Stop Time Scale")]
    public void StopTime()
    {
        Time.timeScale = 0; 
    }
}
