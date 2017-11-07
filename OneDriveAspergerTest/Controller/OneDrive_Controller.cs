using Microsoft.OneDrive.Sdk;
using System.IO;
using System.Threading.Tasks;

namespace OneDriveAspergerTest {
    internal class OneDrive_Controller {
        private readonly Authentification_Entity authentification;

        internal OneDrive_Controller(Authentification_Entity authentification) {
            this.authentification = authentification;
        }

        internal async Task<Item> SaveSketch(FileStream image) {
            Item savedImage = await authentification.OneDriveClient
                        .Drive
                        .Root
                        .ItemWithPath("Test/testing.png")
                        .Content
                        .Request()
                        .PutAsync<Item>(image);

            return savedImage;
        }
    }
}
