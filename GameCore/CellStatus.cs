
namespace GameCore
{
    public enum CellStatus
    {
        /// <summary>
        /// Describes empty cell 
        /// </summary>
        Empty,
        /// <summary>
        /// Describes completion cell
        /// </summary>
        Completion,
        /// <summary>
        /// Describes missed cell
        /// </summary>
        Miss,
        /// <summary>
        /// Describes crippled cell
        /// </summary>
        Crippled,
        /// <summary>
        /// Describes drowned cell
        /// </summary>
        Drowned,
        /// <summary>
        /// Describes free cell
        /// </summary>
        ValidLocation,
        /// <summary>
        /// Describes busy cell
        /// </summary>
        NotValidLocation

    }
}
