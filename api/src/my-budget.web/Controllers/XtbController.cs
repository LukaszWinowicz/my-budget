

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_budget.web.Models;
using my_budget.web.Services;
using xAPI.Commands;
using xAPI.Records;
using xAPI.Responses;
using xAPI.Sync;

namespace my_budget.web.Controllers
{
    [Route("api/xtb")]
    [ApiController]
    public class XtbController : ControllerBase
    {
        private readonly IXtbService _xtbService;
        public XtbController(IXtbService xtbService)
        {
            _xtbService = xtbService;
        }
        
        [HttpGet("GetMyTrades")]
        public IActionResult GetMyTrades()
        {
            try
            {
                var response = _xtbService.GetMyTrades();
                return Ok(response.TradeRecords);
            }
            catch (Exception ex)
            {

                return Unauthorized(new { message = "An exception occured: " + ex.ToString() });
            }
        }

        [HttpGet("GetMyAccountValueByStreaming")]
        public async Task<IActionResult> GetMyAccountValueByStreaming()
        {
            var tcs = new TaskCompletionSource<StreamingBalanceRecord>();

            try
            {
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
                return Ok(new { Balance = balanceRecord.ToString() });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "An exception occured: " + ex.ToString() });
            }
        }


        [HttpGet("GetAllSymbols")]
        public IActionResult GetAllSymbols()
        {
            try
            {
                var response = _xtbService.GetAllSymbols();                
                return Ok(response);
            }
            catch (Exception ex)
            {

                return Unauthorized(new { message = "An exception occured: " + ex.ToString() });
            }
        }
    }
}
