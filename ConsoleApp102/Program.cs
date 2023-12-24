using System;
using System.Data.SqlClient;
using System.Data;
namespace ConsoleApp102
{
    internal class Program
    {
        static string connectionString = @"Data Source = DESKTOP-2J3MN6S; Initial Catalog = NewsletterList; Trusted_Connection=True; TrustServerCertificate= True";
        static async Task Main()
        {

            if (ConnectToDatabase())
            {
                Console.WriteLine("Підключено до бази даних NewsletterList.\n");

                // Завдання 3
                DisplayAllCustomers();
                DisplayAllEmails();
                DisplayAllSections();
                DisplayAllPromotions();
                DisplayAllCities();
                DisplayAllCountries();

                // Завдання 4
                DisplayCustomersByCity("Kyiv");
                DisplayCustomersByCountry("Ukraine");
                DisplayPromotionsByCountry("Ukraine");
            }
            else
            {
                Console.WriteLine("Помилка підключення до бази даних.\n");
            }
        }
        static async Task<bool> ConnectToDatabaseAsync()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підключення: {ex.Message}");
                return false;
            }
        }
        static bool ConnectToDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підключення: {ex.Message}");
                return false;
            }
        }

        static void DisplayAllCustomers()
        {
            string query = "SELECT * FROM Customers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine("Усі покупці:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"{row["FullName"]}, {row["BirthDate"]}, {row["Gender"]}, {row["Email"]}, {row["Country"]}, {row["City"]}");
                }
                Console.WriteLine();
            }
        }

        static void DisplayAllEmails()
        {
            string query = "SELECT Email FROM Customers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine("Email усіх покупців:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(row["Email"]);
                }
                Console.WriteLine();
            }
        }

        static void DisplayAllSections()
        {
            string query = "SELECT DISTINCT Section FROM Interests";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine("Список розділів:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(row["Section"]);
                }
                Console.WriteLine();
            }
        }

        static void DisplayAllPromotions()
        {
            string query = "SELECT * FROM Promotions";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine("Список акційних товарів:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"{row["Section"]}, {row["Country"]}, {row["StartDate"]}, {row["EndDate"]}");
                }
                Console.WriteLine();
            }
        }

        static void DisplayAllCities()
        {
            string query = "SELECT DISTINCT City FROM Customers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine("Усі міста:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(row["City"]);
                }
                Console.WriteLine();
            }
        }

        static void DisplayAllCountries()
        {
            string query = "SELECT DISTINCT Country FROM Customers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine("Усі країни:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine(row["Country"]);
                }
                Console.WriteLine();
            }
        }

        static void DisplayCustomersByCity(string city)
        {
            string query = $"SELECT * FROM Customers WHERE City = '{city}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine($"Покупці з міста {city}:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"{row["FullName"]}, {row["BirthDate"]}, {row["Gender"]}, {row["Email"]}, {row["Country"]}, {row["City"]}");
                }
                Console.WriteLine();
            }
        }

        static void DisplayCustomersByCountry(string country)
        {
            string query = $"SELECT * FROM Customers WHERE Country = '{country}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine($"Покупці з країни {country}:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"{row["FullName"]}, {row["BirthDate"]}, {row["Gender"]}, {row["Email"]}, {row["Country"]}, {row["City"]}");
                }
                Console.WriteLine();
            }
        }

        static void DisplayPromotionsByCountry(string country)
        {
            string query = $"SELECT * FROM Promotions WHERE Country = '{country}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Console.WriteLine($"Акції для країни {country}:");
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"{row["Section"]}, {row["Country"]}, {row["StartDate"]}, {row["EndDate"]}");
                }
                Console.WriteLine();
            }
        }
        static void InsertNewCustomer(string fullName, DateTime birthDate, string gender, string email, string country, string city)
        {
            string query = $"INSERT INTO Customers (FullName, BirthDate, Gender, Email, Country, City) VALUES ('{fullName}', '{birthDate:yyyy-MM-dd}', '{gender}', '{email}', '{country}', '{city}')";

            ExecuteNonQuery(query);
        }

        static void InsertNewCountry(string country)
        {
            string query = $"INSERT INTO Customers (Country) VALUES ('{country}')";

            ExecuteNonQuery(query);
        }

        static void InsertNewCity(string city)
        {
            string query = $"INSERT INTO Customers (City) VALUES ('{city}')";

            ExecuteNonQuery(query);
        }

        static void InsertNewSection(string section, int customerID)
        {
            string query = $"INSERT INTO Interests (CustomerID, Section) VALUES ({customerID}, '{section}')";

            ExecuteNonQuery(query);
        }

        static void InsertNewPromotion(string section, string country, DateTime startDate, DateTime endDate)
        {
            string query = $"INSERT INTO Promotions (Section, Country, StartDate, EndDate) VALUES ('{section}', '{country}', '{startDate:yyyy-MM-dd}', '{endDate:yyyy-MM-dd}')";

            ExecuteNonQuery(query);
        }

        // Додатковий метод для виконання запитів, які не повертають результати
        static void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        static void UpdateCustomerInfo(int customerID, string fullName, DateTime birthDate, string gender, string email, string country, string city)
        {
            string query = $"UPDATE Customers SET FullName = '{fullName}', BirthDate = '{birthDate:yyyy-MM-dd}', Gender = '{gender}', Email = '{email}', Country = '{country}', City = '{city}' WHERE CustomerID = {customerID}";

            ExecuteNonQuery(query);
        }

        static void UpdateCountryInfo(int countryID, string newCountry)
        {
            string query = $"UPDATE Countries SET Country = '{newCountry}' WHERE CountryID = {countryID}";

            ExecuteNonQuery(query);
        }

        static void UpdateCityInfo(int cityID, string newCity)
        {
            string query = $"UPDATE Cities SET City = '{newCity}' WHERE CityID = {cityID}";

            ExecuteNonQuery(query);
        }

        static void UpdateSectionInfo(int interestID, string newSection)
        {
            string query = $"UPDATE Interests SET Section = '{newSection}' WHERE InterestID = {interestID}";

            ExecuteNonQuery(query);
        }

        static void UpdatePromotionInfo(int promotionID, string newSection, string newCountry, DateTime newStartDate, DateTime newEndDate)
        {
            string query = $"UPDATE Promotions SET Section = '{newSection}', Country = '{newCountry}', StartDate = '{newStartDate:yyyy-MM-dd}', EndDate = '{newEndDate:yyyy-MM-dd}' WHERE PromotionID = {promotionID}";

            ExecuteNonQuery(query);
        }
        static void DeleteCustomer(int customerID)
        {
            string query = $"DELETE FROM Customers WHERE CustomerID = {customerID}";

            ExecuteNonQuery(query);
        }

        static void DeleteCountry(int countryID)
        {
            string query = $"DELETE FROM Countries WHERE CountryID = {countryID}";

            ExecuteNonQuery(query);
        }

        static void DeleteCity(int cityID)
        {
            string query = $"DELETE FROM Cities WHERE CityID = {cityID}";

            ExecuteNonQuery(query);
        }

        static void DeleteSection(int interestID)
        {
            string query = $"DELETE FROM Interests WHERE InterestID = {interestID}";

            ExecuteNonQuery(query);
        }

        static void DeletePromotion(int promotionID)
        {
            string query = $"DELETE FROM Promotions WHERE PromotionID = {promotionID}";

            ExecuteNonQuery(query);
        }
        static void DisplayCitiesByCountry(string country)
        {
            string query = $"SELECT City FROM Customers WHERE Country = '{country}'";

            DisplayResult("Міста у країні {country}:", query);
        }

        static void DisplaySectionsByCustomer(int customerID)
        {
            string query = $"SELECT Section FROM Interests WHERE CustomerID = {customerID}";

            DisplayResult($"Розділи для покупця з ID {customerID}:", query);
        }

        static void DisplayPromotionsBySection(string section)
        {
            string query = $"SELECT * FROM Promotions WHERE Section = '{section}'";

            DisplayResult($"Акційні товари для розділу {section}:", query);
        }

        // Додатковий метод для відображення результатів запитів
        static void DisplayResult(string header, string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine(header);

                while (reader.Read())
                {
                    Console.WriteLine(reader[0]);
                }

                Console.WriteLine();
            }

        }

        static void DisplayCustomersCountByCity()
        {
            string query = "SELECT City, COUNT(*) AS CustomerCount FROM Customers GROUP BY City";

            DisplayResult("Кількість покупців у кожному місті:", query);
        }

        static void DisplayCustomersCountByCountry()
        {
            string query = "SELECT Country, COUNT(*) AS CustomerCount FROM Customers GROUP BY Country";

            DisplayResult("Кількість покупців у кожній країні:", query);
        }

        static void DisplayCitiesCountByCountry()
        {
            string query = "SELECT Country, COUNT(DISTINCT City) AS CityCount FROM Customers GROUP BY Country";

            DisplayResult("Кількість міст у кожній країні:", query);
        }

        static void DisplayAverageCitiesPerCountry()
        {
            string query = "SELECT AVG(CityCount) FROM (SELECT Country, COUNT(DISTINCT City) AS CityCount FROM Customers GROUP BY Country) AS CityCounts";

            DisplayResult("Середня кількість міст по всіх країнах:", query);
        }


        static void DisplaySectionsByCustomerAndCountry(int customerID, string country)
        {
            string query = $"SELECT DISTINCT Section FROM Interests WHERE CustomerID = {customerID} AND Section IN (SELECT Section FROM Promotions WHERE Country = '{country}')";

            DisplayResult($"Розділи для покупця з ID {customerID} у країні {country}:", query);
        }

        static void DisplayPromotionsBySectionAndTimePeriod(string section, DateTime startDate, DateTime endDate)
        {
            string query = $"SELECT * FROM Promotions WHERE Section = '{section}' AND StartDate >= '{startDate:yyyy-MM-dd}' AND EndDate <= '{endDate:yyyy-MM-dd}'";

            DisplayResult($"Акційні товари для розділу {section} за період з {startDate:yyyy-MM-dd} по {endDate:yyyy-MM-dd}:", query);
        }

        static void DisplayPromotionsByCustomer(int customerID)
        {
            string query = $"SELECT * FROM Promotions WHERE Section IN (SELECT Section FROM Interests WHERE CustomerID = {customerID})";

            DisplayResult($"Акційні товари для покупця з ID {customerID}:", query);
        }

        static void DisplayTop3CountriesByCustomersCount()
        {
            string query = "SELECT TOP 3 Country, COUNT(*) AS CustomerCount FROM Customers GROUP BY Country ORDER BY CustomerCount DESC";

            DisplayResult("Топ-3 країни за кількістю покупців:", query);
        }

        static void DisplayBestCountryByCustomersCount()
        {
            string query = "SELECT TOP 1 Country, COUNT(*) AS CustomerCount FROM Customers GROUP BY Country ORDER BY CustomerCount DESC";

            DisplayResult("Найкраща країна за кількістю покупців:", query);
        }

        static void DisplayTop3CitiesByCustomersCount()
        {
            string query = "SELECT TOP 3 City, COUNT(*) AS CustomerCount FROM Customers GROUP BY City ORDER BY CustomerCount DESC";

            DisplayResult("Топ-3 міста за кількістю покупців:", query);
        }

        static void DisplayBestCityByCustomersCount()
        {
            string query = "SELECT TOP 1 City, COUNT(*) AS CustomerCount FROM Customers GROUP BY City ORDER BY CustomerCount DESC";

            DisplayResult("Найкраще місто за кількістю покупців:", query);
        }

    }
}