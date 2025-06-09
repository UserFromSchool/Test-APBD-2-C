namespace APBD_Test_Retake.Exceptions;

public class ConflictException(string message = "Conflict 409.", Exception? innerException = null)
    : Exception(message, innerException)
{
}