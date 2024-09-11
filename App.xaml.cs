using Module02Exercise01.View;
using System.Diagnostics;
using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;
using System.Net.Http;
using Module02Exercise01.Resources.Styles;

namespace Module02Exercise01
{
    public partial class App : Application
    {
        public const string TestUrl = "https://google.com";

        private readonly IServiceProvider _serviceProvider;

        

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                this.Resources.MergedDictionaries.Add(new WindowsResources());
            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.Resources.MergedDictionaries.Add(new AndroidResources());

            }

            MainPage = new AppShell();
        }

        // Application Lifecycle Management - OnStart(), OnSleep(), OnResume()
        protected override async void OnStart() //Manages the behavior of the app during startup
        {
            var current = Connectivity.NetworkAccess;

            // if it is able to ping or not
            bool isWebsiteReachable = await IsWebsiteReachable(TestUrl);
            
            if (current == NetworkAccess.Internet && isWebsiteReachable)
            {
                //MainPage = new LoginPage();
                await Shell.Current.GoToAsync("//LoginPage");
                Debug.WriteLine("Application Started");

                MainPage = _serviceProvider.GetRequiredService<LoginPage>();

            }
            else
            {
                //MainPage = new OfflinePage();
                await Shell.Current.GoToAsync("//OfflinePage");
                Debug.WriteLine("Application Started in Offline Mode");

            }
        }
        protected override void OnSleep() //Manages the behavior of the app during suspension
        {
            Debug.WriteLine("Application Sleeping");

        }
        protected override void OnResume() //Manages the behavior of the app during resumption
        {
            Debug.WriteLine("Application Resumed");

        }
        public async Task<bool> IsWebsiteReachable(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;

                }
            }
            catch
            {
                return false;
            }
        }
    }
}
