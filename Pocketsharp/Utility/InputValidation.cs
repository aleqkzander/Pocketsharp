using Pocketsharp.Objects;

namespace Pocketsharp.Utility
{
    internal class InputValidation
    {
        /// <summary>
        /// Return true when all register requirements met
        /// </summary>
        /// <param name="client"></param>
        /// <param name="record"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirm"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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

        /// <summary>
        /// Return true when all login requirements met
        /// </summary>
        /// <param name="client"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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
    }
}