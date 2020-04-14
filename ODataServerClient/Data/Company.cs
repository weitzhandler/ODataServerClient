using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;

namespace Server.Data
{
  public class Company
  {
    public int Id { get; set; }

    HashSet<Contact>? _Contacts;
    [Contained]
    [Column(TypeName = "jsonb")]
    public HashSet<Contact> Contacts
    {
      get => _Contacts ??= new HashSet<Contact>();
      private set => _Contacts = value;
    }
  }

  public class Contact
  {
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    HashSet<Phone>? _Phones;
    [Contained]
    public HashSet<Phone> Phones
    {
      get => _Phones ??= new HashSet<Phone>();
      private set => _Phones = value;
    }

  }

  public class Phone
  {
    [Required]
    public string? Number { get; set; }

  }




}
