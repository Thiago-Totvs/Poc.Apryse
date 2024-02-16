using AprysePoc.FileHelper;
using Microsoft.AspNetCore.Mvc;
using pdftron.PDF;
using pdftron.SDF;

namespace AprysePoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApryseTestController : ControllerBase
    {
        const string APRYSE_KEY = "demo:1708083272960:7f5d1bf503000000001cec61b4f616d37b3355d8063ad749126b422897";

        public ApryseTestController()
        {

            try
            {
                Console.WriteLine("**LOCAL CONSOLE** Initializing Apryse lib.");

                pdftron.PDFNet.Initialize(APRYSE_KEY);

                Console.WriteLine("**LOCAL CONSOLE** Foxit Lib successfully initialized.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while initializing Apryse lib: {ex.Message}");
            }
        }

        [HttpPost("open")]
        public IActionResult OpenFile(IFormFile formFile)
        {

            byte[] byte_buffer = formFile.GetBytesFromFormFile();
            IntPtr buffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(byte_buffer.Length);

            try
            {
                System.Runtime.InteropServices.Marshal.Copy(byte_buffer, 0, buffer, byte_buffer.Length);
                var doc = new PDFDoc(byte_buffer, byte_buffer.Length);

                if (!doc.InitSecurityHandler())
                    return new BadRequestObjectResult("ERROR! Document is encrypted");

                var bytesResult = doc.Save(SDFDoc.SaveOptions.e_linearized);

                doc.Close();

                return new OkObjectResult(bytesResult);

            }
            catch (Exception ex)
            {

                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Opening File: {ex.Message}");
            }

            return new OkResult();
        }
    }
}
