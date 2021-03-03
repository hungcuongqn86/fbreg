using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCode
{
    class FireBaseMessage
    {
        public string Id { get; set; }
        public string Profile { get; set; }
        public int Type { get; set; }

        public FireBaseMessage()
        {
            Id = "";
            Profile = "";
            Type = 0;
        }

        public FireBaseMessage(string id, string profile, int type)
        {
            Id = id;
            Profile = profile;
            Type = type;
        }
    }
}
