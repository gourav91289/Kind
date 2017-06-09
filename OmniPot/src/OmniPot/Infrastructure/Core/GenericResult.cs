
using System;

public class GenericResult
{
    public bool Succeeded { get; set; }
    public string Xl8Key { get; set; } 
    public string Message { get; set; }
    public bool Requires2FA { get; set; }
    public Guid PersonId { get; set; } = Guid.Empty;
}

