namespace Application.Common.Models;

public class ImageWithOptionValueLookupDto
{
    public ImageLookupDto LookupDto { get; set; }
    public Guid? OptionValueId { get; set; }
}
