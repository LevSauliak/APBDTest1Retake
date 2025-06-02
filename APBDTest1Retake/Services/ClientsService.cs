using APBDTest1Retake.Exceptions;
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

    public async Task<int> AddClientWithRental(ClientWithRentalPostDto dto)
    {
        int? pricePerDay = await _dbRepository.GetCarPricePerDay(dto.CarId);
        if (pricePerDay == null)
        {
            throw new NotFoundException("No car with this id exists");
        }
        
        int totalPrice = pricePerDay.Value * (dto.DateTo - dto.DateFrom).Days;
        
        return await _dbRepository.AddClientWithCarRental(dto, totalPrice);
    }
}