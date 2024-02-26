using my_budget.web.Models;
using xAPI.Commands;
using xAPI.Records;
using xAPI.Responses;
using xAPI.Sync;

namespace my_budget.web.Services
{
    public class XtbService : IXtbService
    {
        private static Server serverData = Servers.DEMO;
        private static SyncAPIConnector connector = new SyncAPIConnector(serverData);

        public LoginResponse Login(LoginModel loginModel) 
        {
            string appName = "firstApp";
            string appId = "";

            // Login to server
            Credentials credentials = new Credentials(loginModel.userId, loginModel.password, appId, appName);
            LoginResponse loginResponse = APICommandFactory.ExecuteLoginCommand(connector, credentials, true);
            connector.Streaming.Connect();

            return loginResponse;
        }

        public TradesResponse GetMyTrades()
        {
            TradesResponse tradesResponse = APICommandFactory.ExecuteTradesCommand(connector, true);
            return tradesResponse;
        }

        public async Task<StreamingBalanceRecord> GetMyAccountValueByStreaming()
        {
            var tcs = new TaskCompletionSource<StreamingBalanceRecord>();
            Server serverData = Servers.DEMO;
            SyncAPIConnector connector = new SyncAPIConnector(serverData);
            Credentials credentials = new Credentials("1234", "haslo");
            APICommandFactory.ExecuteLoginCommand(connector, credentials);
            connector.Streaming.Connect();

            // Zmieniona metoda obsługi zdarzeń, aby użyć TaskCompletionSource
            void handler(StreamingBalanceRecord balanceRecord)
            {
                tcs.SetResult(balanceRecord);
                // Opcjonalnie, odłącz handler po otrzymaniu danych
                connector.Streaming.BalanceRecordReceived -= handler;
            }

            connector.Streaming.BalanceRecordReceived += handler;
            connector.Streaming.SubscribeBalance();

            // Oczekiwanie na otrzymanie danych
            var balanceRecord = await tcs.Task;
            return balanceRecord;
        }

        public IEnumerable<string> GetAllSymbols()
        {
            AllSymbolsResponse allSymbolsResponse = APICommandFactory.ExecuteAllSymbolsCommand(connector, true);
            var allSymbols = allSymbolsResponse.SymbolRecords.Select(x => x.Symbol);
            return allSymbols;
        }


        
    }
}
