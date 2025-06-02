using APBDTest1Retake.Models.DTOs;
using APBDTest1Retake.Repositories;

namespace APBDTest1Retake.Services;

public class ClientsService: IClientsService
{
    private IDbRepository _dbRepository;

    public ClientsService(IDbRepository dbRepository)
    {
        _dbRepository = dbRepository;
    }
    public async Task<ClientGetDTO?> GetClient(int id)
    {
        return await _dbRepository.GetClient(id);
    }
}