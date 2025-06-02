using APBDTest1Retake.Models.DTOs;

namespace APBDTest1Retake.Services;

public interface IClientsService
{
    public Task<ClientGetDTO> GetClient(int it);
    public Task<int> AddClientWithRental(ClientWithRentalPostDto dto);
}