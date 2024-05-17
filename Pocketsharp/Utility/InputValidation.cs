using Pocketsharp.Objects;

namespace Pocketsharp.Utility
{
    internal class InputValidation
    {
        public static bool RegistrationInputIsValid(HttpClient client, Record record, string password, string passwordConfirm)
        {
            if (string.IsNullOrEmpty(client.BaseAddress?.ToString()))
                throw new NotImplementedException("Setup the base address on the client");

            if (string.IsNullOrEmpty(record.Email))
                throw new NotImplementedException("A email is required for the registration process");

            if (string.IsNullOrEmpty(password))
                throw new NotImplementedException("A password is required for the registration process");

            if (string.IsNullOrEmpty(passwordConfirm))
                throw new NotImplementedException("A password conformation is required for the registration process");

            return true;
        }

        public static bool LoginInputIsValid(HttpClient client, string email, string password)
        {
            if (string.IsNullOrEmpty(client.BaseAddress?.ToString()))
                throw new NotImplementedException("Setup the base address on the client");

            if (string.IsNullOrEmpty(email))
                throw new NotImplementedException("A email is required for the registration process");

            if (string.IsNullOrEmpty(password))
                throw new NotImplementedException("A password is required for the registration process");

            return true;
        }

        public static bool AvatarDownloadInputIsValid(HttpClient client, Response response, string filename)
        {
            if (string.IsNullOrEmpty(client.BaseAddress?.ToString()))
                throw new NotImplementedException("Setup the base address on the client");

            if (string.IsNullOrEmpty(response.Record.CollectionId))
                throw new NotImplementedException("Collection ID is not valid");

            if (string.IsNullOrEmpty(response.Token))
                throw new NotImplementedException("User Token is not valid");

            if (string.IsNullOrEmpty(filename))
                throw new NotImplementedException("Filename is not valid");

            return true;
        }
    }
}