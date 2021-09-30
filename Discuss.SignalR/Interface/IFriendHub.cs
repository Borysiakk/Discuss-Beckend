namespace Discuss.SignalR.Interface
{
    public interface IFriendHub
    {
        void SendNotifyStatusFriendsToClient(string idMessage,string destinationClientId);
    }
}