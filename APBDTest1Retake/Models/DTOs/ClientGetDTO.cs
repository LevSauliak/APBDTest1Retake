namespace APBDTest1Retake.Models.DTOs;

public class ClientGetDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public List<ClientRentalDTO> Rentals { get; set; }
}

public class ClientRentalDTO
{
    public string Vin { get; set; }
    public string Color {get; set;}
    public string Model {get; set;}
    public DateTime DateFrom {get; set;}
    public DateTime DateTo {get; set;}
    public int TotalPrice {get; set;}
}