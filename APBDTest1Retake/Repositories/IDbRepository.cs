using APBDTest1Retake.Models.DTOs;

namespace APBDTest1Retake.Repositories;

public interface IDbRepository
{
    public Task<ClientGetDTO> GetClient(int id);
}