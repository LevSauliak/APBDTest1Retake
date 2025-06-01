using Microsoft.Data.SqlClient;

namespace APBDTest1Retake.Repositories;

public class DbRepository: IDbRepository
{
    private readonly IConfiguration _configuration;

    public DbRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
}