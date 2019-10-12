using AdwardSoft.DTO.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// Author: AdwardSoft Corrp
/// Custom IdentityUser
/// </summary>
/// <remarks>
/// Developer can modify this class and add more interface suggest by Identity Services: IUserClaimStore, IUserLoginStore...
/// </remarks>
namespace AdwardSoft.Core.Identity
{
    public class UserStore : IUserStore<ApplicationUser>,
        IUserPasswordStore<ApplicationUser>,
        IUserClaimStore<ApplicationUser>,
        IUserLoginStore<ApplicationUser>,
        IUserRoleStore<ApplicationUser>,
        IUserSecurityStampStore<ApplicationUser>,
        IUserTwoFactorStore<ApplicationUser>,
        IUserPhoneNumberStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>,
        IUserLockoutStore<ApplicationUser>
    //IQueryableUserStore<ApplicationUser>

    {
        private readonly IUserRepository _userRepository;

        public UserStore(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region IUserStore
        public async Task<IdentityResult> CreateAsync(ApplicationUser user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userRepository.CreateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userRepository.DeleteAsync(user);

        }

        public async Task<ApplicationUser> FindByIdAsync(string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            return await _userRepository.FindByIdAsync(Convert.ToInt32(userId));

        }

        public async Task<ApplicationUser> FindByNameAsync(string userName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userName == null) throw new ArgumentNullException(nameof(userName));

            return await _userRepository.FindByNameAsync(userName);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedName == string.Empty) throw new ArgumentNullException(nameof(normalizedName));

            user.NormalizedUserName = normalizedName;
            return Task.FromResult<object>(null);
        }
        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userRepository.UpdatePasswordAsync(user);
        }
        #endregion

        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }
        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));

            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);

        }
        #endregion

        #region IUserClaimStore
        public Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));            

            return Task.FromResult<IList<Claim>>(user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList());
        }
        public Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserLoginStore
        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserRoleStore
        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            return Task.FromResult((IList<string>)user.Roles.Select(role => role.Name).ToList());
        }
        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }
        public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
           
            user.SecurityStamp = stamp;
            return Task.FromResult(false);
        }
        #endregion

        #region IUserTwoFactorStore
        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            return Task.FromResult(user.TwoFactorEnabled);
        }
        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserPhoneNumberStore
        public Task<string> GetPhoneNumberAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.PhoneNumber);
        }
        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.PhoneNumberConfirmed);
        }
        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserEmailStore
        public async Task<ApplicationUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedEmail == null) throw new ArgumentNullException(nameof(normalizedEmail));

            return await _userRepository.FindByEmailAsync(normalizedEmail);
        }
        public Task<string> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException("user");


            return Task.FromResult(user.Email);
        }
   
        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.EmailConfirmed);
        }
        public Task<string> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task SetEmailAsync(ApplicationUser user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public async Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException("user");

            await _userRepository.UpdateConfirmEmail(user.Id, confirmed);
        }
        public Task SetEmailConfirmedAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task SetNormalizedEmailAsync(ApplicationUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }
        #endregion

        #region IUserLockoutStore
        public Task<int> GetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.AccessFailedCount);
        }
        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.LockoutEnabled);
        }
        public Task<Nullable<DateTimeOffset>> GetLockoutEndDateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));     

            return Task.FromResult(user.LockoutEndDateUtc != null
                ? new DateTimeOffset?(DateTime.SpecifyKind(user.LockoutEndDateUtc, DateTimeKind.Utc))
                : null);
        }
        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task ResetAccessFailedCountAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled, CancellationToken cancellationToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }
        public Task SetLockoutEndDateAsync(ApplicationUser user, Nullable<DateTimeOffset> lockoutEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion
        public void Dispose()
        {
        }
     }
}
