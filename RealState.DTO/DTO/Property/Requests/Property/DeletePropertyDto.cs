namespace RealState.Domain.Dto.Property;

public class DeletePropertyDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}