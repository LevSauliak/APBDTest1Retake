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

    public async Task<int?> GetCarPricePerDay(int carId)
    {
        string query = @"
            SELECT PricePerDay from cars where ID = @carId;
        ";
        SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@carId", carId);
        
        await conn.OpenAsync();
        var price = await cmd.ExecuteScalarAsync();
        if (price == null || price == DBNull.Value)
        {
            return null;
        }

        return (int)price;
    }

    public async Task<int> AddClientWithCarRental(ClientWithRentalPostDto dto, int totalPrice)
    {
        string queryClient = @"
            insert into clients(FirstName, LastName, Address) 
            OUTPUT INSERTED.ID
            values (@FirstName, @LastName, @Address);
        ";
        string queryCarRental = @"
            insert into car_rentals(clientid, carid, datefrom, dateto, totalprice)
            values (@ClientId, @CarId, @DateFrom, @DateTo, @TotalPrice);
        ";
        
        await using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        
        await conn.OpenAsync();
        await using var transaction = await conn.BeginTransactionAsync();

        try
        {
            await using var cmd1 = new SqlCommand(queryClient, conn);
            cmd1.Transaction = transaction as SqlTransaction;
            cmd1.Parameters.AddWithValue("FirstName", dto.Client.FirstName);
            cmd1.Parameters.AddWithValue("LastName", dto.Client.LastName);
            cmd1.Parameters.AddWithValue("Address", dto.Client.Address);

            var clientId = Convert.ToInt32(await cmd1.ExecuteScalarAsync());
            Console.WriteLine(clientId);

            await using var cmd2 = new SqlCommand(queryCarRental, conn);
            cmd2.Transaction = transaction as SqlTransaction;
            cmd2.Parameters.AddWithValue("CarId", dto.CarId);
            cmd2.Parameters.AddWithValue("DateFrom", dto.DateFrom);
            cmd2.Parameters.AddWithValue("DateTo", dto.DateTo);
            cmd2.Parameters.AddWithValue("TotalPrice", totalPrice);
            cmd2.Parameters.AddWithValue("ClientId", clientId);
            await cmd2.ExecuteNonQueryAsync();

            await transaction.CommitAsync();
            return clientId;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }
}