using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ролевая_модель
{
    [Serializable]
    public class UserData
    {
        public UserData(string Password, string PreviousRole, int CountHead)
        {
            this.Password = Password;
            this.PreviousRole = PreviousRole;
            this.CountHead = CountHead;
        }
        public string Password { get; set; }
        public HashSet<string> roles = new HashSet<string>();
        public string PreviousRole { get; set; }
        public int CountHead { get; set; }
    }
}
