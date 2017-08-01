using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest
{
    class ProductMediaClient
    {
        private WSMediaContent.MediaContentServiceClient wsClient;
        public List<Type> validTypes;
        public string path;

        public ProductMediaClient()
        {
            wsClient = new WSMediaContent.MediaContentServiceClient();
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "PricingAndConfigurationTest.WSProductData"
                    select t;
            validTypes = q.ToList();
        }


        public WSMediaContent.GetMediaContentResponse getMediaContent()
        {
            this.setPath("mediaContentOutput");
            var request = new WSMediaContent.GetMediaContentRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.mediaType = WSMediaContent.mediaTypeType.Image;
            request.productId = "7050";
            var request1 = new WSMediaContent.getMediaContentRequest1(request);
            WSMediaContent.getMediaContentResponse1 response1 = wsClient.getMediaContent(request1);
            return response1.GetMediaContentResponse;
        }

        public void setPath(string path)
        {
            this.path = Environment.ExpandEnvironmentVariables(Path.Combine(ConfigurationManager.AppSettings.Get("responseSavePath"), path + ".txt"));
            File.Delete(path);
        }
    }
}
