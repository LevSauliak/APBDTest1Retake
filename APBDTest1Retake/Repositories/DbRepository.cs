using APBDTest1Retake.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace APBDTest1Retake.Repositories;

public class DbRepository: IDbRepository
{
    private readonly IConfiguration _configuration;

    public DbRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ClientGetDTO?> GetClient(int id)
    {
        string query = @"
            SELECT ID, FIRSTNAME, LASTNAME, ADDRESS from clients
            WHERE ID = @id;
            SELECT car.vin, color.Name as Color, model.Name as Model, cr.DateFrom, cr.DateTo, cr.TotalPrice FROM CLIENTS c
            JOIN car_rentals cr on c.ID = cr.ClientID
            JOIN cars car on cr.CarID = car.ID
            JOIN colors color on car.ColorID = color.ID
            JOIN models model on car.ModelID = model.ID
            WHERE c.ID = @id;
        ";
        
        SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        SqlCommand cmd = new SqlCommand(query, conn);
        
        cmd.Parameters.AddWithValue("@id", id);
        await conn.OpenAsync();
        
        SqlDataReader reader = await cmd.ExecuteReaderAsync();

        await reader.ReadAsync();
        if (!reader.HasRows)
        {
            return null;
        }

        ClientGetDTO dto = new ClientGetDTO()
        {
            Id = (int)reader["ID"],
            FirstName = (string)reader["FirstName"],
            LastName = (string)reader["LastName"],
            Address = (string)reader["ADDRESS"],
        };

        await reader.NextResultAsync();

        List<ClientRentalDTO> rentals = new List<ClientRentalDTO>();
        while (await reader.ReadAsync())
        {
            rentals.Add(new ClientRentalDTO()
            {
                Vin = reader["VIN"].ToString(),
                Color = reader["Color"].ToString(),
                Model = reader["Model"].ToString(),
                DateFrom = DateTime.Parse(reader["DateFrom"].ToString()),
                DateTo = DateTime.Parse(reader["DateTo"].ToString()),
                TotalPrice = (int)reader["TotalPrice"],
            });    
        }
        
        dto.Rentals = rentals;

        return dto;
    }
    
    
}