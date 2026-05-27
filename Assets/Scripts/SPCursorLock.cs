using UnityEngine;

public static class SPCursorLock
{
    public static bool IsLocked => Cursor.lockState == CursorLockMode.Locked && !Cursor.visible;

    public static void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void Unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static bool HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Unlock();
            return false;
        }

        if (!IsLocked)
        {
            if (Input.GetMouseButtonDown(0))
                Lock();

            return false;
        }

        return true;
    }
}
