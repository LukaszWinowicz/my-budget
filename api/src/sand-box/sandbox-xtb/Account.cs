using xAPI.Commands;
using xAPI.Records;
using xAPI.Responses;
using xAPI.Sync;

namespace sandbox_xtb
{



    public class Account
    {
        public void Login(long userId, string password, SyncAPIConnector connector)
        {
            //Server serverData = Servers.DEMO;
            string appName = "firstApp";
            // should be left blank
            // to get more info about appId and appName visit http://developers.xstore.pro/api/tutorials/appid_and_appname
            string appId = "";
            try
            {
               // Console.WriteLine("Server address: " + serverData.Address + " port: " + serverData.MainPort + " streaming port: " + serverData.StreamingPort);

                // Connect to server
                //SyncAPIConnector connector = new SyncAPIConnector(serverData);
                Console.WriteLine("Connected to the server");

                // Login to server
                Credentials credentials = new Credentials(userId, password, appId, appName);
                LoginResponse loginResponse = APICommandFactory.ExecuteLoginCommand(connector, credentials, true);
                Console.WriteLine("Logged in as: " + userId);

                // Execute GetServerTime command
                ServerTimeResponse serverTimeResponse = APICommandFactory.ExecuteServerTimeCommand(connector, true);
                Console.WriteLine("Server time: " + serverTimeResponse.TimeString);

                // Token
                Console.WriteLine("Token: " + loginResponse.StreamSessionId);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured: " + ex.ToString());
            }
            //Console.Read();
        }

        public void GetAllSymbols(SyncAPIConnector connector)
        {
            // Execute GetAllSymbols command
            AllSymbolsResponse allSymbolsResponse = APICommandFactory.ExecuteAllSymbolsCommand(connector, true);
            Console.WriteLine("All symbols count: " + allSymbolsResponse.SymbolRecords.Count);
            // Print all symbols
            Console.WriteLine("All symbols:");
            foreach (SymbolRecord symbolRecord in allSymbolsResponse.SymbolRecords)
            {
                Console.WriteLine(" > " + symbolRecord.Description + " | " + symbolRecord.Symbol + " ask: " + symbolRecord.Ask + " bid: " + symbolRecord.Bid);
            }
            
            Console.Read();

        }
    }
}