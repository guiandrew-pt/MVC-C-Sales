namespace SalesWebMvc.Models.ViewModels;

public struct ErrorViewModel
{
    public string? RequestId { get; set; }
    public string? Message { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

