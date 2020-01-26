using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ролевая_модель
{
    [Serializable]
    public class Administration
    {
        public List<string> UsersNames = new List<string>();
        Dictionary<string, UserData> Users = new Dictionary<string, UserData>();

        public List<string> FilesNames = new List<string>();
        Dictionary<string, Dictionary<string, Rights>> RolesFilesRights = new Dictionary<string, Dictionary<string, Rights>>();
        public Administration()
        {
            foreach(string role in Roles.roles)
            {
                RolesFilesRights[role] = new Dictionary<string, Rights>();
            }
        }

        public void AddUser(string UserName, string Password)
        {
            if (UsersNames.Contains(UserName))
                throw new Exception();

            UsersNames.Add(UserName);
            Users[UserName] = new UserData(Password, null, 0);
            Directory.CreateDirectory($"{UserName}");
        }
        public void RemoveUser(string UserName)
        {
            UsersNames.Remove(UserName);
            Users.Remove(UserName);
        }
        public bool ContainsUserName(string UserName)
        {
            if (UsersNames.Contains(UserName))
                return true;
            else
                return false;
        }
        public bool ContainsUser(string UserName, string Password)
        {
            if (ContainsUserName(UserName) && Users[UserName].Password == Password)
                return true;
            else
                return false;
        }
        public bool GetRoles(string UserName, string RoleName)
        {
            return Users[UserName].roles.Contains(RoleName);
        }
        public bool GetRoles(int i, int j)
        {
            return Users[UsersNames[i]].roles.Contains(Roles.roles[j]);
        }
        public void SetRole(int i, int j, bool value)
        {
            SetRole(UsersNames[i], Roles.roles[j], value);
        }
        public void SetRole(string UserName, string RoleName, bool value)
        {
            if (value == true)
                Users[UserName].roles.Add(RoleName);
            else
                Users[UserName].roles.Remove(RoleName);
        }

        private void ChangeCountHead(string UserName, bool value)
        {
            if (value == true)
            {
                Users[UserName].CountHead++;
                if (Users[UserName].CountHead == 3)
                    throw new Exception();
            }
            else
            {
                Users[UserName].CountHead--;
            }
        }



        public string GetPreviousRole(string UserName)
        {
            return Users[UserName].PreviousRole;
        }
        public void SetPreviousRole(string UserName, string Role)
        {
            Users[UserName].PreviousRole = Role;
        }
        public void AddFile(string FileName, string UserRole)
        {
            if (!Roles.roles.Contains(UserRole))
                throw new Exception();

            AddFile(FileName);
            SetRights(UserRole, FileName, "r", true);
            SetRights(UserRole, FileName, "w", true);
            SetRights(UserRole, FileName, "d", true);
        }
        public void AddFile(string FileName)
        {
            FileStream fs = File.Create(@"Files\" + FileName);
            fs.Close();
            FilesNames.Add(FileName);
            foreach (string role in Roles.roles)
            {
                RolesFilesRights[role][FileName] = new Rights();
            }
        }
        public void RemoveFile(string FileName)
        {
            File.Delete(@"Files\" + FileName);
            FilesNames.Remove(FileName);
            foreach(string role in Roles.roles)
            {
                RolesFilesRights[role].Remove(FileName);
            }
        }
        public Rights GetRights(string Role, string FileName)
        {
            if(!Roles.roles.Contains(Role) || !FilesNames.Contains(FileName))
                throw new Exception();
            return RolesFilesRights[Role][FileName];
        }
        public Rights GetRights(int i, int j)
        {
           return RolesFilesRights[Roles.roles[j]][FilesNames[i]];
        }
        public void SetRights(int i, int j, string RightName, bool value)
        {
            SetRights(Roles.roles[j], FilesNames[i], RightName, value);
        }
        public void SetRights(string RoleName, string FileName, string RightName, bool value)
        {
            switch(RightName)
            {
                case "r":
                    RolesFilesRights[RoleName][FileName].Read = value;
                    break;
                case "w":
                    RolesFilesRights[RoleName][FileName].Write = value;
                    break;
                case "d":
                    RolesFilesRights[RoleName][FileName].Delete = value;
                    break;
            }
            if(value == true)
                foreach(string heir in Roles.Heirs[RoleName])
                {
                    SetRights(heir, FileName, RightName, value);
                }
            else
                foreach (string heir in Roles.Parents[RoleName])
                {
                    SetRights(heir, FileName, RightName, value);
                }
        }
    }
}
