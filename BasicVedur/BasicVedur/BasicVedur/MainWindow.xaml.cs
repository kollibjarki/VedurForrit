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
using System.IO;
using System.Xml;

namespace BasicVedur
{
    public partial class MainWindow : Window
    {
        public static string stationNumber = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void stationNrBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            stationNumber = stationNrBox.Text;
        }

        private void getInfoButton_Click(object sender, RoutedEventArgs e)
        {
            string basicWeatherPath = "http://xmlweather.vedur.is/?op_w=xml&type=obs&lang=en&view=xml&ids=" + stationNumber;
            XmlDocument xmlBasicWeather = new XmlDocument();
            xmlBasicWeather.Load(basicWeatherPath);

            string xmlString = (xmlBasicWeather.OuterXml);
            if (xmlString.Contains("valid=\"1\""))
            {
                XmlNodeList stadur = xmlBasicWeather.GetElementsByTagName("name"),
                       timi = xmlBasicWeather.GetElementsByTagName("time"),
                       hiti = xmlBasicWeather.GetElementsByTagName("T"),
                       vindstefna = xmlBasicWeather.GetElementsByTagName("D"),
                       vindhradi = xmlBasicWeather.GetElementsByTagName("F"),
                       vedurLysing = xmlBasicWeather.GetElementsByTagName("W");

                string timiStyttur = (timi[0].InnerXml).ToString().Substring(5,11),
                       stuttLysing = (vedurLysing[0].InnerXml).ToString();

                timiTextBox.Text = (timiStyttur);
                stationNumDisplayBox.Text = (stationNumber);
                stadurTextBox.Text = (stadur[0].InnerXml);
                temperatureTextBox.Text = (hiti[0].InnerXml);
                windSpeedTextBox.Text = (vindhradi[0].InnerXml);
                windDirectionTextBox.Text = (vindstefna[0].InnerXml);
                if (stuttLysing == "")
                {
                    shortDescriptionTextBox.Text = "N/A";
                }
                else
                {
                shortDescriptionTextBox.Text = (vedurLysing[0].InnerXml);

                }
                stationNrBox.Text = String.Empty;

            }
            else if (xmlString.Contains("valid=\"0\""))
            {
                XmlNodeList stadur = xmlBasicWeather.GetElementsByTagName("name"),
                       timi = xmlBasicWeather.GetElementsByTagName("time");
                string timiStyttur = (timi[0].InnerXml).ToString().Substring(5);
                timiTextBox.Text = (timiStyttur);
                stationNumDisplayBox.Text = (stationNumber);
                stadurTextBox.Text = (stadur[0].InnerXml);

                MessageBox.Show("Data not accessible at the moment");
                stationNrBox.Text = String.Empty;

            }
            else
            {
                
                MessageBox.Show("Invalid station ID number");
                stationNrBox.Text = String.Empty;

            }
        }

        public void getTextButton_Click(object sender, RoutedEventArgs e)
        {
            string detailedTextPath = "http://xmlweather.vedur.is/?op_w=xml&type=txt&lang=is&view=xml&ids=3";
            XmlDocument xmlDetailDescription = new XmlDocument();
            xmlDetailDescription.Load(detailedTextPath);

            XmlNodeList description = xmlDetailDescription.GetElementsByTagName("content"),
                            descriptionCreation = xmlDetailDescription.GetElementsByTagName("creation"),
                            descriptionValidFrom = xmlDetailDescription.GetElementsByTagName("valid_from"),
                            descriptionValidTo = xmlDetailDescription.GetElementsByTagName("valid_to");

            TextDescriptionTextBox.Text = (description[0].InnerXml);
            desciptionCreatedTextBox.Text = (descriptionCreation[0].InnerXml);
        }

        
    }
}


//{
//}

//(timi[0].InnerXml);
//(hiti[0].InnerXml);
//(vindhradi[0].InnerXml);
//(vindstefna[0].InnerXml);
//(vedurLysing[0].InnerXml);

