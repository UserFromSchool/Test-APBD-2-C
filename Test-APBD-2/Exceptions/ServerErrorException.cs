namespace APBD_Test_Retake.Exceptions;

public class ServerErrorException(string message = "Server internal error 500.", Exception? innerException = null)
    : Exception(message, innerException)
{
}