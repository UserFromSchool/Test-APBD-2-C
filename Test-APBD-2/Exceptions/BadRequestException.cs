namespace APBD_Test_Retake.Exceptions;

public class BadRequestException(string message = "Bad request 400.", Exception? innerException = null)
    : Exception(message, innerException)
{
}