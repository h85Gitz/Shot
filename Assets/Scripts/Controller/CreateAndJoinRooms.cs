using Models;
using Photon.Pun;
using TMPro;

namespace Controller
{
    public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
    {
        //发布者 定义传递的识别参数和信息内容
        public class Publisher: EventNotify
        {
            public static string Name => "TakeMessage";
            public readonly bool IsMultPlay;

            public Publisher(bool isMultPlay)
            {
                IsMultPlay = isMultPlay;
            }
        }

        public TMP_InputField createRoom;
        public TMP_InputField joinRoom;


        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(createRoom.text);
        }

        public void JoinRoom()
        {
            PhotonNetwork.CreateRoom(joinRoom.text);
        }

        public override void OnJoinedRoom()
        {

            NotifierDispense.Notify(Publisher.Name,new Publisher(true));
            // PhotonNetwork.LoadLevel("SampleScene");


        }
    }
}