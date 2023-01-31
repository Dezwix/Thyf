using UnityEngine;

public class CursorController : MonoBehaviour
{
    #region Private Fields
    public static bool isFrozen = false;
    #endregion

    void Update()
    {
        if (isFrozen && Input.GetKeyDown(KeyCode.Escape))
        {
            UnfreezePointer();
            return;
        }
            

        if (!isFrozen && Input.GetKeyDown(KeyCode.Escape))
            FreezePointer();

    }

    void FreezePointer()
    {
        Debug.Log("FROZEN");
        Cursor.visible = true;
        isFrozen = true;
    }

    void UnfreezePointer()
    {
        Debug.Log("NOT FROZEN");
        Cursor.visible = false;
        isFrozen = false;
    }
}
