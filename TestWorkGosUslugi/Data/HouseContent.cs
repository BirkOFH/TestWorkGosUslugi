namespace TestWorkGosUslugi.Data
{
  public class HouseContent
  {
    //      var content = new StringContent("{\"regionProperty\":null,\"municipalProperty\":null,\"hostelTypeCodes\":null}", null, "application/json");
    public Guid regionCode { get; set; } = Guid.Parse("27eb7c10-a234-44da-a59c-8b1f864966de"); //Guid.Empty;
    public bool calcCount { get; set; } = true;
    public string cadastreNumber { get; set; } = "74:33:0303002:177";
    public string[] statuses { get; set; } = new []{ "APPROVED" };
    public string? regionProperty { get; set; } = null;
    public string? municipalProperty { get; set; } = null;
    public string? hostelTypeCodes { get; set; } = null;

  }
}
