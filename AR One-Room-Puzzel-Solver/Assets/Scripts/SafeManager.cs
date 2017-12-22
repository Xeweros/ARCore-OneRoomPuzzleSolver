using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.HelloAR;

public class SafeManager : MonoBehaviour
{
    // holds the color and the number for the right code
    public SafeCode[] m_aSafeUnlockCode;

    // gives the player feedback for his/her input of the code
    public Light m_lCodeFeedback;

    // current position of the codeinput ( if idx = 2, then the player has 2 right inputs of max digits of the code)
    public int m_nIdxCodeCorrectInsertPosition = 0;

    // position where the key spawn in the safe
    public Transform m_transKeyPosition;

    // number how many digits the code has
    [Range(1, 9, order = 1)]
    public int m_nCodeDigits = 2;

    public GameManager m_scrGameManager;

    // soundfeedback for the user, when he inputs a code
    public AudioSource m_ausoCodeRight;
    public AudioSource m_ausoCodeWrong;

    public struct SafeCode
    {
        public int m_nNumber;
        public Color m_cColorClue;
    }

    private void Start()
    {
        m_scrGameManager = FindObjectOfType<GameManager>();

        m_aSafeUnlockCode = new SafeCode[9];

        for (int i = 0; i < m_aSafeUnlockCode.Length; ++i)
        {
            m_aSafeUnlockCode[i].m_nNumber = 0;
        }

        m_aSafeUnlockCode[0].m_cColorClue = Color.yellow;
        m_aSafeUnlockCode[1].m_cColorClue = Color.blue;
        m_aSafeUnlockCode[2].m_cColorClue = Color.cyan;
        m_aSafeUnlockCode[3].m_cColorClue = Color.red;
        m_aSafeUnlockCode[4].m_cColorClue = Color.green;
        m_aSafeUnlockCode[5].m_cColorClue = Color.magenta;
        m_aSafeUnlockCode[6].m_cColorClue = Color.white;
        m_aSafeUnlockCode[7].m_cColorClue = Color.grey;
        m_aSafeUnlockCode[8].m_cColorClue = Color.black;

        m_lCodeFeedback.GetComponent<Light>().color = Color.yellow;

        for (int i = 0; i < m_nCodeDigits; ++i)
        {
            m_aSafeUnlockCode[i].m_nNumber = Random.Range(0, 10);
            Debug.Log(i + " Number " + m_aSafeUnlockCode[i].m_nNumber);
        }

        UpdateNumpadColorForClue();

        // Send the data for which number to spawn and what color they should have, to match the clue for the right number
        m_scrGameManager.SpawnNumbersForSafe(m_nCodeDigits, m_aSafeUnlockCode);
    }

    public void InsertCode(int _numberPressed)
    {
        if (m_aSafeUnlockCode[m_nIdxCodeCorrectInsertPosition].m_nNumber == _numberPressed)
        {
            // correct number
            m_nIdxCodeCorrectInsertPosition++;

            m_ausoCodeRight.Play();
            m_lCodeFeedback.GetComponent<Light>().color = Color.green;
            Invoke("ResetFeedbackLightColor", 1.0f);

            if (m_nIdxCodeCorrectInsertPosition == m_nCodeDigits)
            {
                m_scrGameManager.SpawnSafeKey(m_transKeyPosition.position);
                Destroy(gameObject);
            }
            else
            {
                UpdateNumpadColorForClue();
            }
        }
        else
        {
            // wrong number
            m_nIdxCodeCorrectInsertPosition = 0;

            UpdateNumpadColorForClue();

            m_ausoCodeWrong.Play();
            m_lCodeFeedback.GetComponent<Light>().color = Color.red;
            Invoke("ResetFeedbackLightColor", 1.0f);
        }
    }

    public void ResetFeedbackLightColor()
    {
        m_lCodeFeedback.GetComponent<Light>().color = Color.yellow;
    }

    // Updates the color to match clue for the next right number
    public void UpdateNumpadColorForClue()
    {
        GameObject _numpad = transform.GetChild(0).gameObject;
        GameObject _numpadNumber;
        for (int i = 0; i < 10; ++i)
        {
            _numpadNumber = _numpad.transform.GetChild(i).gameObject;

            MeshRenderer[] _renderer = _numpadNumber.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer _mr in _renderer)
            {
                _mr.material.color = m_aSafeUnlockCode[m_nIdxCodeCorrectInsertPosition].m_cColorClue;
            }
        }
    }
}
