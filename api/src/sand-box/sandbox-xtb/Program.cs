using sandbox_xtb;
using xAPI.Sync;

Server serverData = Servers.DEMO;
SyncAPIConnector connector = new SyncAPIConnector(serverData);

Account account = new Account();
account.Login(1234, "haslo", connector);

account.GetAllSymbols(connector);

