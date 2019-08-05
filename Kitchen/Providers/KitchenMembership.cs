using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;
using Kitchen.Models;
using System.Text.RegularExpressions;

namespace Kitchen.Providers
{
    public static class KitchenMembershipProvider //: MembershipProvider
    {
        public static int ValidateUser(string username, string password)
        {
            int isValid = 0;
            //int userid;
            //string passw;
            using (UsersContext _db = new UsersContext())
            {
                
                var query = (from u in _db.UserProfiles
                             where u.UserName == username
                             join v in _db.KitchenMembership on u.UserId equals v.UserId
                             select new {passw = v.Password, userid = v.UserId });

                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(username);
                if (match.Success)
                    query = (from u in _db.UserProfiles
                                            where u.EmailAddress == username
                                join v in _db.KitchenMembership on u.UserId equals v.UserId
                             select new { passw = v.Password, userid = v.UserId });
                try
                {

                    var user = query.FirstOrDefault();
                    

                    if (user != null && Crypto.VerifyHashedPassword(user.passw, password))
                    {
                        isValid = user.userid;
                    }
                }
                catch
                {
                    isValid = 0;
                }
            }
            return isValid;
        }

        public static MembershipUser CreateUser(string email, string password, string address, string phone, string username)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser == null)
            {
                try
                {
                    using (UsersContext _db = new UsersContext())
                    {
                        UserProfile user = new UserProfile();
                        user.EmailAddress = email;
                        user.Address = address;
                        user.PhoneNumber = phone;
                        user.UserName = username;
                        _db.UserProfiles.Add(user);
                        _db.SaveChanges();
                        membershipUser = GetUser(email, false);
                        return membershipUser;
                    }
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static MembershipUser GetUser(string email, bool userIsOnline)
        {
            try
            {
                using (UsersContext _db = new UsersContext())
                {
                    var users = from u in _db.UserProfiles
                                where u.EmailAddress == email
                                select u;
                    if (users.Count() > 0)
                    {
                        UserProfile user = users.First();
                        return new MembershipUser(Membership.Provider.Name, user.UserName , user.UserId, null, null, null, true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }


        //public override string ApplicationName
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public override bool ValidateUser(string username, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool ChangePassword(string username, string oldPassword, string newPassword)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        //{
        //    throw new NotImplementedException();
        //}

        //public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool DeleteUser(string username, bool deleteAllRelatedData)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool EnablePasswordReset
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override bool EnablePasswordRetrieval
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        //{
        //    throw new NotImplementedException();
        //}
        //public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        //{
        //    throw new NotImplementedException();
        //}
        //public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        //{
        //    throw new NotImplementedException();
        //}
        //public override int GetNumberOfUsersOnline()
        //{
        //    throw new NotImplementedException();
        //}
        //public override string GetPassword(string username, string answer)
        //{
        //    throw new NotImplementedException();
        //}
        //public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        //{
        //    throw new NotImplementedException();
        //}
        //public override string GetUserNameByEmail(string email)
        //{
        //    throw new NotImplementedException();
        //}
        //public override int MaxInvalidPasswordAttempts
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override int MinRequiredNonAlphanumericCharacters
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override int MinRequiredPasswordLength
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override int PasswordAttemptWindow
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override MembershipPasswordFormat PasswordFormat
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override string PasswordStrengthRegularExpression
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override bool RequiresQuestionAndAnswer
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override bool RequiresUniqueEmail
        //{
        //    get { throw new NotImplementedException(); }
        //}
        //public override string ResetPassword(string username, string answer)
        //{
        //    throw new NotImplementedException();
        //}
        //public override bool UnlockUser(string userName)
        //{
        //    throw new NotImplementedException();
        //}
        //public override void UpdateUser(MembershipUser user)
        //{
        //    throw new NotImplementedException();
        //}
    }
}