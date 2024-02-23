using xAPI.Commands;
using xAPI.Records;
using xAPI.Responses;
using xAPI.Sync;

namespace sandbox_xtb
{
    public class AccountStreaming
    {
        public void Login(long userId, string password)
        {
            Server serverData = Servers.DEMO;
            string appName = "firstApp";
            // should be left blank
            // to get more info about appId and appName visit http://developers.xstore.pro/api/tutorials/appid_and_appname
            string appId = "";
            try
            {
                Console.WriteLine("Server address: " + serverData.Address + " port: " + serverData.MainPort + " streaming port: " + serverData.StreamingPort);

                // Connect to server
                SyncAPIConnector connector = new SyncAPIConnector(serverData);
                Console.WriteLine("Connected to the server");

                // Login to server
                Credentials credentials = new Credentials(userId, password, appId, appName);
                LoginResponse loginResponse = APICommandFactory.ExecuteLoginCommand(connector, credentials, true);
                Console.WriteLine("Logged in as: " + userId);

                // Connection to streaming
                Console.WriteLine("Connection to streaming");
                connector.Streaming.Connect();

            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured: " + ex.ToString());
            }
            Console.Read();
        }



    }
}
