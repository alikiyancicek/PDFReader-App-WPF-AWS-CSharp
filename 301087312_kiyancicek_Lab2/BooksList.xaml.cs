﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Amazon;
using Amazon.Runtime;
using System.Data;
using System.Drawing;
using Table = Amazon.DynamoDBv2.DocumentModel.Table;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _301087312_kiyancicek_Lab2
{

    /// <summary>
    /// Interaction logic for BooksList.xaml
    /// </summary>
    public partial class BooksList : Window
    {
        public string userEmail;
        private BasicAWSCredentials credentials;
        //  private AmazonDynamoDBClient client;
        // private DynamoDBContext context;

        public BooksList(string email)
        {
            InitializeComponent();
            userEmail = email;
            _ = loadDataAsync(userEmail);
            // credentials = AmazonDBClient.getConnection();
        }



        public async Task loadDataAsync(string email)
        {
            credentials = AmazonDBClient.getConnection();
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
            
            Table table = Table.LoadTable(client, "Bookshelf");

            Document doc = await table.GetItemAsync(email);
            string emailInput = doc.Values.ElementAt(4);
            string title1 = doc.Values.ElementAt(2);
            string title2 = doc.Values.ElementAt(5);
            string title3 = doc.Values.ElementAt(6);
            DateTime time1 = (DateTime)doc.Values.ElementAt(3);
            DateTime time2 = (DateTime)doc.Values.ElementAt(0);
            DateTime time3 = (DateTime)doc.Values.ElementAt(1);




            if (DateTime.Compare(time1, time2) > 0 && DateTime.Compare(time2, time3) > 0)
            {
                button1.Content = title1;
                button2.Content = title2;
                button3.Content = title3;
                button1.MouseDoubleClick += buttn1;
                button2.MouseDoubleClick += buttn2;
                button3.MouseDoubleClick += buttn3;

            }
            else if (DateTime.Compare(time2, time1) > 0 && DateTime.Compare(time1, time3) > 0)
            {
                button1.Content = title2;
                button2.Content = title1;
                button3.Content = title3;
                button1.MouseDoubleClick += buttn2;
                button2.MouseDoubleClick += buttn1;
                button3.MouseDoubleClick += buttn3;

            }
            else if (DateTime.Compare(time3, time2) > 0 && DateTime.Compare(time2, time1) > 0)
            {
                button1.Content = title3;
                button2.Content = title2;
                button3.Content = title1;
                button1.MouseDoubleClick += buttn3;
                button2.MouseDoubleClick += buttn2;
                button3.MouseDoubleClick += buttn1;

            }
            else if (DateTime.Compare(time1, time3) > 0 && DateTime.Compare(time3, time2) > 0)
            {
                button1.Content = title1;
                button2.Content = title3;
                button3.Content = title2;
                button1.MouseDoubleClick += buttn1;
                button2.MouseDoubleClick += buttn3;
                button3.MouseDoubleClick += buttn2;

            }
            else if (DateTime.Compare(time3, time1) > 0 && DateTime.Compare(time1, time2) > 0)
            {
                button1.Content = title3;
                button2.Content = title1;
                button3.Content = title2;
                button1.MouseDoubleClick += buttn3;
                button2.MouseDoubleClick += buttn1;
                button3.MouseDoubleClick += buttn2;
            }


            else if (DateTime.Compare(time2, time3) > 0 && DateTime.Compare(time3, time1) > 0)
            {
                button1.Content = title2;
                button2.Content = title3;
                button3.Content = title1;
                button1.MouseDoubleClick += buttn2;
                button2.MouseDoubleClick += buttn3;
                button3.MouseDoubleClick += buttn1;

            }
            else
            {
                button1.Content = title1;
                button2.Content = title2;
                button3.Content = title3;
                button1.MouseDoubleClick += buttn1;
                button2.MouseDoubleClick += buttn2;
                button3.MouseDoubleClick += buttn3;


            }

            booksLabel.Content = "Hello " + emailInput;


        }



        private void buttn1(object sender, RoutedEventArgs e)
        {
            PdfViewer viewForm = new PdfViewer(userEmail, "title1");
            viewForm.Show();

        }

        private void buttn2(object sender, RoutedEventArgs e)
        {
            PdfViewer viewForm = new PdfViewer(userEmail, "title2");
            viewForm.Show();

        }
        private void buttn3(object sender, RoutedEventArgs e)
        {
            PdfViewer viewForm = new PdfViewer(userEmail, "title3");
            viewForm.Show();
        }


    }
}