using my_budget.web.Models;
using xAPI.Records;
using xAPI.Responses;

namespace my_budget.web.Services
{
    public interface IXtbService
    {
        LoginResponse Login(LoginModel loginModel);
        TradesResponse GetMyTrades();
        //Task<StreamingBalanceRecord> GetMyAccountValueByStreaming();
        string GetCurrentBalance();
        IEnumerable<string> GetAllSymbols();
    }
}
