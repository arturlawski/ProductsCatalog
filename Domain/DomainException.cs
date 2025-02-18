namespace MediaExpert
{
    /// <summary>
    /// Wyjątek domeny
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Inicjalizuje nową instację <see cref="DomainException"/> z opisem błędu
        /// </summary>
        /// <param name="message">Wiadomość opisująca błąd</param>
        public DomainException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Inicjalizuje nową instancję <see cref="DomainException"/> z opisem błędu
        /// oraz referencją do wyjątku, który go wywołał
        /// </summary>
        /// <param name="message">Wiadomość opisująca błąd</param>
        /// <param name="innerException">Wyjątek, który wywołał błąd <see cref="Exception"/></param>
        public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
