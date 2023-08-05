using VladyKsenicUmbracoProjekt.lib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;
using Umbraco.Core.Services;
using Umbraco.Web.Security;

namespace VladyKsenicUmbracoProjekt.lib.Repositories
{
    public class OsobnaStrankaMemberRepository 
    {
        public const string OsobnaStrankaMemberTypeAlias = "osobnaStrankaMember";
        //public const string OsobnaStrankaMemberCourseRole = "";
        public List<OsobnaStrankaMember> GetAll(string sortBy = "Name", string sortDir = "ASC")
        {
            List<OsobnaStrankaMember> dataList = new List<OsobnaStrankaMember>();
            OsobnaStrankaMemberRolesInfo rolesInfo = new OsobnaStrankaMemberRolesInfo();

            foreach (IMember member in GetAllMembers(sortBy, sortDir))
            {
                OsobnaStrankaMember dataRec = OsobnaStrankaMember.CreateCopyFrom(member);
                dataRec.IsCourseUser = rolesInfo.IsCourseMember(this.MemberService, member);
                dataList.Add(dataRec);
            }

            return dataList;
        }

        public List<OsobnaStrankaMember> GetCourseUsers(string sortBy = "Name", string sortDir = "ASC")
        {
            List<OsobnaStrankaMember> dataList = new List<OsobnaStrankaMember>();
            OsobnaStrankaMemberRolesInfo rolesInfo = new OsobnaStrankaMemberRolesInfo();

            foreach (IMember member in GetAllMembers(sortBy, sortDir))
            {
                OsobnaStrankaMember dataRec = OsobnaStrankaMember.CreateCopyFrom(member);
                if (rolesInfo.IsCourseMember(this.MemberService, member))
                {
                    dataList.Add(dataRec);
                }
            }

            return dataList;
        }

        IEnumerable<IMember> GetAllMembers(string sortBy = "Name", string sortDir = "ASC")
        {
            long totalRecords;

            return this.MemberService.GetAll(0, _PagingModel.AllItemsPerPage, out totalRecords, sortBy, sortDir == "DESC" ? Direction.Descending : Direction.Ascending, OsobnaStrankaMemberRepository.OsobnaStrankaMemberTypeAlias);
        }

        public OsobnaStrankaMember GetCurrentMember()
        {
            return Get(OsobnaStrankaMember.GetCurrentMemberId());
        }

        public OsobnaStrankaMember Get(int id)
        {
            return CreateCopyFrom(this.MemberService.GetById(id));
        }

        public OsobnaStrankaMember Get(string id)
        {
            return Get(int.Parse(id));
        }

        public OsobnaStrankaMember GetMemberByEmail(string email)
        {
            IMember member = this.MemberService.GetByEmail(email);
            return member == null ? null : CreateCopyFrom(member);
        }

        OsobnaStrankaMember CreateCopyFrom(IMember imember)
        {
            OsobnaStrankaMember member = OsobnaStrankaMember.CreateCopyFrom(imember);

            member.IsCourseUser = System.Web.Security.Roles.IsUserInRole(member.Username, OsobnaStrankaMemberRepository.OsobnaStrankaMemberCourseRole);

            return member;

        }
        public MembershipCreateStatus Save(MembershipHelper members, OsobnaStrankaMember member, bool updatePermissions = true)
        {
            if (member.IsNew)
            {
                return Insert(members, member);
            }
            else
            {
                return Update(member, updatePermissions);
            }
        }

        public MembershipCreateStatus Insert(MembershipHelper members, OsobnaStrankaMember member)
        {
            if (this.MemberService.GetById(member.MemberId) != null)
            {
                return MembershipCreateStatus.DuplicateProviderUserKey;
            }

            var registerModel = members.CreateRegistrationModel(OsobnaStrankaMemberRepository.OsobnaStrankaMemberTypeAlias);
            registerModel.Name = member.Name;
            registerModel.Email = member.Email;
            registerModel.Password = member.Password;
            registerModel.Username = member.Email;
            registerModel.UsernameIsEmail = true;

            MembershipCreateStatus status;
            var newMember = members.RegisterMember(registerModel, out status, false);

            if (status == MembershipCreateStatus.Success)
            {
                // Assign user roles
                if (member.IsCourseUser)
                {
                    System.Web.Security.Roles.AddUserToRole(member.Username, OsobnaStrankaMemberRepository.OsobnaStrankaMemberCourseRole);
                }
            }

            return status;
        }

