using APBDTest1Retake.Models.DTOs;

namespace APBDTest1Retake.Repositories;

public interface IDbRepository
{
    public Task<ClientGetDTO> GetClient(int id);
    public Task<int?> GetCarPricePerDay(int carId);
    public Task<int> AddClientWithCarRental(ClientWithRentalPostDto dto, int totalPrice);
}