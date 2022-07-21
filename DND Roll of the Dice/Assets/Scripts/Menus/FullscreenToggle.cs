using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    bool isFullscreen = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setFullscreen(!isFullscreen);
        }
    }

    public void setFullscreen(bool fullscreen)
    {
        if (fullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        isFullscreen = !isFullscreen;
    }
}
