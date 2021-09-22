using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime LastActive { get; set; } = DateTime.Now;

        public string Gender { get; set; }

        public string Bio { get; set; }

        public string LookingFor { get; set; }

        public string Interest { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public byte[] PasswordHash{get;set;}
        public byte[] PasswordSalt { get; set; }


        // for age
        public int GetAge(){
            return DateOfBirth.CalculateAge();
        }
    }
}