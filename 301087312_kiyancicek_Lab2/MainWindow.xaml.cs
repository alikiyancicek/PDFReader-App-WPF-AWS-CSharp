using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Amazon.Runtime;
using _301087312_kiyancicek_Lab2.Models;
using Table = Amazon.DynamoDBv2.DocumentModel.Table;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _301087312_kiyancicek_Lab2
{

    public partial class MainWindow : Window
    {
        private User user;
        private string tableName = "Users";
        private AmazonDynamoDBClient client;
        private DynamoDBContext context;
        private static BasicAWSCredentials credentials;
        public MainWindow()
        {
            InitializeComponent();
            user = new User();
            this.BtnLogin.IsEnabled = true;
            credentials = AmazonDBClient.getConnection();

        }
        private void TEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            wrong.Content = "";
            user.UserEmail = TEmail.Text;
        }
        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (TEmail.Text != "") creatingTable();
            else wrong.Content = "Please try again.";
        }
        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TEmail.Text != "") await loadDataAsync();
            else wrong.Content = "Please try again with valid informations.";
        }
        private async void creatingTable()
        {
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            context = new DynamoDBContext(client);
            List<string> currentTables = client.ListTablesAsync().Result.TableNames;

            if (!currentTables.Contains(tableName))
            {
                await CreateUserTable(client, tableName);
                await saveUser(context);
            }
            else
            {
                await saveUser(context);
            }
            BtnLogin.IsEnabled = true;
        }
        public static async Task CreateUserTable(AmazonDynamoDBClient client, string tableName)
        {
            var tableResponse = client.ListTablesAsync();
            if (!tableResponse.Result.TableNames.Contains(tableName))
            {
                await Task.Run(() =>
                {
                    client.CreateTableAsync(new CreateTableRequest
                    {
                        TableName = "Users",
                        ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 5, WriteCapacityUnits = 5 },
                        KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName="UserEmail",
                        KeyType=KeyType.HASH
                    }
                },
                        AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName="UserEmail",
                        AttributeType=ScalarAttributeType.S
                    }
                }
                    });

                    CreateTableRequest request = new CreateTableRequest
                    {
                        TableName = "Bookshelf",
                        AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "UserEmail",
                        AttributeType = ScalarAttributeType.S
                    }
                },
                        KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "UserEmail",
                        KeyType = KeyType.HASH
                    }
                },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 5,
                            WriteCapacityUnits = 5
                        },
                    };
                    var response = client.CreateTableAsync(request);
                    Thread.Sleep(5000);
                });
            }
        }
        private async Task saveUser(DynamoDBContext context)
        {
            bool userExisted;
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            Table table = Table.LoadTable(client, "Users");
            Table table2 = Table.LoadTable(client, "Bookshelf");
            string email = TEmail.Text;
            Document doc = await table.GetItemAsync(email);
            if (doc == null)
            {
                userExisted = false;
            }
            else
            {
                userExisted = true;
            }

            user.Password = TPassword.Password;
            var book = new Document();
            book["UserEmail"] = email;
            book["BookTitle1"] = "AWS Certified Solutions";
            book["DateTime1"] = DateTime.Now;
            book["LastPage1"] = 1;
            book["BookTitle2"] = "Beginning Serverless Computing";
            book["DateTime2"] = DateTime.Now;
            book["LastPage2"] = 1;
            book["BookTitle3"] = "Docker";
            book["DateTime3"] = DateTime.Now;
            book["LastPage3"] = 1;

            if (userExisted)
            {
                MessageBox.Show("Account exist already!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                await Task.Run(() =>
                {
                    context.SaveAsync<User>(user);
                    table2.PutItemAsync(book);
                    MessageBox.Show("Account Created Successfully!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                });
            }
        }
        public async Task loadDataAsync()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;
            var credentials = new BasicAWSCredentials(accessKeyID, secretKey);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            Table table = Table.LoadTable(client, "Users");
            string email = TEmail.Text;
            string password = TPassword.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Fields can't be empty!", "Alert", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                Document doc = await table.GetItemAsync(email);
                string emailInput = doc.Values.ElementAt(1);
                string userPasword = doc.Values.ElementAt(0);
                string result = emailInput;
                string pass = userPasword;
                if (email == result & password == pass)
                {

                    BooksList booksForm = new BooksList(emailInput);

                    MessageBox.Show("Successfully Logged In");
                    booksForm.Show();
                }
                else
                {
                    MessageBox.Show("Incorrect Email or Password entered!");
                }
            }
        }
        private void textsing()
        {
            wrong.Content = "User Was Added Please Login";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
