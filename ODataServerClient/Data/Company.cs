using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.OData.Builder;
using Newtonsoft.Json;

namespace Server.Data
{

  public class Company
  {
    public int Id { get; set; }

    List<Contact>? _Contacts;

    [Column(TypeName = "jsonb")]
    [Contained]
    [JsonProperty]
    public List<Contact> Contacts
    {
      get => _Contacts ??= new List<Contact>();
      set => _Contacts = value;
    }
  }

  [ComplexType]
  public class Contact
  {
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    List<Phone>? _Phones;

    [Contained]
    [JsonProperty]
    public List<Phone> Phones
    {
      get => _Phones ??= new List<Phone>();
      set => _Phones = value;
    }

  }

  [ComplexType]
  public class Phone
  {
    [Required]
    public string? Number { get; set; }

  }




}
