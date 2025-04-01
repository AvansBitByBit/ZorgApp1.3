using System;

namespace ApiClient.Models
{

    [Serializable]
    public class Patient
    {
        public int id;
        public string voornaam;
        public string achternaam;
        public string geboortedatum;
        public int trajectID;

    }
}

