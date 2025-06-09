namespace APBD_Test_Retake.Exceptions;

public class NotFoundException(string message = "Not found 404.", Exception? innerException = null)
    : Exception(message, innerException)
{
}