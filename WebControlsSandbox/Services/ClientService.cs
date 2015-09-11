using System.Collections.Generic;
using System.Linq;
using TypeAhead.Models;

namespace TypeAhead.Services
{
    public class ClientService
    {
        public static List<ClientModel> GetClientData(string searchTerm)
        {
            var clients = new List<ClientModel>
            {
                new ClientModel()
                {
                    ClientId = 1,
                    ClientName = "Client 1",
                    CategoryId = 1,
                    CategoryName = "Category 1"
                },
                new ClientModel()
                {
                    ClientId = 2,
                    ClientName = "Client 2",
                    CategoryId = 1,
                    CategoryName = "Category 1"
                },
                new ClientModel()
                {
                    ClientId = 3,
                    ClientName = "Client 3",
                    CategoryId = 1,
                    CategoryName = "Category 1"
                },
                new ClientModel()
                {
                    ClientId = 4,
                    ClientName = "Client Test 4",
                    CategoryId = 2,
                    CategoryName = "Category 2"
                },
                new ClientModel()
                {
                    ClientId = 5,
                    ClientName = "Test Client 5",
                    CategoryId = 2,
                    CategoryName = "Category 2"
                },
                new ClientModel()
                {
                    ClientId = 6,
                    ClientName = "Test Client 6",
                    CategoryId = 2,
                    CategoryName = "Category 2"
                },
                new ClientModel()
                {
                    ClientId = 7,
                    ClientName = "Client 7",
                    CategoryId = 3,
                    CategoryName = "Category 3"
                },
                new ClientModel()
                {
                    ClientId = 8,
                    ClientName = "Client 8",
                    CategoryId = 3,
                    CategoryName = "Category 3"
                },
                new ClientModel()
                {
                    ClientId = 9,
                    ClientName = "Client 9",
                    CategoryId = 3,
                    CategoryName = "Category 3"
                },
                new ClientModel()
                {
                    ClientId = 10,
                    ClientName = "Client 10",
                    CategoryId = 4,
                    CategoryName = "Category 4"
                },
                new ClientModel()
                {
                    ClientId = 11,
                    ClientName = "Another Client (11)",
                    CategoryId = 4,
                    CategoryName = "Category 4"
                },
                new ClientModel()
                {
                    ClientId = 12,
                    ClientName = "12th Client",
                    CategoryId = 4,
                    CategoryName = "Category 4"
                },
                new ClientModel()
                {
                    ClientId = 13,
                    ClientName = "Client Number 13",
                    CategoryId = 5,
                    CategoryName = "Category 5"
                }
            };

            //var keywords = searchTerm.ToLower().Split(' ').ToList();

            //var predicate = PredicateBuilder.False<ClientModel>();

            //foreach (string keyword in keywords)
            //{

            //}

            // This one doesn't work, because it treats each entered search string as equal.
            // var results = clients.Where(item => keywords.Any(keyword => item.ClientName.ToLower().Contains(keyword))).ToList();

            var results = clients.Where(c => c.ClientName.ToLower().Contains(searchTerm.ToLower())).Take(7).ToList();

            return results;
        }

        public static List<ClientModel> PrefetchClients()
        {
            var clients = new List<ClientModel>
            {
                new ClientModel()
                {
                    ClientId = 1,
                    ClientName = "Client 1",
                    CategoryId = 1,
                    CategoryName = "Category 1"
                },
                new ClientModel()
                {
                    ClientId = 2,
                    ClientName = "Client 2",
                    CategoryId = 1,
                    CategoryName = "Category 1"
                },
                new ClientModel()
                {
                    ClientId = 3,
                    ClientName = "Client 3",
                    CategoryId = 1,
                    CategoryName = "Category 1"
                },
                new ClientModel()
                {
                    ClientId = 4,
                    ClientName = "Client Test 4",
                    CategoryId = 2,
                    CategoryName = "Category 2"
                },
                new ClientModel()
                {
                    ClientId = 5,
                    ClientName = "Test Client 5",
                    CategoryId = 2,
                    CategoryName = "Category 2"
                },
                new ClientModel()
                {
                    ClientId = 6,
                    ClientName = "Test Client 6",
                    CategoryId = 2,
                    CategoryName = "Category 2"
                },
                new ClientModel()
                {
                    ClientId = 7,
                    ClientName = "Client 7",
                    CategoryId = 3,
                    CategoryName = "Category 3"
                },
                new ClientModel()
                {
                    ClientId = 8,
                    ClientName = "Client 8",
                    CategoryId = 3,
                    CategoryName = "Category 3"
                },
                new ClientModel()
                {
                    ClientId = 9,
                    ClientName = "Client 9",
                    CategoryId = 3,
                    CategoryName = "Category 3"
                },
                new ClientModel()
                {
                    ClientId = 10,
                    ClientName = "Client 10",
                    CategoryId = 4,
                    CategoryName = "Category 4"
                },
                new ClientModel()
                {
                    ClientId = 11,
                    ClientName = "Another Client (11)",
                    CategoryId = 4,
                    CategoryName = "Category 4"
                },
                new ClientModel()
                {
                    ClientId = 12,
                    ClientName = "12th Client",
                    CategoryId = 4,
                    CategoryName = "Category 4"
                },
                new ClientModel()
                {
                    ClientId = 13,
                    ClientName = "Client Number 13",
                    CategoryId = 5,
                    CategoryName = "Category 5"
                }
            };

            var results = clients.Take(5).ToList();

            return results;
        }
    }
}