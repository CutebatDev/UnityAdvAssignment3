using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "/Save.dat";
    private string path;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;


    private void Awake()
    {
        path = Application.persistentDataPath + SAVE_FILE_NAME;
    }


    [ContextMenu("Save!")]
    public void SaveGame()
    {

        using (FileStream fs = new FileStream(path, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            // Position
            writer.Write(gameManager.playerCharacterController.transform.position.x);
            writer.Write(gameManager.playerCharacterController.transform.position.y);
            writer.Write(gameManager.playerCharacterController.transform.position.z);

            // Rotation
            writer.Write(gameManager.playerCharacterController.transform.eulerAngles.x);
            writer.Write(gameManager.playerCharacterController.transform.eulerAngles.y);
            writer.Write(gameManager.playerCharacterController.transform.eulerAngles.z);

            // Stats
            writer.Write(gameManager.playerCharacterController.hp);
            writer.Write(gameManager.playerCharacterController.currentWaypointIndex);
        }
    }

    [ContextMenu("Load!")]
    public void LoadGame()
    {
        if (!File.Exists(path))
        {

#if UNITY_EDITOR
            Debug.LogWarning("No save file found!");
#endif

            return;
        }

        using (FileStream fs = new FileStream(path, FileMode.Open))
        using (BinaryReader reader = new BinaryReader(fs))
        {
            // Position
            float posX = reader.ReadSingle();
            float posY = reader.ReadSingle();
            float posZ = reader.ReadSingle();
            gameManager.playerCharacterController.transform.position = new Vector3(posX, posY, posZ);

            // Rotation
            float rotX = reader.ReadSingle();
            float rotY = reader.ReadSingle();
            float rotZ = reader.ReadSingle();
            gameManager.playerCharacterController.transform.eulerAngles= new Vector3(rotX, rotY, rotZ);

            // Stats
            gameManager.playerCharacterController.hp = reader.ReadInt32();
            gameManager.playerCharacterController.currentWaypointIndex = reader.ReadInt32();

            // Update HP+Destionation
            uiManager.RefreshHPText(gameManager.playerCharacterController.hp);
            gameManager.playerCharacterController.SetDestination(gameManager.playerCharacterController.currentWaypointIndex);
        }
    }
}