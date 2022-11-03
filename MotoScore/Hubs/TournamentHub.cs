using Microsoft.AspNetCore.SignalR;
namespace MotoScore.Hubs
{
    public class TournamentHub : Hub
    {
        private static List<string> UserList = new List< string>();
        public override async Task OnConnectedAsync()
        {
            UserList.Add(Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserList.Remove(Context.ConnectionId);
        }

        public async Task RefreshTournaments() {
            foreach (var id in UserList) {
                await Clients.Client(id).SendAsync("SendRefreshRequest");
            }
            
        }

    }
}
