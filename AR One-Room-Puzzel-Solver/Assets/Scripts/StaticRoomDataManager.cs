using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using UnityEngine;

public class StaticRoomDataManager : MonoBehaviour
{
    public static int m_nNumberOfRooms   = 10;
    public static RoomData[] m_aRoomData = new RoomData[m_nNumberOfRooms];

    public static int m_nActiveRoom = 0;
    public static string m_sActiveRoomName  = "Raum ";
    public static float m_fActiveRoomWidth  = 4.5f;
    public static float m_fActiveRoomHeight = 3.0f;
    public static float m_fActiveRoomDepth  = 4.5f;

    public static string m_sDataName = "RoomData.xml";

#if UNITY_EDITOR
    public static string m_sDataPath = Application.dataPath + @"\Data\" + m_sDataName;
#elif UNITY_ANDROID || UNITY_IOS
    public static string m_sDataPath = Application.persistentDataPath + @"\Data\" + m_sDataName;
#endif

    public static string m_sXMLBody     = "body";
    public static string m_sXMLRoom     = "room";
    public static string m_sXMLName     = "name";
    public static string m_sXMLWidth    = "width";
    public static string m_sXMLHeight   = "height";
    public static string m_sXMLDepth    = "depth";

    public struct RoomData
    {
        public string m_sName;
        public float m_fWidth;
        public float m_fHeight;
        public float m_fDepth;
    }

    public static void CreateSaveDataIfNotExistsElseReadData()
    {
        if (!SaveDataExists())
        {
            for (int i = 0; i < m_nNumberOfRooms; ++i)
            {
                m_aRoomData[i].m_sName   = m_sActiveRoomName + (i + 1);
                m_aRoomData[i].m_fWidth  = m_fActiveRoomWidth;
                m_aRoomData[i].m_fHeight = m_fActiveRoomHeight;
                m_aRoomData[i].m_fDepth  = m_fActiveRoomDepth;
            }

            WriteRoomData();
        }
        else
        {
            ReadRoomData();
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

    public static void ReadRoomData()
    {
        try
        {
            if (SaveDataExists())
            {
                XDocument _loadRoomDataDocument = XDocument.Load(m_sDataPath);

                float _value = 0;
                for (int i = 0; i < m_nNumberOfRooms; ++i)
                {
                    m_aRoomData[i].m_sName = _loadRoomDataDocument.Element(m_sXMLBody).Element(m_sXMLRoom + i.ToString()).Element(m_sXMLName).Value.ToString();

                    if (float.TryParse(_loadRoomDataDocument.Element(m_sXMLBody).Element(m_sXMLRoom + i.ToString()).Element(m_sXMLWidth).Value, out _value))
                    {
                        m_aRoomData[i].m_fWidth = _value;
                    }

                    if (float.TryParse(_loadRoomDataDocument.Element(m_sXMLBody).Element(m_sXMLRoom + i.ToString()).Element(m_sXMLHeight).Value, out _value))
                    {
                        m_aRoomData[i].m_fHeight = _value;
                    }

                    if (float.TryParse(_loadRoomDataDocument.Element(m_sXMLBody).Element(m_sXMLRoom + i.ToString()).Element(m_sXMLDepth).Value, out _value))
                    {
                        m_aRoomData[i].m_fDepth = _value;
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
            Debug.Log("Loading RoomData failed! " + e);
        }
    }

    public static void WriteRoomData()
    {

#if UNITY_EDITOR
        Directory.CreateDirectory(Application.dataPath + @"\Data");
#elif UNITY_ANDROID || UNITY_IOS
        Directory.CreateDirectory(Application.persistentDataPath + @"\Data");
#endif


        try
        {
            XmlTextWriter _roomDataWriter = new XmlTextWriter(m_sDataPath, System.Text.Encoding.UTF8);
            _roomDataWriter.WriteStartDocument(true);
            _roomDataWriter.Formatting = Formatting.Indented;
            _roomDataWriter.Indentation = 2;
            _roomDataWriter.WriteStartElement(m_sXMLBody);

            for (int i = 0; i < m_nNumberOfRooms; ++i)
            {
                _roomDataWriter.WriteStartElement(m_sXMLRoom + i.ToString());

                _roomDataWriter.WriteStartElement(m_sXMLName);
                _roomDataWriter.WriteString(m_aRoomData[i].m_sName);
                _roomDataWriter.WriteEndElement();

                _roomDataWriter.WriteStartElement(m_sXMLWidth);
                _roomDataWriter.WriteString(m_aRoomData[i].m_fWidth.ToString());
                _roomDataWriter.WriteEndElement();

                _roomDataWriter.WriteStartElement(m_sXMLHeight);
                _roomDataWriter.WriteString(m_aRoomData[i].m_fHeight.ToString());
                _roomDataWriter.WriteEndElement();

                _roomDataWriter.WriteStartElement(m_sXMLDepth);
                _roomDataWriter.WriteString(m_aRoomData[i].m_fDepth.ToString());
                _roomDataWriter.WriteEndElement();

                _roomDataWriter.WriteEndElement(); // room end
            }

            _roomDataWriter.WriteEndElement(); // body end

            _roomDataWriter.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log("Saving RoomData failed! " + e);
        }
    }

    public static void DebugLogEverything()
    {
        for (int i = 0; i < m_nNumberOfRooms; ++i)
        {
            Debug.Log("Name: " + m_aRoomData[i].m_sName);
            Debug.Log("Wdith: " + m_aRoomData[i].m_fWidth);
            Debug.Log("Height: " + m_aRoomData[i].m_fHeight);
            Debug.Log("Depth: " + m_aRoomData[i].m_fDepth);
        }
    }

    public static RoomData ReturnRoomDataAtIdx(int _idx)
    {
        if (_idx >= m_nNumberOfRooms)
        {
            return m_aRoomData[m_nNumberOfRooms - 1];
        }
        else if (_idx <= 0)
        {
            return m_aRoomData[0];
        }
        else
        {
            return m_aRoomData[_idx];
        }
    }

    public static void SetActiveRoomData(int _idx)
    {
        m_sActiveRoomName   = m_aRoomData[_idx].m_sName;
        m_fActiveRoomWidth  = m_aRoomData[_idx].m_fWidth;
        m_fActiveRoomHeight = m_aRoomData[_idx].m_fHeight;
        m_fActiveRoomDepth  = m_aRoomData[_idx].m_fDepth;

        m_nActiveRoom = _idx;

        Debug.Log("Name: " + m_sActiveRoomName);
        Debug.Log("Breite: " + m_fActiveRoomWidth);
        Debug.Log("Höhe: " + m_fActiveRoomHeight);
        Debug.Log("Tiefe: " + m_fActiveRoomDepth);
    }
}
