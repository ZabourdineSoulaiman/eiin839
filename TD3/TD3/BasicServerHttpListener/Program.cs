using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Device.Location;

namespace TD3
{
    internal class Program
    {
        static readonly HttpClient client = new HttpClient();
        public const string apiKey = "b5311bdd1466122d65678117f6ba1bbbda257fbb";

        private static async Task Main(string[] args)
        {
            List<Contract> contracts = await getContracts();
            Position position = new Position(0.0, 0.0);
            foreach(var item in contracts)
            {
                Station closestStation = null;
                closestStation = await getClosestStationAsync(item, position);
                if(closestStation == null)
                {
                    Console.WriteLine(item.name + "'s closest doesn't have any stations.\n");

                }
                else
                {
                    Console.WriteLine(item.name + "'s closest station is : " + closestStation.name + ".\n");

                }
            }

            Console.WriteLine("Enter key to close");
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
                string response = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract.name + "&apiKey=" + Program.apiKey);
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
                string response = await client.GetStringAsync("https://api.jcdecaux.com/vls/v1/stations/" + station.number + "?contract=" + station.contract_name + "&apiKey=" + Program.apiKey);
                stationinfo = JsonConvert.DeserializeObject<Station>(response);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return stationinfo;
        }

        public static async Task<Station> getClosestStationAsync(Contract contract, Position position)
        {
            Station closestStation = null;
            double distance = Double.MaxValue;

            GeoCoordinate coordinate = new GeoCoordinate(position.lat, position.lng);
            List<Station> stations = await getStations(contract);

            foreach(var item in stations)
            {
                if(closestStation == null){
                    closestStation = item;
                    distance = coordinate.GetDistanceTo(new GeoCoordinate(item.position.lat, item.position.lng));
                }
                else
                {
                    if (coordinate.GetDistanceTo(new GeoCoordinate(item.position.lat, item.position.lng)) < distance)
                    {
                        closestStation = item;
                        distance = coordinate.GetDistanceTo(new GeoCoordinate(item.position.lat, item.position.lng));
                    }
                }
            }

            return closestStation;
        }


    }

}