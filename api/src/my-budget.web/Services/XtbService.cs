using my_budget.web.Models;
using xAPI.Commands;
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

            return loginResponse;
        }

        public TradesResponse GetMyTrades()
        {
            TradesResponse tradesResponse = APICommandFactory.ExecuteTradesCommand(connector, true);
            return tradesResponse;
        }

        //public string GetMyAccountValue()
        //{
        //    return "null";
        //}

        public IEnumerable<string> GetAllSymbols()
        {
            AllSymbolsResponse allSymbolsResponse = APICommandFactory.ExecuteAllSymbolsCommand(connector, true);
            var allSymbols = allSymbolsResponse.SymbolRecords.Select(x => x.Symbol);
            return allSymbols;
        }
    }
}