        public MembershipCreateStatus Update(OsobnaStrankaMember member, bool updatePermissions)
        {
            IMember updateMember = this.MemberService.GetById(member.MemberId);
            if (updateMember == null)
            {
                return MembershipCreateStatus.UserRejected;
            }

            bool wasChange = false;

            if (updateMember.Name != member.Name)
            {
                updateMember.Name = member.Name;
                wasChange = true;
            }
            if (updateMember.Email != member.Email)
            {
                updateMember.Username = member.Email;
                updateMember.Email = member.Email;
                IMember checkMember = this.MemberService.GetByEmail(updateMember.Email);
                if (checkMember != null)
                {
                    return MembershipCreateStatus.DuplicateEmail;
                }
                wasChange = true;
            }
            if (updatePermissions)
            {
                if (updateMember.IsApproved != member.IsApproved)
                {
                    updateMember.IsApproved = member.IsApproved;
                    wasChange = true;
                }
                if (updateMember.IsLockedOut != member.IsLockedOut)
                {
                    updateMember.IsLockedOut = member.IsLockedOut;
                    wasChange = true;
                }
            }

            if (wasChange)
            {
                this.MemberService.Save(updateMember);
            }

            if (updatePermissions)
            {
                // Assign user roles
                if (member.IsCourseUser)
                {
                    System.Web.Security.Roles.AddUserToRole(member.Username, OsobnaStrankaMemberRepository.OsobnaStrankaMemberCourseRole);
                }
                else
                {
                    System.Web.Security.Roles.RemoveUserFromRole(member.Username, OsobnaStrankaMemberRepository.OsobnaStrankaMemberCourseRole);
                }
            }

            return MembershipCreateStatus.Success;
        }

        public MembershipCreateStatus SavePassword(OsobnaStrankaMember member)
        {
            IMember updateMember = this.MemberService.GetById(member.MemberId);
            if (updateMember == null)
            {
                return MembershipCreateStatus.UserRejected;
            }

            try
            {
                this.MemberService.SavePassword(updateMember, member.Password);
            }
            catch
            {
                return MembershipCreateStatus.InvalidPassword;
            }

            return MembershipCreateStatus.Success;
        }

        public MembershipCreateStatus Delete(OsobnaStrankaMember member)
        {
            IMember deleteMember = this.MemberService.GetById(member.MemberId);
            if (deleteMember == null)
            {
                return MembershipCreateStatus.UserRejected;
            }
            this.MemberService.Delete(deleteMember);

            return MembershipCreateStatus.Success;
        }

        public string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.Success:
                    return string.Empty;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    return "Užívateľ už existuje.";
                case MembershipCreateStatus.InvalidUserName:
                    return "Neplatné meno užívateľa.";
                case MembershipCreateStatus.InvalidPassword:
                    return "Neplatné heslo. Heslo musí mať aspoň 8 znakov.";
                case MembershipCreateStatus.InvalidQuestion:
                    return "Neplatná otázka.";
                case MembershipCreateStatus.InvalidAnswer:
                    return "Neplatná odpoveď.";
                case MembershipCreateStatus.InvalidEmail:
                    return "Neplatný e-mail.";
                case MembershipCreateStatus.DuplicateUserName:
                case MembershipCreateStatus.DuplicateEmail:
                    return "Duplicitný e-mail.";
                case MembershipCreateStatus.UserRejected:
                case MembershipCreateStatus.InvalidProviderUserKey:
                    return "Užívateľ zamietnutý.";
                case MembershipCreateStatus.ProviderError:
                    return "Systémová chyba. Kontaktujte nás.";
            }

            return "Unknown error.";
        }
    }

    public class OsobnaStrankaMember
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }

        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }

        public bool IsCourseUser { get; set; }

        public bool IsNew
        {
            get
            {
                return this.MemberId <= 0;
            }
        }

        public MembershipCreateStatus Success { get; internal set; }

        public static OsobnaStrankaMember CreateCopyFrom(IMember member)
        {
            return new OsobnaStrankaMember()
            {
                MemberId = member.Id,
                Name = member.Name,
                Username = member.Username,
                Email = member.Email,
                IsApproved = member.IsApproved,
                IsLockedOut = member.IsLockedOut,
            };
        }

        public static OsobnaStrankaMember CreateNewCourseMember(string email)
        {
            OsobnaStrankaMember trg = new OsobnaStrankaMember();
            trg.MemberId = 0;
            trg.Name = email;
            trg.Username = email;
            trg.Email = email;
            trg.IsApproved = true;
            trg.IsLockedOut = false;
            trg.IsCourseUser = true;

            DateTime dt = DateTime.Now;
            string pswd = dt.Ticks.ToString();

            trg.Password = pswd;
            trg.PasswordRepeat = pswd;

            return trg;
        }

        public static int GetCurrentMemberId()
        {
            MembershipUser user = GetCurrentUser();
            return (int)user.ProviderUserKey;
        }
        static MembershipUser GetCurrentUser()
        {
            return Membership.GetUser();
        }
    }

    public class OsobnaStrankaMemberRolesInfo
    {
        Hashtable htCourse;

        public bool IsCourseMember(IMemberService service, IMember member)
        {
            return IsMemberInRole(service, member, OsobnaStrankaMemberRepository.OsobnaStrankaMemberCourseRole, ref htCourse);
        }

        bool IsMemberInRole(IMemberService service, IMember member, string roleName, ref Hashtable ht)
        {
            if (ht == null)
            {
                ht = LoadRolesInfo(service, roleName);
            }

            return ht.ContainsKey(member.Id);
        }

        Hashtable LoadRolesInfo(IMemberService service, string roleName)
        {
            Hashtable ht = new Hashtable();
            foreach (IMember member in service.GetMembersInRole(roleName))
            {
                ht.Add(member.Id, member);
            }

            return ht;
        }
    }
}
