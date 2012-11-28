namespace Papercut.WebHost
{
    using System.Configuration;

    using Funq;

    using Papercut.Smtp;

    using ServiceStack.WebHost.Endpoints;

    public class PapercutHttpListener
        : AppHostHttpListenerBase
    {
        private string listeningOn;

        public PapercutHttpListener()
            : base("HttpListener Host for Papercut", typeof(MailboxService).Assembly) { }

        public AppConfig Config { get; set; }

        public string ListeningOn
        {
            get
            {
                return this.listeningOn;
            }
        }

        public override void Configure(Container container)
        {
            ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.JsonDateHandler.ISO8601;

            this.Config = new AppConfig
                {
                    MailFolder = ConfigurationManager.AppSettings["MailFolder"]
                };

            container.Register(this.Config);
        }

        public override void Start(string urlBase)
        {
            listeningOn = urlBase;
            Logger.Write("Server Ready - Listening for new connections " + listeningOn + "...");
            base.Start(urlBase);
        }
    }
}