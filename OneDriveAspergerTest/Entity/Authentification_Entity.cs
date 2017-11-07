using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.Authentication;

namespace OneDriveAspergerTest {
    internal class Authentification_Entity {
        internal OneDriveClient OneDriveClient { get; private set; }

        private string clientID = "3ddba8ef-8549-4f8d-a582-216f6e435613";
        private string[] scopes = { "onedrive.readonly", "onedrive.readwrite", "onedrive.appfolder", "wl.signin" };

        internal Authentification_Entity() {
            Authenticate();
        }

        internal async void Authenticate() {
            MsaAuthenticationProvider msaAuthProvider = new MsaAuthenticationProvider(clientID, "https://login.live.com/oauth20_desktop.srf", scopes);

            await msaAuthProvider.AuthenticateUserAsync();
            OneDriveClient oneDriveClient = new OneDriveClient("https://api.onedrive.com/v1.0", msaAuthProvider);

            OneDriveClient = oneDriveClient;
        }
    }
}