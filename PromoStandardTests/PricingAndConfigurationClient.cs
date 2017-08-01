using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace PricingAndConfigurationTest
{
    class PricingAndConfigurationClient   
    {
        private WSPricingAndConfiguration.PricingAndConfigurationServiceClient wsClient;
        public List<Type> validTypes;
        public string path;

        public PricingAndConfigurationClient()
        {
            wsClient = new WSPricingAndConfiguration.PricingAndConfigurationServiceClient();
            wsClient.Endpoint.Behaviors.Add(new InspectorBehavior());
            wsClient.InnerChannel.OperationTimeout = new TimeSpan(0,10,0);
            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "PricingAndConfigurationTest.WSPricingAndConfiguration"
                    select t;
            validTypes = q.ToList();
        }
        

        public void setPath(string path)
        {
            this.path = Environment.ExpandEnvironmentVariables(Path.Combine(ConfigurationManager.AppSettings.Get("responseSavePath"), path + ".txt"));
            File.Delete(path);
        }

        public WSPricingAndConfiguration.GetAvailableLocationsResponse getAvailableLocations()
        {
            this.setPath("availableLocations");
            var request = new WSPricingAndConfiguration.GetAvailableLocationsRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.productId = "1035";
            var request1 = new WSPricingAndConfiguration.getAvailableLocationsRequest1(request);
            WSPricingAndConfiguration.getAvailableLocationsResponse1 response1 = wsClient.getAvailableLocations(request1);
            return response1.GetAvailableLocationsResponse;
        }

        public WSPricingAndConfiguration.GetDecorationColorsResponse getDecorationColors()
        {
            this.setPath("decorationColors");
            var request = new WSPricingAndConfiguration.GetDecorationColorsRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.productId = "1035";
            request.locationId = 29;
            var request1 = new WSPricingAndConfiguration.getDecorationColorsRequest1(request);
            WSPricingAndConfiguration.getDecorationColorsResponse1 response1 = wsClient.getDecorationColors(request1);
            return response1.GetDecorationColorsResponse;
        }

        public WSPricingAndConfiguration.GetFobPointsResponse getFobPoints()
        {
            this.setPath("fobPoints");
            var request = new WSPricingAndConfiguration.GetFobPointsRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.productId = "1035";
            var request1 = new WSPricingAndConfiguration.getFobPointsRequest1(request);
            WSPricingAndConfiguration.getFobPointsResponse1 response1 = wsClient.getFobPoints(request1);
            return response1.GetFobPointsResponse;
        }

        public WSPricingAndConfiguration.GetAvailableChargesResponse getAvailableCharges()
        {
            this.setPath("availableCharges");
            var request = new WSPricingAndConfiguration.GetAvailableChargesRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.productId = "1035";
            var request1 = new WSPricingAndConfiguration.getAvailableChargesRequest1(request);
            WSPricingAndConfiguration.getAvailableChargesResponse1 response1 = wsClient.getAvailableCharges(request1);
            return response1.GetAvailableChargesResponse;
        }

        public WSPricingAndConfiguration.GetConfigurationAndPricingResponse getConfigurationAndPricing()
        {
            this.setPath("pricingAndConfigurationOutput");
            var request = new WSPricingAndConfiguration.GetConfigurationAndPricingRequest();
            request.wsVersion = "1.0.0";
            request.id = ConfigurationManager.AppSettings.Get("wsUserName");
            request.password = ConfigurationManager.AppSettings.Get("wsPassword");
            request.priceType = WSPricingAndConfiguration.priceType.List;
            request.currency = WSPricingAndConfiguration.CurrencyCodeType.USD;
            request.fobId = "1";
            request.productId = "1035";
            var request1 = new WSPricingAndConfiguration.getConfigurationAndPricingRequest1(request);
            try
            {
                WSPricingAndConfiguration.getConfigurationAndPricingResponse1 response1 = wsClient.getConfigurationAndPricing(request1);
                var response = response1.GetConfigurationAndPricingResponse;
                return response;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
