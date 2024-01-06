using App1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
namespace App1.Services
{


    public class SqlDataStore : IDataStore<Users>
    {
        private readonly string connectionString = @"Data Source=DESKTOP-TB81M5D\SQLEXPRESS;Initial Catalog=Tourism;Integrated Security=True;Trust Server Certificate=True";

        public SqlDataStore(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<bool> AddItemAsync(Users item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Здесь вы можете выполнить SQL-запрос для добавления элемента в базу данных
                    // Пример: INSERT INTO YourTable (Id, Text, Description) VALUES ('new_guid', 'New item', 'Item description')
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при добавлении элемента
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateItemAsync(Users item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Здесь вы можете выполнить SQL-запрос для обновления элемента в базе данных
                    // Пример: UPDATE YourTable SET Text = 'Updated item', Description = 'Updated description' WHERE Id = 'item_id'
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при обновлении элемента
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Здесь вы можете выполнить SQL-запрос для удаления элемента из базы данных
                    // Пример: DELETE FROM YourTable WHERE Id = 'item_id'
                }

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при удалении элемента
                return await Task.FromResult(false);
            }
        }

        public async Task<Users> GetItemAsync(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Здесь вы можете выполнить SQL-запрос для получения элемента из базы данных
                    // Пример: SELECT * FROM YourTable WHERE Id = 'item_id'
                }

                return await Task.FromResult(new Users()); // Замените на фактический код получения элемента
            }
            catch (Exception ex)
            {
                // Обработка ошибок при получении элемента
                return null;
            }
        }

        public async Task<IEnumerable<Users>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Здесь вы можете выполнить SQL-запрос для получения всех элементов из базы данных
                    // Пример: SELECT * FROM YourTable
                }

                return await Task.FromResult(new List<Users>()); // Замените на фактический код получения всех элементов
            }
            catch (Exception ex)
            {
                // Обработка ошибок при получении всех элементов
                return null;
            }
        }


        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);



                        var result = command.ExecuteScalar();
                        return result != null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок при аутентификации
                Console.WriteLine($"Error Code: {ex.HResult}, Message: {ex.Message}");
                return false;
            }
        }
    }


}
