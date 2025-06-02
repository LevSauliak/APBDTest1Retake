namespace APBDTest1Retake.Models.DTOs;

public class ClientWithRentalPostDto
{
    public Client Client { get; set; }
    public int CarId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}

public class Client {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
}