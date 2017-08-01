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
    class Program
    {
        
        static void Main(string[] args)
        {
            PricingAndConfigurationClient pricingAndConfigurationTester = new PricingAndConfigurationClient();

            //getFobPoints test
            var fobPointsResponse = pricingAndConfigurationTester.getFobPoints();
            printResponse(fobPointsResponse, pricingAndConfigurationTester.path, pricingAndConfigurationTester.validTypes);

            //getConfigurationAndPricing test
            var configurationAndPricesResponse = pricingAndConfigurationTester.getConfigurationAndPricing();
            printResponse(configurationAndPricesResponse, pricingAndConfigurationTester.path, pricingAndConfigurationTester.validTypes);

            //getProductData test
            var productDataTester = new ProductDataClient();
            var productDataResponse = productDataTester.getProductData();
            printResponse(productDataResponse, productDataTester.path, productDataTester.validTypes);

            //getMediaContent test
            var mediaContentTester = new ProductMediaClient();
            var mediaContentResponse = mediaContentTester.getMediaContent();
            printResponse(mediaContentResponse, mediaContentTester.path, mediaContentTester.validTypes);

            Console.WriteLine("Finish response processing, files saved on " + Environment.ExpandEnvironmentVariables(Path.Combine(ConfigurationManager.AppSettings.Get("responseSavePath"))));
            Console.WriteLine("press any key to exit...");
            Console.ReadKey();            
        }

        static void printResponse(object response, string path, List<Type> validTypes)
        {
            var responseType = response.GetType();
            var output = new StringBuilder();
            output.AppendLine("Response Object: " + responseType.Name);
            HierarchyPrinter.validTypes = validTypes;
            foreach (var property in responseType.GetProperties())
            {
                HierarchyPrinter.printMembersValues(property, property.GetValue(response), ref output, 1);
            }
            Console.Write(output.ToString());
            TextWriter writer = new StreamWriter(path);
            writer.Write(output.ToString());
            writer.Flush();
            writer.Close();
        }
    }
}
