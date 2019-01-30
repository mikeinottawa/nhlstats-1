﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace NHLStats
{
    // Person class is meant for miscellaneous people associated to a game
    // Like coaches and referees
    public class Person
    {
        public string personId { get; set; } // ID of the person
        public string fullName { get; set; } // Full name of the person
        public string role { get; set; } // Role of the person (e.g.:  coach, official, etc.)
        public string subRole { get; set; } // Specific role of the person (e.g.:  referee, linesman, etc.)

        public Person(JObject json)
        {
            // Populating the person object if the type is official
            if (json.ContainsKey("official"))
            {
                personId = json.SelectToken("official.id").ToString();
                fullName = json.SelectToken("official.fullName").ToString();
                role = "Official";
                subRole = json.SelectToken("officialType").ToString();
            }
            else if (json.ContainsKey("position"))
            {
                // Create a dummy ID for a coach as they do not have IDs
                personId = Guid.NewGuid().ToString();
                fullName = json.SelectToken("person.fullName").ToString();
                role = json.SelectToken("position.type").ToString();
            }
        }

    }
}
