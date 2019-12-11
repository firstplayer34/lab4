using System;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public class Station
    {
        public string city;
        public string arrivalTime;
        public string departureTime;
        public string toString()
        {
            return String.Format("" +
                "\tCity: {0}" +
                "\tArrival time: {1}" +
                "\tArrival city: {2}"
                , this.city, this.arrivalTime, this.departureTime);
        }
        public Station(string a,string b, string c)
        {
            this.city = a;
            this.arrivalTime = b;
            this.departureTime = c;
        }
    }
    public class Train
    {
        public string number;
        public string arrivalCity, departureCity, arrivalTime, departureTime;
        public List<Station> stops;
        public Train(string a, string b, string c, string d, string e, List<Station> stops)
        {
            this.number = a;
            this.arrivalCity = b;
            this.departureCity = c;
            this.arrivalTime = d;
            this.departureTime = e;
            this.stops = stops;
        }
        public String toString()
        {
            var s = String.Format(
                "Train number {0}\n" +
                "Departure city {1}\n" +
                "Departure tine {2}\n" +
                "Arrival city {3}\n" +
                "Arrival time {4}\n",
                this.number, this.departureCity, this.departureTime, this.arrivalCity, this.arrivalTime);
            foreach (Station stop in stops)
            {
                s += stop.toString() + '\n';
            }
            return s;
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("E://input.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            Train a = new Train("","","","","",null);
            a.number = xRoot.Attributes[0].Value;
            foreach (XmlNode node in xRoot)
            {
                if (node.Name == "Departure")
                {
                    a.departureTime = node.InnerText;
                    //string s = node.InnerText;
                    //richTextBox1.Text += "Отправка: " + s + '\n';
                }
                if (node.Name == "Arrival")
                {
                    a.arrivalTime = node.InnerText;
                    //richTextBox1.Text += String.Format("Прибытие: {0}\n", node.InnerText);
                }
                if (node.Name == "DepartureCity")
                {
                    a.departureCity = node.InnerText;
                    //richTextBox1.Text += String.Format("Город отправки: {0}\n", node.InnerText);
                }
                if (node.Name == "ArrivalCity")
                    a.arrivalCity = node.InnerText;
                    //richTextBox1.Text += String.Format("Город прибытия: {0}\n", node.InnerText);
                if (node.Name == "Route")
                {
                    List<Station> stops = new List<Station>();
                    //richTextBox1.Text += "Путь: \n";
                    //string city = "";
                    //string departure = "";
                    //string arrival = "";
                    foreach (XmlNode item1 in node.ChildNodes)
                    {
                        Station stop = new Station( "", "", "");
                        if (item1.Name == "Item")
                        {
                            foreach (XmlNode item in item1.ChildNodes)
                            {
                                if (item.Name == "City") stop.city = item.InnerText; /*city += item.InnerText;*/
                                if (item.Name == "Departure") stop.departureTime = item.InnerText; /*departure += item.InnerText;*/
                                if (item.Name == "Arrival") stop.arrivalTime = item.InnerText; /*arrival += item.InnerText;*/
                            }
                            stops.Add(stop);

                            //if (city != "") richTextBox1.Text += String.Format("\tГород: {0}", city);
                            //if (departure != "") richTextBox1.Text += String.Format("\tОтправление: {0}", departure);
                            //if (arrival != "") richTextBox1.Text += String.Format("\tПрибытие: {0}", arrival);
                            //richTextBox1.Text += '\n';
                            //city = "";
                            //departure = "";
                            //arrival = "";
                        }
                    }
                    a.stops = stops;
                }
            }
            richTextBox1.Text += a.toString();
        }
    }
}
