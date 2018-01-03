using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using UnityEngine;

public class StaticOptionManager : MonoBehaviour
{
    public enum DifficultySetting { DS_NONE, DS_EASY, DS_NORMAL, DS_HARD, DS_COUNT };

    /*
    Center
    _______
    |     |
    |  o  |
    |_____|
    
    Wall middle
    _______
    |     |
    |     |
    |__o__|

    */
    public enum RoomSetting { RS_NONE, RS_WALL_MIDDLE, RS_CENTER, RS_COUNT}

    public static int m_nMusicVolume                = 0;
    public static int m_nSoundEffectsVolume         = 0;
    public static DifficultySetting m_eDifficulty   = DifficultySetting.DS_EASY;
    public static RoomSetting m_eRoomSetting        = RoomSetting.RS_WALL_MIDDLE;

    public static string m_sDataName = "Option.xml";

#if UNITY_EDITOR
    public static string m_sDataPath = Application.dataPath + @"\Data\" + m_sDataName;
#elif UNITY_ANDROID || UNITY_IOS
    public static string m_sDataPath = Application.persistentDataPath + @"\Data\" + m_sDataName;
#endif

    public static string m_sXMLBody              = "body";
    public static string m_sXMLMusicVolume       = "music";
    public static string m_sXMLSoundEffectVolume = "sound";
    public static string m_sXMLDifficulty        = "difficulty";
    public static string m_sXMLRoomSetting       = "room";

    public static void CreateSaveDataIfNotExistsElseReadData()
    {
        if (!SaveDataExists())
        {
            m_nMusicVolume          = 0;
            m_nSoundEffectsVolume   = 0;
            m_eDifficulty           = DifficultySetting.DS_EASY;
            m_eRoomSetting          = RoomSetting.RS_WALL_MIDDLE;

            WriteOptionData();
        }
        else
        {
            ReadOptionData();
            DebugLogEverything();
        }
    }

    public static bool SaveDataExists()
    {
        if (File.Exists(m_sDataPath))
        {
            return true;
        }

        return false;
    }

    public static void DebugLogEverything()
    {
        Debug.Log("Music Volume: "       + m_nMusicVolume);
        Debug.Log("Soundeffect Volume: " + m_nSoundEffectsVolume);
        Debug.Log("Difficulty: "         + m_eDifficulty);
        Debug.Log("RoomSetting: "        + m_eRoomSetting);
    }

    public static void ReadOptionData()
    {
        try
        {
            if (SaveDataExists())
            {
                XDocument _loadOptionDataDocument = XDocument.Load(m_sDataPath);

                int _value = 0;

                if (int.TryParse(_loadOptionDataDocument.Element(m_sXMLBody).Element(m_sXMLMusicVolume).Value, out _value))
                {
                    m_nMusicVolume = _value;
                }

                if (int.TryParse(_loadOptionDataDocument.Element(m_sXMLBody).Element(m_sXMLSoundEffectVolume).Value, out _value))
                {
                    m_nSoundEffectsVolume = _value;
                }

                if (int.TryParse(_loadOptionDataDocument.Element(m_sXMLBody).Element(m_sXMLDifficulty).Value, out _value))
                {
                    switch (_value)
                    {
                        case 1:
                            m_eDifficulty = DifficultySetting.DS_EASY;
                            break;

                        case 2:
                            m_eDifficulty = DifficultySetting.DS_NORMAL;
                            break;

                        case 3:
                            m_eDifficulty = DifficultySetting.DS_HARD;
                            break;

                        default:
                            m_eDifficulty = DifficultySetting.DS_EASY;
                            break;
                    }
                }

                if (int.TryParse(_loadOptionDataDocument.Element(m_sXMLBody).Element(m_sXMLRoomSetting).Value, out _value))
                {
                    switch (_value)
                    {
                        case 1:
                            m_eRoomSetting = RoomSetting.RS_WALL_MIDDLE;
                            break;

                        case 2:
                            m_eRoomSetting = RoomSetting.RS_CENTER;
                            break;

                        default:
                            m_eRoomSetting = RoomSetting.RS_WALL_MIDDLE;
                            break;
                    }
                }
            }
            else
            {
                Debug.Log("File doesnt exists!");
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Loading OptionData failed! " + e);
        }
    }

    public static void WriteOptionData()
    {

#if UNITY_EDITOR
        Directory.CreateDirectory(Application.dataPath + @"\Data");
#elif UNITY_ANDROID || UNITY_IOS
        Directory.CreateDirectory(Application.persistentDataPath + @"\Data");
#endif

        try
        {
            XmlTextWriter _optionDataWriter = new XmlTextWriter(m_sDataPath, System.Text.Encoding.UTF8);
            _optionDataWriter.WriteStartDocument(true);
            _optionDataWriter.Formatting = Formatting.Indented;
            _optionDataWriter.Indentation = 2;
            _optionDataWriter.WriteStartElement(m_sXMLBody);


            _optionDataWriter.WriteStartElement(m_sXMLMusicVolume);
            _optionDataWriter.WriteString(m_nMusicVolume.ToString());
            _optionDataWriter.WriteEndElement();

            _optionDataWriter.WriteStartElement(m_sXMLSoundEffectVolume);
            _optionDataWriter.WriteString(m_nSoundEffectsVolume.ToString());
            _optionDataWriter.WriteEndElement();

            _optionDataWriter.WriteStartElement(m_sXMLDifficulty);
            switch (m_eDifficulty)
            {
                case DifficultySetting.DS_EASY:
                    _optionDataWriter.WriteString(1.ToString());
                    break;

                case DifficultySetting.DS_NORMAL:
                    _optionDataWriter.WriteString(2.ToString());
                    break;

                case DifficultySetting.DS_HARD:
                    _optionDataWriter.WriteString(3.ToString());
                    break;
            }
            
            _optionDataWriter.WriteEndElement();

            _optionDataWriter.WriteStartElement(m_sXMLRoomSetting);
            switch (m_eRoomSetting)
            {              
                case RoomSetting.RS_WALL_MIDDLE:
                    _optionDataWriter.WriteString(1.ToString());
                    break;

                case RoomSetting.RS_CENTER:
                    _optionDataWriter.WriteString(2.ToString());
                    break;
            }
            _optionDataWriter.WriteEndElement();


            _optionDataWriter.WriteEndElement(); // body end

            _optionDataWriter.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log("Saving OptionData failed! " + e);
        }
    }
}
