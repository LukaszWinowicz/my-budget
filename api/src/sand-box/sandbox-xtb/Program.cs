using xAPI.Commands;
using xAPI.Records;
using xAPI.Responses;
using xAPI.Sync;

Server serverData = Servers.DEMO;
long userId = 12345;
string password = "pass";
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
    // Execute GetServerTime command
    ServerTimeResponse serverTimeResponse = APICommandFactory.ExecuteServerTimeCommand(connector, true);
    Console.WriteLine("Server time: " + serverTimeResponse.TimeString);
    // Execute GetAllSymbols command
    AllSymbolsResponse allSymbolsResponse = APICommandFactory.ExecuteAllSymbolsCommand(connector, true);
    Console.WriteLine("All symbols count: " + allSymbolsResponse.SymbolRecords.Count);
    // Print first 5 symbols
    Console.WriteLine("First five symbols:");
    foreach (SymbolRecord symbolRecord in allSymbolsResponse.SymbolRecords.Take(5))
    {
        Console.WriteLine(" > " + symbolRecord.Symbol + " ask: " + symbolRecord.Ask + " bid: " + symbolRecord.Bid);
    }
}
catch (Exception ex)
{
    Console.WriteLine("An exception occured: " + ex.ToString());
}
Console.Read();

