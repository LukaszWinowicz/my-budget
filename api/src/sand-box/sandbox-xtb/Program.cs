using sandbox_xtb;
using xAPI.Commands;
using xAPI.Records;
using xAPI.Sync;

//Server serverData = Servers.DEMO;
//SyncAPIConnector connector = new SyncAPIConnector(serverData);

//Account account = new Account();
//account.Login(1234, "haslo", connector);

////account.GetAllSymbols(connector);

//account.GetMyTrades(connector);

SyncAPIConnector connector = new SyncAPIConnector(Servers.DEMO);
Credentials credentials = new Credentials("1234", "haslo");
APICommandFactory.ExecuteLoginCommand(connector, credentials);
connector.Streaming.Connect();
connector.Streaming.BalanceRecordReceived += Streaming_BalanceRecordReceived;
connector.Streaming.SubscribeBalance();


void Streaming_BalanceRecordReceived(StreamingBalanceRecord balanceRecord)
{
    Console.WriteLine("Got balance: " + balanceRecord.ToString());
    // do something with balance record here
}