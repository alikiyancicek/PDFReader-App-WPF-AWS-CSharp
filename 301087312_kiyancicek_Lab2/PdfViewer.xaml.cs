using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Windows;
using Amazon.Runtime;
using Table = Amazon.DynamoDBv2.DocumentModel.Table;
using Microsoft.Extensions.Configuration;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;

namespace _301087312_kiyancicek_Lab2
{
    public partial class PdfViewer : Window
    {
        string userEmail;
        string bookTitle;
        public PdfViewer(string email, string title)
        {
            userEmail = email;
            bookTitle = title;
            InitializeComponent();

            if (bookTitle.Equals("title1"))
            {
                pdfviewer1.ItemSource = GetBook1();
            }
            else if (bookTitle.Equals("title2"))
            {
                pdfviewer1.ItemSource = GetBook2();
            }
            else if (bookTitle.Equals("title3"))
            {
                pdfviewer1.ItemSource = GetBook3();
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            if (bookTitle.Equals("title1"))
            {
                closing1();
            }
            else if (bookTitle.Equals("title2"))
            {
                closing2();
            }
            else if (bookTitle.Equals("title3"))
            {
                closing3();
            }
        }

        public async void closing1()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);

            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            DynamoDBContext context = new DynamoDBContext(client);

            UpdateItemOperationConfig config = new UpdateItemOperationConfig
            {
                // Get updated item in response.
                ReturnValues = ReturnValues.AllNewAttributes
            };
            Table table = Table.LoadTable(client, "Bookshelf");
            var user = new Document();
            user["UserEmail"] = userEmail;
            user["DateTime1"] = DateTime.Now;
            user["LastPage1"] = pdfviewer1.CurrentPageIndex;
            await table.UpdateItemAsync(user, config);
            MessageBox.Show("SnapShot Successful");
        }

        public async void closing2()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);

            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            DynamoDBContext context = new DynamoDBContext(client);

            UpdateItemOperationConfig config = new UpdateItemOperationConfig
            {
                // Get updated item in response.
                ReturnValues = ReturnValues.AllNewAttributes
            };
            Table table = Table.LoadTable(client, "Bookshelf");
            var user = new Document();
            user["UserEmail"] = userEmail;
            user["DateTime2"] = DateTime.Now;
            user["LastPage2"] = pdfviewer1.CurrentPageIndex;
            await table.UpdateItemAsync(user, config);
            MessageBox.Show("SnapShot Successful");
        }

        public async void closing3()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);

            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            DynamoDBContext context = new DynamoDBContext(client);

            UpdateItemOperationConfig config = new UpdateItemOperationConfig
            {
                // Get updated item in response.
                ReturnValues = ReturnValues.AllNewAttributes
            };
            Table table = Table.LoadTable(client, "Bookshelf");
            var user = new Document();
            user["UserEmail"] = userEmail;
            user["DateTime3"] = DateTime.Now;
            user["LastPage3"] = pdfviewer1.CurrentPageIndex;
            await table.UpdateItemAsync(user, config);
            MessageBox.Show("SnapShot Successful");
        }

        public MemoryStream GetBook1()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);

            String accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            String secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            AmazonS3Client client = new AmazonS3Client(accessKeyID, secretKey);

            GetObjectRequest request = new GetObjectRequest();
            request.BucketName = "ali001bucket1";
            request.Key = "BeginningServerlessComputing.pdf";
            GetObjectResponse response = client.GetObjectAsync(request).Result;

            MemoryStream docStream = new MemoryStream();
            response.ResponseStream.CopyTo(docStream);
            return docStream;
        }
        public MemoryStream GetBook2()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);

            String accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            String secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            AmazonS3Client client = new AmazonS3Client(accessKeyID, secretKey);

            GetObjectRequest request = new GetObjectRequest();
            request.BucketName = "ali001bucket1";
            request.Key = "AWSCertifiedSolutions.pdf";
            GetObjectResponse response = client.GetObjectAsync(request).Result;

            MemoryStream docStream = new MemoryStream();
            response.ResponseStream.CopyTo(docStream);
            return docStream;
        }
        public MemoryStream GetBook3()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);

            String accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            String secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            AmazonS3Client client = new AmazonS3Client(accessKeyID, secretKey);

            GetObjectRequest request = new GetObjectRequest();
            request.BucketName = "ali001bucket1";
            request.Key = "Docker.pdf";
            GetObjectResponse response = client.GetObjectAsync(request).Result;

            MemoryStream docStream = new MemoryStream();
            response.ResponseStream.CopyTo(docStream);
            return docStream;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bookTitle.Equals("title1"))
            {
                closing1();
            }
            else if (bookTitle.Equals("title2"))
            {
                closing2();
            }
            else if (bookTitle.Equals("title3"))
            {
                closing3();
            }
        }
    }
}