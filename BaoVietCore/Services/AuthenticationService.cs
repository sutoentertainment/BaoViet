using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Security.Credentials;
using Windows.Security.Cryptography;
using System.Threading.Tasks;
using BaoVietCore.Interfaces;

namespace BaoVietCore.Services
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private AuthenticationResult authorized = AuthenticationResult.Fail;

        private const string CredentialsName = "BaoVietCredentials";
        private const string Username = "BaoVietUser";

        public AuthenticationService(Manager man) : base(man)
        {

        }

        public async Task<AuthenticationResult> WindowsHelloAuthen()
        {
            // Do we have capability to provide credentials from the device
            if (await KeyCredentialManager.IsSupportedAsync())
            {
                // Get credentials for current user and app
                KeyCredentialRetrievalResult result = await KeyCredentialManager.OpenAsync(CredentialsName);
                if (result.Credential != null)
                {
                    KeyCredentialOperationResult signResult = await result.Credential.RequestSignAsync(CryptographicBuffer.ConvertStringToBinary("Auth", BinaryStringEncoding.Utf8));
                    if (signResult.Status == KeyCredentialStatus.Success)
                    {
                        authorized = AuthenticationResult.Success;
                    }
                }
                // No previous saved credentials found
                else
                {
                    KeyCredentialRetrievalResult creationResult = await KeyCredentialManager.RequestCreateAsync
                        (CredentialsName, KeyCredentialCreationOption.ReplaceExisting);
                    if (creationResult.Status == KeyCredentialStatus.Success)
                    {
                        authorized = AuthenticationResult.Success;
                    }
                }
            }
            else
            {
                return AuthenticationResult.NotAvailable;
            }

            return authorized;
        }

        public AuthenticationResult PasswordAuthen(string input)
        {

            var credential = GetCredentialFromLocker();
            if (credential == null)
                authorized = AuthenticationResult.NotAvailable;
            else
            {
                credential.RetrievePassword();
                if (input == credential.Password)
                    authorized = AuthenticationResult.Success;
                else
                    authorized = AuthenticationResult.Fail;
            }

            return authorized;
        }

        private PasswordCredential GetCredentialFromLocker()
        {
            PasswordCredential credential = null;

            var vault = new PasswordVault();
            IReadOnlyList<PasswordCredential> credentialList = null;
            try
            {
                credentialList = vault.FindAllByUserName(Username);
            }
            catch
            {
                return credential;
            }
            if (credentialList.Count > 0)
            {
                credential = credentialList[0];
            }

            return credential;
        }

        public void CreatePassword(string password)
        {
            var vault = new PasswordVault();
            vault.Add(new PasswordCredential(CredentialsName, Username, password));

        }

        public void DeleteAccount()
        {
            if (string.IsNullOrEmpty(Username))
                return;
            var vault = new PasswordVault();
            var list = vault.FindAllByUserName(Username).ToList();
            foreach (var item in list)
            {
                vault.Remove(item);
            }

        }
    }

}
