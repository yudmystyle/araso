
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using System.Security.Cryptography;
using System;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Demo1Controller Controller;
    private PhotonView myPhotonView;
    static string QuestionSeeder;
    static string QuestionCategory;
    static string RandomString(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] uintBuffer = new byte[sizeof(uint)];

            while (length-- > 0)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                res.Append(valid[(int)(num % (uint)valid.Length)]);
            }
        }

        return res.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            Debug.Log("Full");

            Controller.StartDuel();
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
        //PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom(); // if it fails to join a room then it will try to create its own
    }

    void CreateRoom()
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = UnityEngine.Random.Range(0, 10000); //creating a random name for the room
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); //attempting to create a new room
        Debug.Log(randomRoomNumber);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        //Controller.StartDuel();
    }
    /*
    [PunRPC]
    public void SetQuestionSeeder()
    {
        string[] category =
        {
            "bahasa",
            "matematika",
            "tubuh dan kesehatan",
            "kuliner",
            "olahraga dan permainan",
            "sains"
        };
        string selected = category[UnityEngine.Random.Range(0, category.Length - 1)];
        QuestionCategory = selected;
    }
    */

}
