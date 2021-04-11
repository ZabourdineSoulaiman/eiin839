using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient();
        public const string apiKey = "b5311bdd1466122d65678117f6ba1bbbda257fbb";

        private static async Task Main(string[] args)
        {
            List<Contract> contracts = await getContracts();
            Contract contract = contracts[0];
            List<Station> stations = await getStations(contract);

            Station stationInfo = await getStationInfo(stations[0]);

            Console.WriteLine(stationInfo.ToString());

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }

        public static async Task<List<Contract>> getContracts()
        {
            List<Contract> contracts = new List<Contract>();
            try
            {
                string response = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + Program.apiKey);
                contracts = JsonConvert.DeserializeObject<List<Contract>>(response);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return contracts;
        }

        public static async Task<List<Station>> getStations(Contract contract)
        {
            List<Station> stations = new List<Station>();
            try
            {
                string response = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract="+ contract.name +"&apiKey=" + Program.apiKey);
                stations = JsonConvert.DeserializeObject<List<Station>>(response);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return stations;
        }

        public static async Task<Station> getStationInfo(Station station)
        {
            Station stationinfo = new Station();
            try
            {
                string response = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations/" + station.number +"?contract=" + station.contract_name + "&apiKey=" + Program.apiKey);
                stationinfo = JsonConvert.DeserializeObject<Station>(response);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return stationinfo;
        }

    }

    /*class Contract
    {
        public string name { get; set; }
        public string commercial_name { get; set; }
        public string country_code { get; set; }
        public List<string> cities { get; set; }

    }

    class Station
    {
        public int number { get; set; }
        public string contract_name { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public Boolean banking { get; set; }
        public Boolean bonus { get; set; }
        public int bike_stands { get; set; }
        public int available_bike_stands { get; set; }
        public int available_bikes { get; set; }
        public string status { get; set; }
        public long last_update { get; set; }
        public Boolean connected { get; set; }
        public Boolean overflow { get; set; }
        public Stands totalStands { get; set; }
        public Stands mainStands { get; set; }
        public Stands overflowStands { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (System.Reflection.PropertyInfo property in this.GetType().GetProperties())
            {
                sb.Append(property.Name);
                sb.Append(": ");
                if (property.GetIndexParameters().Length > 0)
                {
                    sb.Append("Indexed Property cannot be used");
                }
                else
                {
                    sb.Append(property.GetValue(this, null));
                }

                sb.Append(System.Environment.NewLine);
            }

            return sb.ToString();
        }
    }

    class Stands
    {
        public Availabilities availabilities { get; set; }
        public int capacity { get; set; }

    }

    class Availabilities
    {
        public int bikes { get; set; }
        public int stands { get; set; }

    }

    class Position
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }*/

}