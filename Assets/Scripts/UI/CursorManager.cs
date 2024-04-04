using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] InputReader2 inputReader;
    [SerializeField] CursorMapping[] cursorMappings;

    private PauseMenuManager pauseMenuManager;

    private float targetEnemyRadius = 10;

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

    private void Start()
    {
        pauseMenuManager = GetComponent<PauseMenuManager>();
    }

    private void Update()
    {
        if (inputReader.IsCursorOn)
        {
            TurnOnCursor();
        }
        else
        {
            TurnOffCursor();
        }
    }

    private void TurnOffCursor()
    {
        Cursor.visible = false;
    }

    private void TurnOnCursor()
    {
        Cursor.visible = true;

        DrawCursor();
    }

    private void DrawCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, targetEnemyRadius, LayerMask.GetMask("Enemy")))
        {
            SetCursor(CursorType.TARGETENEMY);
        }
        else
        {
            SetCursor(CursorType.DEFAULT);
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
