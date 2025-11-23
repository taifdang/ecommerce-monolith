namespace Application.Common.Models;

public class ImageWithOptionValueLookupDto
{
    public ImageLookupDto LookupDto { get; init; }
    public Guid? OptionValueId { get; init; }
}
