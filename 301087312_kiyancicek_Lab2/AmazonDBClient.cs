using Microsoft.Extensions.Configuration;
using System.IO;
using Amazon.Runtime;

namespace _301087312_kiyancicek_Lab2
{
    public static class AmazonDBClient
    {

        public static BasicAWSCredentials getConnection()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true);

            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;
            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);
            return credentials;
        }
    }
}
