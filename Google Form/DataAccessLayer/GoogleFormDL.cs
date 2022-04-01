

using Google_Form.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace Google_Form.DataAccessLayer
{
    public class GoogleFormDL : IGoogleFormDL
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        public GoogleFormDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlConnectionString"]);
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            RegistrationResponse response = new RegistrationResponse();
            try
            {

                if(_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"INSERT INTO googleform.GoogleAccount_UserDetail 
                                    (FirstName, LastName, UserName_EmailID, Password)
                                    VALUES
                                    (@FirstName, @LastName, @UserName_EmailID, @Password)";

                using(MySqlCommand sqlCommand = new MySqlCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@FirstName", request.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", request.LastName);
                    sqlCommand.Parameters.AddWithValue("@UserName_EmailID", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if(Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Registration Query Not Executed";
                    }
                }

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }
    }
}
