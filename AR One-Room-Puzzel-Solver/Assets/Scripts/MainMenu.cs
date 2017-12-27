using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject m_goViewportContent;

    public GameObject m_goChangeRoomGUI;

    private GameObject m_goMainMenu;
    private GameObject m_goLevelSelect;
    private GameObject m_goRoomOption;
    private GameObject m_goRoomChange;
    private GameObject m_goOption;

    private int m_nRoomToChange = 0;
    private int m_nActiveRoom = 0;

    private string m_sChangeRoomName = "";
    private float m_fChangeRoomWidth  = 1.0f;
    private float m_fChangeRoomHeight = 1.0f;
    private float m_fChangeRoomDepth  = 1.0f;

    private void Start()
    {
        StaticRoomDataManager.CreateSaveDataIfNotExistsElseReadData();

        for (int i = 0; i < StaticRoomDataManager.m_nNumberOfRooms; ++i)
        {
            m_goViewportContent.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = StaticRoomDataManager.m_aRoomData[i].m_sName;
            m_goViewportContent.transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = StaticRoomDataManager.m_aRoomData[i].m_fWidth.ToString();
            m_goViewportContent.transform.GetChild(i).GetChild(4).GetComponent<TextMeshProUGUI>().text = StaticRoomDataManager.m_aRoomData[i].m_fHeight.ToString();
            m_goViewportContent.transform.GetChild(i).GetChild(6).GetComponent<TextMeshProUGUI>().text = StaticRoomDataManager.m_aRoomData[i].m_fDepth.ToString();
        }
    }


    public void StartLevel1()
    {
        Debug.Log("load Level 1");
        SceneManager.LoadScene(1);
    }

    public void StartLevel2()
    {
        Debug.Log("load Level 2");
        //SceneManager.LoadScene(2);
    }

    public void StartLevel3()
    {
        Debug.Log("load Level 3");
        //SceneManager.LoadScene(3);
    }

    public void StartTutorial()
    {
        Debug.Log("load Level Tutorial");
        //SceneManager.LoadScene(6);
    }

    public void LoadRandomLevel()
    {
        Debug.Log("load Random Level");
        //SceneManager.LoadScene(4);
    }

    public void LoadCredits()
    {
        Debug.Log("Load Credits");
        //SceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActivateRoomData(int _value)
    {
        Debug.Log("Number: " + _value);
        m_nActiveRoom = _value;

        StaticRoomDataManager.SetActiveRoomData(_value);
    }

    public void ChangeRoomData(int _value)
    {
        Debug.Log("Number: " + _value);
        m_nRoomToChange = _value;

        m_sChangeRoomName   = StaticRoomDataManager.m_aRoomData[m_nRoomToChange].m_sName;
        m_fChangeRoomWidth  = StaticRoomDataManager.m_aRoomData[m_nRoomToChange].m_fWidth;
        m_fChangeRoomHeight = StaticRoomDataManager.m_aRoomData[m_nRoomToChange].m_fHeight;
        m_fChangeRoomDepth  = StaticRoomDataManager.m_aRoomData[m_nRoomToChange].m_fDepth;

        m_goChangeRoomGUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = m_sChangeRoomName;

        m_goChangeRoomGUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomWidth.ToString();
        m_goChangeRoomGUI.transform.GetChild(4).GetComponent<Slider>().value = m_fChangeRoomWidth * 100;

        m_goChangeRoomGUI.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomHeight.ToString();
        m_goChangeRoomGUI.transform.GetChild(7).GetComponent<Slider>().value = m_fChangeRoomHeight * 100;

        m_goChangeRoomGUI.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomDepth.ToString();
        m_goChangeRoomGUI.transform.GetChild(10).GetComponent<Slider>().value = m_fChangeRoomDepth * 100;
    }

    public void SaveRoomChanges()
    {
        StaticRoomDataManager.m_aRoomData[m_nRoomToChange].m_sName   = m_sChangeRoomName;
        StaticRoomDataManager.m_aRoomData[m_nRoomToChange].m_fWidth  = m_fChangeRoomWidth;
        StaticRoomDataManager.m_aRoomData[m_nRoomToChange].m_fHeight = m_fChangeRoomHeight;
        StaticRoomDataManager.m_aRoomData[m_nRoomToChange].m_fDepth  = m_fChangeRoomDepth;

        m_goViewportContent.transform.GetChild(m_nRoomToChange).GetChild(0).GetComponent<TextMeshProUGUI>().text = m_sChangeRoomName;
        m_goViewportContent.transform.GetChild(m_nRoomToChange).GetChild(2).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomWidth.ToString();
        m_goViewportContent.transform.GetChild(m_nRoomToChange).GetChild(4).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomHeight.ToString();
        m_goViewportContent.transform.GetChild(m_nRoomToChange).GetChild(6).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomDepth.ToString();

        StaticRoomDataManager.WriteRoomData();
    }

    public void OnWidthSliderChange(float _value)
    {
        m_fChangeRoomWidth = _value / 100.0f;
        m_goChangeRoomGUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomWidth.ToString();
    }

    public void OnHeightSliderChange(float _value)
    {
        m_fChangeRoomHeight = _value / 100.0f;
        m_goChangeRoomGUI.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomHeight.ToString();
    }

    public void OnDepthSliderChange(float _value)
    {
        m_fChangeRoomDepth = _value / 100.0f;
        m_goChangeRoomGUI.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = m_fChangeRoomDepth.ToString();
    }
}
