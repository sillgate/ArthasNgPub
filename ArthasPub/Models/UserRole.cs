using ArthasPub.Models;
using System;
using System.Linq;
using System.Web.Security;

namespace ArthasPub.Models
{
    public class UserRole : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        /*
        public override string[] GetRolesForUser(string username)
        {
            using (ApplicationDbContext Context = new ApplicationDbContext())
            {
                var userRoles = (from user in Context.Users
                                 join roleMapping in Context.AspNetUserRole
                                 on user.Id equals roleMapping.UserId
                                 join role in Context.Roles
                                 on roleMapping.RoleId equals role.Id
                                 where user.Username == username
                                 select role.RoleName).ToArray();
                return userRoles;
            }
        }
        */
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}