using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numpad : MonoBehaviour
{
    public int m_nNumpadNumber = 0;
    public KeyCode m_kcKeyboardDebug;
    public SafeManager m_scrSageManager;

	void Update ()
    {
		if (Input.GetKeyDown(m_kcKeyboardDebug))
        {
            Debug.Log("Numpad " + m_kcKeyboardDebug + " Number " + m_nNumpadNumber);
            m_scrSageManager.InsertCode(m_nNumpadNumber);
        }
	}

    // call when user presses a number on the safe numpad, which send the sagemanager the number which was pressed to check if it was right
    public void PressedNumberOnNumpad()
    {
        m_scrSageManager.InsertCode(m_nNumpadNumber);
    }
}
