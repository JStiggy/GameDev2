using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable()]
public class SaveData : ISerializable
{
    public int version = 1;
    public long saveFlags = 0;
    public float energyReserve = 100f;

    public string saveScene = "Start Room";
    public float saveXPosition = -6.82f;
    public float saveYPosition = -3.73f;

    public int[,] mapData = new int[5, 5]
    {
        { 0,0,0,0,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0},
        { 0,0,0,0,0}
    };


    public SaveData() {}

    public SaveData(SerializationInfo info, StreamingContext ctxt)
    {
        //Get the values from info and assign them to the appropriate properties
        saveFlags = (long)info.GetValue("saveFlags", typeof(long));
        version = (int)info.GetValue("version", typeof(int));
        energyReserve = (float)info.GetValue("energyReserve", typeof(float));
        saveScene = (string)info.GetValue("saveScene", typeof(string));
        saveXPosition = (float)info.GetValue("saveXPosition", typeof(float));
        saveYPosition = (float)info.GetValue("saveYPosition", typeof(float));
        mapData = (int[,])info.GetValue("mapData", typeof(int[,]));
        //Example of Save Data updating between versions, WILL NOT BE USED UNTIL NEAR BETA/ALPHA phase
        //if (version == 2)

        //    saveScene = (string)info.GetValue("saveScene", typeof(string));
        //}
        //else
        //{
        //    saveScene = "Test";
        //    version = 2;
        //}
    }

    //Serialization function.
    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("saveFlags", (saveFlags));
        info.AddValue("version", (version));
        info.AddValue("energyReserve", (energyReserve));
        info.AddValue("saveScene", (saveScene));
        info.AddValue("saveXPosition", (saveXPosition));
        info.AddValue("saveYPosition", (saveYPosition));
        info.AddValue("mapData", (mapData));
    }

    public void Save()
    {
        Stream stream = File.Open("Assets\\Data\\player.dat", FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Binder = new VersionDeserializationBinder();
        Debug.Log("Writing Information");
        bformatter.Serialize(stream, this);
        stream.Close();

    }


    public SaveData Load()
    {
        SaveData data = new SaveData();
        Stream stream = File.Open("Assets\\Data\\player.dat", FileMode.Open);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Binder = new VersionDeserializationBinder();
        Debug.Log("Reading Data");
        data = (SaveData)bformatter.Deserialize(stream);
        stream.Close();
        return data;
    }

}
