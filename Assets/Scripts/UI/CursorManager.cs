using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] CursorMapping[] cursorMappings;

    // This is the same as enemy chase & player spell radius
    private float targetEnemyRadius = 7;

    public enum CursorType
    {
        DEFAULT,
        TARGETENEMY
    }

    [System.Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotSpot;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, targetEnemyRadius);

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                SetCursor(CursorType.TARGETENEMY);
            }
            else
            {
                SetCursor(CursorType.DEFAULT);
            }
        }
    }

    public void SetCursor(CursorType type)
    {
        CursorMapping cursorMapping = GetCursorMapping(type);
        Cursor.SetCursor(cursorMapping.texture, cursorMapping.hotSpot, CursorMode.Auto);
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach (CursorMapping cm in cursorMappings)
        {
            if (cm.type == type)
            {
                return cm;
            }
        }

        return cursorMappings[0];
    }
}
