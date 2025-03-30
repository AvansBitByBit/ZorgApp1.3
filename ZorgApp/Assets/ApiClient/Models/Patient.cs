using System;

namespace ApiClient.Models
{

    [Serializable]
    public class Patient
    {
        public int ID;
        public string Voornaam;
        public string Achternaam;
        public int TrajectID;

    }
}