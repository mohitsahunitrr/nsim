namespace NSim
{
    public enum EventState
    {
        /// <summary>
        /// The event has been created
        /// </summary>
        Created, 

        /// <summary>
        /// Triggered; awaiting callback scheduling
        /// </summary>
        Triggered,

        /// <summary>
        /// Succeeded; all callbacks were executed
        /// </summary>
        Succeeded,

        /// <summary>
        /// Failed; should failed events make callbacks? todo
        /// </summary>
        Failed
    }
}
