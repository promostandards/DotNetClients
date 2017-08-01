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
    class ProductDataClient
    {
        private WSProductData.ProductDataServiceClient wsClient;
        public List<Type> validTypes;
        public string path;


        public ProductDataClient()
        {
            wsClient = new WSProductData.ProductDataServiceClient();
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "PricingAndConfigurationTest.WSProductData"
                    select t;
            validTypes = q.ToList();
        }


        public WSProductData.GetProductResponse getProductData()
        {
            this.setPath("productDataOutput");
            var request = new WSProductData.GetProductRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.localizationCountry = "US";
            request.localizationLanguage = "en";
            request.productId = "1035";
            var request1 = new WSProductData.getProductRequest1(request);
            WSProductData.getProductResponse1 response1 = wsClient.getProduct(request1);
            return response1.GetProductResponse;
        }

        public void setPath(string path)
        {
            this.path = Environment.ExpandEnvironmentVariables(Path.Combine(ConfigurationManager.AppSettings.Get("responseSavePath"), path + ".txt"));
            File.Delete(path);
        }
    }
}
